using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DeathBook.Model;

public class CollectedSoulsPanel : MonoBehaviour
{
    public Text CollectedSouls;

	// Update is called once per frame
	void Update ()
    {
        CollectedSouls.text = LevelManager.Instance.GameLevel.NumDead.ToString();
	}
}
