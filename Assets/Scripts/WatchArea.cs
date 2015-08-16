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
        float adjustedRatio = Mathf.Clamp((LevelManager.Instance.GameLevel.Awareness / 0.6f), 0f, 1f);

        AwarenessBar.SetCompletedRatio(adjustedRatio);
        Percentage.text = (int)(adjustedRatio * 100f) + "%";
    }
}
