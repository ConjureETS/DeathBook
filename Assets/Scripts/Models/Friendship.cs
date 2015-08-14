using System;
using System.Collections.Generic;
using UnityEngine;
using DeathBook.Util;

namespace DeathBook.Model
{
	public class Friendship : Observable, Updatable
	{
		private Person self;
		public Person Self { get { return self; } }
		private Person friend;
		public Person Friend { get { return friend; } }
		
		private Friendship other;
		public Friendship Other { get { return other; } set { other = value; } }

		private FriendshipLink link;
		public FriendshipLink Link { get { return link; } }

		private float awareness = 0; //on a scale from 0 to 1
		public float Awareness { get { return awareness; } }

		public Friendship(Person self, Person friend)
		{
			this.self = self;
			this.friend = friend;
		}

		public void Update(float deltaTime)
		{
			//This function is only called when friend is dead
			awareness = Mathf.Max(awareness + deltaTime * CalculateWeight(), 100);

			NotifyObservers();
		}

		//returns a number between 0 and 1
		private float CalculateWeight()
		{
			float weight = 0;

			weight += link.Importance;

			return weight;
		}

		internal enum Knowledge
		{
			Alive, Doubt, Dead
		}
	}
}
