using UnityEngine;
using System.Collections;
using DeathBook.Util;
using DeathBook.Model;
using UnityEngine.UI;

public class WatchArea : MonoBehaviour, IObserver
{
    public RatioProgression AwarenessBar;
    public Text Percentage;

	// Use this for initialization
	void Start ()
    {
        LevelManager.Instance.GameLevel.Subscribe(this);
        UpdateBar();
	}

    public void Notify()
    {
        UpdateBar();
    }

    private void UpdateBar()
    {
        AwarenessBar.SetCompletedRatio(LevelManager.Instance.GameLevel.Awareness);
        Percentage.text = (int)Mathf.Clamp((LevelManager.Instance.GameLevel.Awareness * 100f / 0.6f), 0f, 100f) + "%";
    }
}
