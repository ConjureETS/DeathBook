using UnityEngine;
using System.Collections.Generic;
using DeathBook.Util;

namespace DeathBook.Model
{
	public class Level : Observable, Updatable
	{
		private const float TimeScale = 30f;

		private int score;
		public int Score { get { return score; } }

		private List<Person> people;
		public List<Person> People { get { return people; } }
		private List<FriendshipLink> friendships;
		public List<FriendshipLink> Friendships { get { return friendships; } }

		//1 = 1 minute
		private float gameTime;
		public int GameTime { get { return (int)(gameTime * TimeScale); } }

		private float globalAwareness; //on a scale from 0 to 1
		public float GlobalAwareness { get { return globalAwareness; } }

		public Level(List<Person> people, List<FriendshipLink> friendships)
		{
			this.people = people;
			this.friendships = friendships;
		}

		public void Update(float deltaTime)
		{
			gameTime += deltaTime;
			NotifyObservers();

			//TODO Global awareness - start trends
		}
	}
}
