﻿using UnityEngine;
using System.Collections.Generic;

namespace DeathBook.Model
{
	public class LevelGenerator
	{
		private int numPeople;
		private int avgConnections;
		private float probability;
		private float radius;

		private const float minConnTime = 3;
		private const float maxConnTime = 20;

		public Level GenerateLevel(int numPeople, int avgFriends, float probability, float radius, GameStrategy strategy)
		{
			this.numPeople = numPeople;
			this.avgConnections = avgFriends;
			this.probability = probability;
			this.radius = radius;

			List<Person> people = CreatePeople();
			List<FriendshipLink> friendships = CreateFriendships(people);

			return new Level(people, friendships, strategy);
		}

		private List<Person> CreatePeople()
		{
			List<Person> people = new List<Person>(numPeople);

            /* Sphere uniform distribution using the spiral method with the golden angle
            * ~2.39996323 rad, the golden angle (the most irrational angle)
            * is used here to make sure that the sin and cos functions
            * dont end up drawing clusters of points and the spirals are way
            * less visible.
            */
			float dlong = Mathf.PI * (3 - Mathf.Sqrt(5)); //~2.39996323

			float dz = (2f / numPeople) * radius;
			float longitude = 0f;
			float z = radius - dz / 2;
			float r, x, y;
			Person p;

			for (int i = 0; i < numPeople; i++)
			{
				r = Mathf.Sqrt(radius * radius - z * z);

				x = Mathf.Cos(longitude) * r;
				y = Mathf.Sin(longitude) * r;

				p = CreatePerson(i, x, y, z);

				people.Add(p);

				z -= dz;

				longitude += dlong;
			}

			return people;
		}

		private List<FriendshipLink> CreateFriendships(List<Person> people)
		{
			List<FriendshipLink> friendships = new List<FriendshipLink>();
			Person p1, p2;

			int totalCount  = people.Count;
			int missing;

			LinkedList<DistanceNode> list = new LinkedList<DistanceNode>();

			for (int i = 0; i < totalCount; i++)
			{
				p1 = people[i];
				missing = Mathf.Clamp((int) Utils.GetRandomValue(avgConnections, avgConnections*1.2f, 3), 1, 100) - p1.FriendCount;

				if (missing <= 0)
					continue;

				list.Clear();

				for (int j = i+1; j < totalCount; j++)
				{
					p2 = people[j];
					if (p2.FriendCount < avgConnections * 1.2)
						list.AddLast(new DistanceNode(p1, p2));
				}

				while (list.Count > 0 && missing > 0)
				{
					DistanceNode smallest = list.First.Value;

					//Lerp between probability and 1, depending on how many are left and how many are missing

					float prob = Mathf.Lerp(probability, 1, missing / list.Count);
					foreach (DistanceNode node in list)
					{
						if (node.dist < smallest.dist)
							smallest = node;
					}
					//TODO Code/use a heap instead

					if (Random.value < prob)
					{
						friendships.Add(CreateFriendship(p1, smallest.p));
						missing--;
					}
					list.Remove(smallest);
				}
			}

			return friendships;
		}

		private FriendshipLink CreateFriendship(Person p1, Person p2)
		{
			FriendshipLink f = new FriendshipLink(p1, p2, Random.value);
			Friendship f1 = new Friendship(p1, p2, f);
			Friendship f2 = new Friendship(p2, p1, f);
			f1.Other = f2;
			f2.Other = f1;

			p1.AddFriendship(f1);
			p2.AddFriendship(f2);

			return f;
		}

		private Person CreatePerson(int id, float x, float y, float z)
		{
			Vector3 pos = new Vector3(x, y, z);
			//Value between 3 and 21
			float connectionDuration = Utils.GetRandomValue(12, 9, 3);
			int connectionTime = Random.Range(0, 24 * 60);
			int disconnectionTime = (connectionTime + (int)(connectionDuration * 60)) % (24 * 60);
			float freq = Utils.GetRandomValue(0, 1, 3);

			bool isFemale = Random.value <= 0.5;
			
            /*
			string fName = "Fifi"; //isFemale ? NameGenerator.GetFemaleName() : NameGenerator.GetMaleName();
			string lName = "Brindacier"; //NameGenerator.GetLastName();*/

            var generatedPerson = isFemale ? PersonGenerator.GetGeneratedFemale() : PersonGenerator.GetGeneratedMale();

            Person p = new Person(id, generatedPerson.FirstName, generatedPerson.LastName, pos, connectionTime, disconnectionTime, freq, generatedPerson.Picture);

			return p;
		}
	}

	internal class DistanceNode
	{
		public Person p;
		public float dist;

		public DistanceNode(Person p1, Person p2)
		{
			p = p2;
			dist = (p2.InitialPosition - p1.InitialPosition).sqrMagnitude;
		}
	}
}
