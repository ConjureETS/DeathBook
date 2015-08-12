using UnityEngine;
using System.Collections.Generic;

namespace DeathBook.Model
{
	public class Person
	{
		private string name;
		private List<Friendship> friendList = new List<Friendship>();
		public Vector3 initialPosition;
		private int timeBetweenPosts; // f = 1/T;
		private int connectionTime;
		private int disconnectionTime;
		private int awarenessLevel;
		private bool alive;

		private int happiness;
		private bool connected;

		//private Node node;

		public Person(float x, float y, float z)
		{
			initialPosition = new Vector3(x, y, z);
		}

		private bool isConnected(int time)
		{
			return disconnectionTime > time && time > connectionTime;
		}

		private int calculateWeight()
		{
			//friendCount * ____ + 1/timeBetweenPosts + }
			return 0;
		}
	}
}