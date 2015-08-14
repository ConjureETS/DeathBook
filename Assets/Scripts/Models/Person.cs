using UnityEngine;
using System.Collections.Generic;

namespace DeathBook.Model
{
	public class Person
	{
		public int id;
		private string name;
		private List<Friendship> friendList = new List<Friendship>();
		public Vector3 initialPosition;
		public float connectionTime;
		public float disconnectionTime;
		public float awarenessLevel;
		public bool alive;
		public bool connected;

		public int numFriends;
		public int timeBetweenPosts; // f = 1/T;
		public float importance; // Size of the quad

		private int happiness;

		public Person(int id)
		{
			this.id = id;
		}

		public void AddFriendship(Friendship f)
		{
			friendList.Add(f);
			numFriends++;
		}

		private bool isConnected(int time)
		{
			return disconnectionTime > time && time > connectionTime;
		}

		private int calculateWeight()
		{
			//friendCount * ____ + 1/timeBetweenPosts + }
			return 0;
		}
	}
}