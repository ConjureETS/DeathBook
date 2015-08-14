using UnityEngine;
using System.Collections.Generic;
using System;
using DeathBook.Util;

namespace DeathBook.Model
{
	public class Person : Observable, Updatable
	{
		public int id;
		public int Id { get { return id; } }

		private string firstName;
		private string lastName;
		public string Name { get { return firstName + " " + lastName; } }
		public string FirstName { get { return firstName; } }

		private Vector3 initialPosition;
		public Vector3 InitialPosition { get { return initialPosition; } }

		private List<Friendship> friendsList = new List<Friendship>();
		public List<Friendship> FriendList { get { return friendsList; } }
		private List<Friendship> deadFriendsList = new List<Friendship>();
		public List<Friendship> DeadFriendList { get { return deadFriendsList; } }

		private int numAliveFriends = 0;
		private int numDeadFriends = 0;
		private int friendCount = 0;
        public int FriendCount { get { return friendCount; } }

		private int timeBetweenPosts; // f = 1/T;
		public int TimeBetweenPosts { get { return timeBetweenPosts; } }

		private float connectionTime;
		public float ConnectionTime { get { return connectionTime; } }

		private float disconnectionTime;
		public float DisconnectionTime { get { return disconnectionTime; } }

		private float awarenessLevel = 0; //on a scale from 0 to 1
		public float AwarenessLevel { get { return awarenessLevel; } }

		private bool alive = true;
		public bool Alive { get { return alive; } }

		//private int happiness = 1; //on a scale from 0 to 1
		//public int Happiness { get { return happiness; } }

		private bool online = false;
		public bool Online { get { return online; } }

		public Person(int id, Vector3 pos)
		{
			this.id = id;
			initialPosition = pos;

            // TODO Use names from db
			firstName = "Mark";
			lastName = "Zuckerberg";
		}

		public void AddFriendship(Friendship f)
		{
			friendsList.Add(f);
			numAliveFriends++;
			friendCount++;
		}

		public void KillFriend(Friendship f)
		{
			numAliveFriends--;
			numDeadFriends++;
			deadFriendsList.Add(f);
		}

		public void Update(float deltaTime)
		{
			//TODO Update if connected
			int time = LevelManager.Instance.GameLevel.GameTime;

			//The following actions are only performed if user is online
			if (!Online)
				return;
		}
	}
}