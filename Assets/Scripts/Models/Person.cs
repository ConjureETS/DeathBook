using UnityEngine;
using System.Collections.Generic;
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
		public float AwarenessLevel { get { return awarenessLevel; }
			set
			{
				float change = value - awarenessLevel;
				awarenessLevel = value;
				LevelManager.Instance.GameLevel.AddAwareness(change);
				NotifyObservers();
			}
		}

		private Level level;
		private Level GameLevel
		{
			get
			{
				if (level == null)
					level = LevelManager.Instance.GameLevel;
				return level;
			}
		}

		private int lastFriendDeath = 0;
		public int LastFriendDeath { get { return lastFriendDeath; } }

		private bool alive = true;
		public bool Alive { get { return alive; } }

		private bool online = true;
		public bool Online { get { return online; } set { online = value; NotifyObservers(); } }
		
		private Sprite picture;
		public Sprite Picture { get { return picture; } }

		private System.Action onSelected;
		public System.Action OnSelected {get {return onSelected;} set { onSelected = value; } }

		private ShoutBubble status = null;
		public ShoutBubble CurrentStatus 
		{ 
			get { return status; }
			set { status = value; NotifyObservers(); } 
		}

		private GameStrategy strategy;
		public GameStrategy Strategy
		{
			get
			{
				if (strategy == null)
					strategy = LevelManager.Instance.GameLevel.Strategy;
				return strategy;
			}
		}

		public Person(int id, string fName, string lName, Vector3 pos, int conn, int disconn, float freq, Sprite pic)
		{
			this.id = id;
			this.firstName = fName;
			this.lastName = lName;
			this.initialPosition = pos;
			this.connectionTime = conn;
			this.disconnectionTime = disconn;
			//Debug.Log("I am " + id + " and I connect at " + Utils.GetTimeString(connectionTime) + " until " + Utils.GetTimeString(disconnectionTime));
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
			//Debug.Log("I am " + id + " and my friend " + f.Friend.Id + " was killed");
			numAliveFriends--;
			numDeadFriends++;
			deadFriendsList.Add(f);
		}

		public bool Kill()
		{
            if (Online || (LevelManager.Instance.GameLevel.tutorialInt > -1 && LevelManager.Instance.GameLevel.tutorialInt < 4))
			    return false;

			//Debug.Log("Person " + id + " died!");
			alive = false;
			CurrentStatus = null;

            if (GameLevel.tutorialInt == 4)
                GameLevel.tutorialInt = 5;

			foreach (Friendship f in friendsList)
				f.Other.NotifyFriendWasKilled();
			NotifyObservers();

			GameLevel.RegisterKill(this);

            if (LevelManager.Instance.GameLevel.tutorialInt == 5 && LevelManager.Instance.GameLevel.NumDead == 3)
            {
                LevelManager.Instance.GameLevel.tutorialInt = 6;
            }

			return true;
		}

		public void NoticeDeath(Friendship f)
		{
			int deathTime = GameLevel.GameTime;
			int sinceLastDeath = numDeadFriends == 0 ? int.MaxValue/2 : deathTime - lastFriendDeath;

			float strategyOutput = Strategy.GetAwarenessChange(numDeadFriends, numAliveFriends, sinceLastDeath);

			AwarenessLevel = Mathf.Min(AwarenessLevel + strategyOutput, 1f);

			if (Random.value < 0.2f)
			{
				CurrentStatus = new ShoutBubble(GameLevel.GameTime, f);
			}

			//Debug.Log("I am " + id + " and I know my friend " + f.Friend.Id + " was killed.. " + strategyOutput);
		}

		//Time in hours
		private bool IsOnline(int time)
		{
			if (ConnectionTime < DisconnectionTime)
				return (time > ConnectionTime && time < DisconnectionTime);
			return !(time < ConnectionTime && time > DisconnectionTime);
		}

		/*private void ShareStatus()
		{
			if (Random.value < 0.3f)
			{
				foreach (Friendship f in friendsList)
					f.Friend.ReceiveStatus(CurrentStatus);
			}
		}*/

		/*public void ReceiveStatus(Status newStatus)
		{
			Debug.Log("Oh noes, " + newStatus.Friends.Friend + " died...");
			foreach (Friendship f in friendsList)
			{
				if (f.Friend == newStatus.Friends) ;
			}
		}*/

		public void Update(float deltaTime)
		{
			if (CurrentStatus != null)
			{
				if (CurrentStatus.EndTime < GameLevel.GameTime)
				{
					//ShareStatus();
					CurrentStatus = null;
				}
			}

			if (!Alive)
				return;

			int time = GameLevel.DayTime;

			bool isOnline = IsOnline(time);

			if (isOnline != Online)
				Online = isOnline;

			//The following actions are only performed if user is online
			if (!Online)
				return;

			foreach (Friendship f in deadFriendsList)
				f.Update(deltaTime);
		}
		
		public void SelectNode()
		{
			if (OnSelected != null)
			{
				OnSelected();
			}
		}
	}
}
