using UnityEngine;
using System.Collections;
using DeathBook.Util;
using DeathBook.Model;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text TimerText;

    

    void Update()
    {
        TimerText.text = Utils.GetTimeString(LevelManager.Instance.GameLevel.DayTime);
    }
}
