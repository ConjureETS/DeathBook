using UnityEngine;
using System.Collections.Generic;

namespace DeathBook.Model
{
	public class LevelGenerator
	{
		private int numPeople;
		private int avgConnections;
		private float probability;
		private float radius;

		public Level GenerateLevel(int numPeople, int avgFriends, float probability, float radius)
		{
			this.numPeople = numPeople;
			this.avgConnections = avgFriends;
			this.probability = probability;
			this.radius = radius;

			List<Person> people = CreatePeople();
			List<Friendship> friendships = CreateFriendships(people);

			return new Level(people, friendships);
		}

		private List<Person> CreatePeople()
		{
			List<Person> people = new List<Person>(numPeople);

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

				p = new Person(i, x, y, z);
				people.Add(p);

				z -= dz;

				longitude += dlong;
			}

			Debug.Log("People: " + people.Count);

			return people;
		}

		private List<Friendship> CreateFriendships(List<Person> people)
		{
			Debug.Log("Creating friendships" + probability);

			List<Friendship> friendships = new List<Friendship>();
			Person p1, p2;

			int totalCount  = people.Count;
			int missing;

			LinkedList<DistanceNode> list = new LinkedList<DistanceNode>();

			for (int i = 0; i < totalCount; i++)
			{
				p1 = people[i];
				missing = avgConnections - p1.numFriends; // TODO Add randomness

				if (missing <= 0)
					break;

				list.Clear();

				for (int j = i+1; j < totalCount; j++)
				{
					p2 = people[j];
					if (p2.numFriends < avgConnections * 1.2)
						list.AddLast(new DistanceNode(p1, p2));
				}

				while (list.Count > 0 && missing > 0)
				{
					DistanceNode smallest = list.First.Value;

					//Lerp between probability and 1, depending on how many are left and how many are missing

					float prob = Mathf.Lerp(probability, 1, missing / list.Count);
					foreach (DistanceNode node in list)
					{
						if (node.dist < smallest.dist && Random.value < prob)
							smallest = node;
					}
					//TODO Code/use a heap instead

					friendships.Add(CreateFriendship(p1, smallest.p));
					missing--;
					list.Remove(smallest);
				}
			}
			Debug.Log(friendships.Count);
			return friendships;
		}

		private Friendship CreateFriendship(Person p1, Person p2)
		{
			Friendship f = new Friendship(p1, p2, Random.Range(1,100));
			p1.AddFriendship(f);
			p2.AddFriendship(f);
			return f;
		}
	}

	internal class DistanceNode
	{
		public Person p;
		public float dist;

		public DistanceNode(Person p1, Person p2)
		{
			p = p2;
			dist = (p2.initialPosition - p1.initialPosition).sqrMagnitude;
		}
	}


	/*
	 * 1. Friendship urgency - 0-1
	 *		Number of friends missing
	 *		VS number of nodes left
	 *		
	 * 2. Friendship possibility
	 *		Closeness
	 *			0 < distance^2 < root(2)*rSq < 4*rSq
	 * 
	 */ 
}
