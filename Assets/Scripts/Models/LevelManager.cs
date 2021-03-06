﻿using UnityEngine;
using System.Collections.Generic;

namespace DeathBook.Model
{
	public class LevelManager
	{
		private static LevelManager instance = new LevelManager();
		public static LevelManager Instance { get {return instance; } }

		private Level level = null;
		public Level GameLevel { get { return level; } }

		private LevelGenerator gen = new LevelGenerator();

		private LevelManager() {}

		public Level NewLevel(int numPeople, int avgFriends, float probability, float radius, GameStrategy strategy)
		{
			return level = gen.GenerateLevel(numPeople, avgFriends, probability, radius, strategy);
		}
	}
}
