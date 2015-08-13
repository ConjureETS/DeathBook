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
		public int numFriends;
		private int timeBetweenPosts; // f = 1/T;
		private int connectionTime;
		private int disconnectionTime;
		private int awarenessLevel;
		private bool alive;

		private int happiness;
		private bool connected;

		//private Node node;

		public Person(int id, float x, float y, float z)
		{
			this.id = id;
			initialPosition = new Vector3(x, y, z);
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