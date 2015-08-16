using UnityEngine;
using System.Collections.Generic;
using DeathBook.Util;

namespace DeathBook.Model
{
	public class Level : Observable, Updatable
	{
		private const float TimeScale = 30*4f;

		private int score;
		public int Score { get { return score; } }

		private List<Person> people;
		public List<Person> People { get { return people; } }
		private List<FriendshipLink> friendships;
		public List<FriendshipLink> Friendships { get { return friendships; } }

		//1 = 1 minute
		private float gameTime; // real seconds elapsed since beginning
		public int GameTime { get { return (int)(gameTime * TimeScale); } }
		//Time of day, between 0 minute to 1440 minutes (a day)
		public int DayTime { get { return GameTime % (24*60); } }

		private int lastHour = -1;

		private float globalAwareness; //on a scale from 0 to 1
		public float GlobalAwareness { get { return globalAwareness; } }

		private GameStrategy strategy = null;
		public GameStrategy Strategy { get { return strategy; } }

		private int numAlive;
		public int NumAlive { get { return numAlive; } set { numAlive = value; NotifyObservers(); } }

		private int numDead;
		public int NumDead { get { return numDead; } set { numDead = value; NotifyObservers(); } }

		private float awareness;
		public float Awareness { get { return awareness; } set { awareness = value; NotifyObservers(); } }


		public Level(List<Person> people, List<FriendshipLink> friendships, GameStrategy strategy)
		{
			this.people = people;
			this.friendships = friendships;
			this.strategy = strategy;
			this.numAlive = people.Count;
			this.numDead = 0;
			this.awareness = 0;
		}

		public void RegisterKill(Person p)
		{
			numDead++;
			numAlive--;
			Awareness = (Awareness * (NumAlive + 1) - p.AwarenessLevel) / NumAlive;
			Debug.Log("Killed - " + p.AwarenessLevel + " nK = " + numAlive);
		}

		public void AddAwareness(float addition)
		{
			Awareness += addition / NumAlive;
			Debug.Log("Added - " + addition + " nK = " + numAlive);
		}

		public void Update(float deltaTime)
		{
			gameTime += deltaTime;
			int hour = DayTime / 60;
			if (hour != lastHour)
			{
				lastHour = hour;
				NotifyObservers();
			}
		}
	}
}
