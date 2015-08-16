using UnityEngine;
using System.Collections;
using DeathBook.Model;

public class RulesTest : MonoBehaviour {

	public GameStrategy strategy = new GameStrategy();
	public ChangeAwarenessTest awareness = new ChangeAwarenessTest();
	public NoticeDeathTest noticeDeath = new NoticeDeathTest();

	public static GameStrategy str = null;

	[System.Serializable]
	public class ChangeAwarenessTest
	{
		public int numDeadFriends;
		public int numAliveFriends;
		public int sinceLastDeath;
		public bool test = false;
		public float result = 0f;

		public void Update()
		{
			if (!test)
				return;

			test = false;

			result = RulesTest.str.GetAwarenessChange(numDeadFriends, numAliveFriends, sinceLastDeath);
		}
	}

	[System.Serializable]
	public class NoticeDeathTest 
	{
		public float popularity;
		public float importance;
		public int numAliveFriends;
		public float awareness;

		public bool test = false;
		public float result = 0f;

		public void Update()
		{
			if (!test)
				return;

			test = false;

			result = RulesTest.str.GetDeathNoticing(popularity, importance, numAliveFriends, awareness);
		}
	}


	// Use this for initialization
	void Start () {
		str = strategy;
	}
	
	// Update is called once per frame
	void Update () {
		awareness.Update();
	}
}
