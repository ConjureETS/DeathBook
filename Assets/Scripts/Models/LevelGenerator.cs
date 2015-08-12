using UnityEngine;
using System.Collections.Generic;

namespace DeathBook.Model
{
	public class LevelGenerator
	{
		private int numFriends;
		private int avgConnections;
		private float radius;

		public Level GenerateLevel()
		{
			//and here...


			List<Person> people = CreatePeople();
			List<Friendship> friendships = CreateFriendships(people);

			return null;
		}

		private List<Person> CreatePeople()
		{
			List<Person> people = new List<Person>(numFriends);

			float dlong = Mathf.PI * (3 - Mathf.Sqrt(5)); //~2.39996323

			float dz = (2f / numFriends) * radius;
			float longitude = 0f;
			float z = radius - dz / 2;
			float r, x, y;
			Person p;

			for (int i = 0; i < numFriends; i++)
			{
				r = Mathf.Sqrt(radius * radius - z * z);

				x = Mathf.Cos(longitude) * r;
				y = Mathf.Sin(longitude) * r;

				p = new Person(x,y,z);
				people.Add(p);

				z -= dz;

				longitude += dlong;
			}

			return people;
		}

		private List<Friendship> CreateFriendships(List<Person> people)
		{
			List<Friendship> friendships = new List<Friendship>();

			float rSq = radius * radius;

			for (int i = 0, count = people.Count; i < count; i++)
			{
				for (int j = i+1; j < count; j++)
				{
					// Nombre d'amis présents

					// 

				}
			}




			//TODO
			return null;
		}
	}
}
