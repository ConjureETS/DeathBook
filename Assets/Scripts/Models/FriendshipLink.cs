using System;
using System.Collections.Generic;
using DeathBook.Util;

namespace DeathBook.Model
{
	public class FriendshipLink : Observable
	{
		private Person friend1, friend2;
		public Person Friend1 { get { return friend1; } }
		public Person Friend2 { get { return friend2; } }
		
		private float importance; //on a scale from 0 to 1
		public float Importance { get { return importance; } }

		private float awareness = 0; //on a scale from 0 to 1
		public float Awareness
		{
			get { return awareness; }
			set { awareness = value; NotifyObservers(); }
		}

		public FriendshipLink(Person p1, Person p2, float importance)
		{
			friend1 = p1;
			friend2 = p2;
			this.importance = importance;
		}
	}
}
