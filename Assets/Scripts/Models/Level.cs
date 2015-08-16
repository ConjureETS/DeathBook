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
        public int tutorialInt = 0;

		public Level(List<Person> people, List<FriendshipLink> friendships)
		{
			this.people = people;
			this.friendships = friendships;
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

			//TODO Global awareness - start trends
		}
	}
}
