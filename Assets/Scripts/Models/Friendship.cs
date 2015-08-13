using System;
using System.Collections.Generic;

namespace DeathBook.Model
{
	public class Friendship
	{
		public Person friend1, friend2;
		private int importance; //on a scale from 1 to 100

		public Friendship(Person p1, Person p2, int scale)
		{
			friend1 = p1;
			friend2 = p2;
			importance = scale;
		}
	}
}
