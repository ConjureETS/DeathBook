using System;
using System.Collections.Generic;
using UnityEngine;
using DeathBook.Util;

namespace DeathBook.Model
{
	public class Friendship : Updatable
	{
		private Person self;
		public Person Self { get { return self; } }
		private Person friend;
		public Person Friend { get { return friend; } }
		
		private Friendship other;
		public Friendship Other { get { return other; } set { other = value; } }

		private FriendshipLink link;
		public FriendshipLink Link { get { return link; } }

		private bool noticedDeath = false;

		public Friendship(Person self, Person friend, FriendshipLink link)
		{
			this.self = self;
			this.friend = friend;
			this.link = link;
		}

		public void Update(float deltaTime)
		{
			if (noticedDeath)
				return;

			//This function is only called when friend is dead
			//awareness = Mathf.Min(awareness + deltaTime * CalculateWeight(), 1);
			link.Awareness = Mathf.Min(link.Awareness + deltaTime * 0.1f, 1f);
			if (link.Awareness >= 1f)
			{
				self.NoticeDeath(this);
				noticedDeath = true;
			}
		}

		//returns a number between 0 and 1
		private float CalculateWeight()
		{
			float weight = 0;

			weight += link.Importance;
			//weight += friend.TimeBetweenPosts;

			return weight * 0.1f;
		}

		/*internal enum Knowledge
		{
			Alive, Doubt, Dead
		}*/
	}
}
