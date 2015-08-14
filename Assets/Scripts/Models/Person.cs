using UnityEngine;
using System.Collections.Generic;
using System;

namespace DeathBook.Model
{
	public class Person : Observable
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

        public string Name
        {
            get { return name; }
        }

        public bool Alive
        {
            get { return alive; }
        }

        public List<Friendship> FriendList
        {
            get { return friendList; }
        }

        public int FriendsCount
        {
            get { return numFriends; }
        }

		public bool Connected
		{
			get { return connected; }
		}

		public Person(int id, Vector3 pos)
		{
			this.id = id;
			initialPosition = pos;
            alive = true;

            // Temporary
            name = String.Format("Firstname{0} Lastname{0}", id);
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