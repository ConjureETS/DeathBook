using System.Collections.Generic;
using UnityEngine;

namespace DeathBook.Model
{
	public class Utils
	{
		public static void Test()
		{
			float mean = 50;
			float range = 50;
			int numSteps = 2;

			int numTries = 100;

			for (int i = 0; i < numTries; i++)
			{
				Debug.Log(GetRandomValue(mean, range, numSteps));
			}
		}

		public static float GetRandomValue(float mean, float range, int numSteps)
		{
			float sum = 0;
			for (int i = 0; i < numSteps; i++)
			{
				sum += Random.value;
			}
			return (sum / numSteps * 2 - 1) * range + mean;
		}

		public static string GetTimeString(int time)
		{
            float dTime = time / 60;

            // Quick patch, but it's working (1 hour off)
            return ((dTime % 12) + 1) + ":" + (time % 60).ToString("D2") + ((dTime >= 11 && dTime <= 22) ? " PM" : " AM");
		}
	}
}
