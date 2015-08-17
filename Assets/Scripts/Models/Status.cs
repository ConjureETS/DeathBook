using System.Collections.Generic;

namespace DeathBook.Model
{
	public class Status
	{
		private int startTime;
		public int StartTime { get { return startTime; } }
		private int endTime;
		public int EndTime { get { return endTime; } }
		private Friendship friends;
		public Friendship Friends { get { return friends; } }

		public Status(int startTime, Friendship friendship)
		{
			this.startTime = startTime;
			this.endTime = startTime + (int) Utils.GetRandomValue(700, 250, 3);
			friends = friendship;
		}
	}
}