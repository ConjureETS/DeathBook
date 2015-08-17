using UnityEngine;
using System.Collections;
using DeathBook.Util;
using DeathBook.Model;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text TimerText;
	int lastTime = -1;

    void Update()
    {
		int time = LevelManager.Instance.GameLevel.DayTime / 30;
		if (time != lastTime)
		{
			TimerText.text = Utils.GetTimeString(time * 30);
			lastTime = time;
		}
    }
}
