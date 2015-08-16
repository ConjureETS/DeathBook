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

		private float postFrequency; //on a scale from 0 to 1
		public float PostFrequency { get { return postFrequency; } }

		private int connectionTime;
		public int ConnectionTime { get { return connectionTime; } }
		private int disconnectionTime;
		public int DisconnectionTime { get { return disconnectionTime; } }

		private float awarenessLevel = 0; //on a scale from 0 to 1
		public float AwarenessLevel { get { return awarenessLevel; } }

		private bool alive = true;
		public bool Alive { get { return alive; } }

		private bool online = true;
		public bool Online { get { return online; } set { online = value; NotifyObservers(); } }
		
		private Sprite picture;
		public Sprite Picture { get { return picture; } }


		public Person(int id, string fName, string lName, Vector3 pos, int conn, int disconn, float freq, Sprite pic)
		{
			this.id = id;
			this.firstName = fName;
			this.lastName = lName;
			this.initialPosition = pos;
			this.connectionTime = conn;
			this.disconnectionTime = disconn;
			Debug.Log("I am " + id + " and I connect at " + Utils.GetTimeString(connectionTime) + " until " + Utils.GetTimeString(disconnectionTime));
			this.postFrequency = freq;
			this.picture = pic;

			online = IsOnline(0);
		}

		public void AddFriendship(Friendship f)
		{
			friendsList.Add(f);
			numAliveFriends++;
			friendCount++;
		}

		public void NotifyFriendWasKilled(Friendship f)
		{
			Debug.Log("I am " + id + " and my friend " + f.Friend.Id + " was killed");
			numAliveFriends--;
			numDeadFriends++;
			deadFriendsList.Add(f);
		}

		public bool Kill()
		{
			if (Online)
				return false;

			Debug.Log("Person " + id + " died!");
			alive = false;
			foreach (Friendship f in friendsList)
				f.Friend.NotifyFriendWasKilled(f.Other);
			NotifyObservers();

			return true;
		}

		public void NoticeDeath(Friendship f)
		{
			//TODO apply more rules here
			awarenessLevel = Mathf.Min(AwarenessLevel + 0.2f, 1f);
			Debug.Log("I am " + id + " and I know my friend " + f.Friend.Id + " was killed.. " + AwarenessLevel);
			//TODO remove from dead friends list to accelerate
			NotifyObservers();
		}

		//Time in hours
		private bool IsOnline(int time)
		{
			if (ConnectionTime < DisconnectionTime)
				return (time > ConnectionTime && time < DisconnectionTime);
			return !(time < ConnectionTime && time > DisconnectionTime);
		}

		public void Update(float deltaTime)
		{
			int time = LevelManager.Instance.GameLevel.DayTime;

			bool isOnline = IsOnline(time);

			if (isOnline != Online)
				Online = isOnline;

			//The following actions are only performed if user is online
			if (!Online)
				return;

			foreach (Friendship f in deadFriendsList)
				f.Update(deltaTime);
		}
	}
}