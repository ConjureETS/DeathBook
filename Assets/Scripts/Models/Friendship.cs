using System;
using System.Collections.Generic;
using UnityEngine;
using DeathBook.Util;

namespace DeathBook.Model
{
	public class Friendship : Updatable, IComparable<Friendship>
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

		public void NotifyFriendWasKilled()
		{
			Link.KillCount++;
			Self.NotifyFriendWasKilled(this);
		}

		public void Update(float deltaTime)
		{
			if (noticedDeath)
				return;

			//This function is only called when friend is dead
			//awareness = Mathf.Min(awareness + deltaTime * CalculateWeight(), 1);

			link.Awareness += Self.Strategy.GetDeathNoticing(1f /*Friend.Popularity*/, Link.Importance, Self.FriendCount, Self.AwarenessLevel);

			if (link.Awareness >= 1f)
			{
				self.NoticeDeath(this);
				noticedDeath = true;
			}
		}

        public int CompareTo(Friendship other)
        {
            int value = 0;

            if (this.Friend.AwarenessLevel < other.Friend.AwarenessLevel)
            {
                value = 1;
            }
            else if (this.Friend.AwarenessLevel > other.Friend.AwarenessLevel)
            {
                value = -1;
            }

            return value;
        }
	}
}
