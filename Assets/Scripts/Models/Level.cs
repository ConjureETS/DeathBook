using UnityEngine;
using System.Collections.Generic;

namespace DeathBook.Model
{
	public class Level
	{
		private int score;

		public List<Person> people;
		public List<Friendship> friendships;


		//private Generator gen;
		private int gameTime;
		private int globalAwareness;

		public Level(List<Person> people, List<Friendship> friendships)
		{
			this.people = people;
			this.friendships = friendships;
		}
	}
}
