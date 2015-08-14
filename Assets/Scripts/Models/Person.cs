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

        private Texture picture;

		//private Node node;

        public string Name
        {
            get { return name; }
        }

        public bool Alive
        {
            get { return alive; }
        }

        public int AwarenessLevel
        {
            get { return awarenessLevel; }
        }

        public List<Friendship> FriendList
        {
            get { return friendList; }
        }

        public int FriendsCount
        {
            get { return numFriends; }
        }

        public bool Online
        {
            get { return connected; }
        }

        public Texture Picture
        {
            get { return picture; }
        }

		public Person(int id, float x, float y, float z)
		{
			this.id = id;
			initialPosition = new Vector3(x, y, z);
            alive = true;

            // For testing purposes
            picture = UnityEngine.Random.Range(0, 2) == 0 ? PictureGenerator.GetFemalePicture() : PictureGenerator.GetMalePicture();

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