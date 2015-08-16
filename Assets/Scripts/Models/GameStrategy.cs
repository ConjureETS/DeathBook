using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace DeathBook.Model
{
	[System.Serializable]
	public class GameStrategy
	{
		[System.Serializable]
		public class AwarenessChangeOptions
		{
			public Vector3 friendRatio = new Vector3();
			public Vector3 lastDeath = new Vector3();
			public float maxDeathDuration = 24 * 60 * 7;
			public float modifier = 0.1f;
		}
		
		[System.Serializable]
		public class DeathNoticingOptions
		{
			public Vector3 popularity = new Vector3();
			public Vector3 importance = new Vector3();
			public Vector3 numFriends = new Vector3();
			public Vector3 awareness = new Vector3();
			public float modifier = 0.1f;
		}
		
		[System.Serializable]
		public class ChanceToPostOptions
		{
			public Vector3 popularity = new Vector3();
			public Vector3 importance = new Vector3();
			public Vector3 frequency = new Vector3();
			public float modifier = 0.1f;
		}


		public AwarenessChangeOptions awarenessChange = new AwarenessChangeOptions();
		public DeathNoticingOptions deathNoticing = new DeathNoticingOptions();
		public ChanceToPostOptions chanceToPost = new ChanceToPostOptions();

		public float GetAwarenessChange(int numDeadFriends, int numAliveFriends, int sinceLastDeath)
		{
			float friendsRatioInd = numAliveFriends == 0 ? 1 : GetValue(Mathf.Min(numDeadFriends * 1f / numAliveFriends, 1f), awarenessChange.friendRatio);
			
			float max = awarenessChange.maxDeathDuration;
			float lastDeathInd = GetValue(1 - Mathf.Min(sinceLastDeath, max) / max, awarenessChange.lastDeath);

			Debug.Log(friendsRatioInd + "  --  " + lastDeathInd);
			
			return Mathf.Clamp(friendsRatioInd * lastDeathInd, 0, 1) * awarenessChange.modifier;
		}

		public float GetDeathNoticing(float friendPopularity, float friendshipImportance, int numAliveFriends, float awareness)
		{
			float popularityInd = GetValue(friendPopularity, deathNoticing.popularity);

			float importanceInd = GetValue(friendshipImportance, deathNoticing.importance);

			float aliveFriendsInd = GetValue(Mathf.Min(numAliveFriends * 0.1f, 1), deathNoticing.numFriends);

			float awarenessInd = GetValue(awareness, deathNoticing.awareness);

			return Mathf.Clamp(popularityInd * importanceInd * aliveFriendsInd * awarenessInd, 0, 1) * deathNoticing.modifier; 
		}

		public float GetChanceToPost(float friendPopularity, float friendshipImportance)
		{
			float popularityInd = GetValue(1 - friendPopularity, deathNoticing.popularity);

			float importanceInd = GetValue(1 - friendshipImportance, deathNoticing.importance);

			return Mathf.Clamp(popularityInd * importanceInd, 0, 1) * chanceToPost.modifier; 
		}

		public float GetChanceToRead()
		{
			return 0;
		}

		private float GetValue(float data, Vector3 modifier)
		{
			return Mathf.Clamp(modifier[0] + modifier[1] * data, 0, 1) * modifier[2];
		}
	}
}
