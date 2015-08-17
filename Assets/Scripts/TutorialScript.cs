using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DeathBook.Model;

public class TutorialScript : MonoBehaviour {

    public GameObject panel;
    public Text tutorialText;
    public Button btnNext;
    private Level lvl;


    void Start()
    {
        lvl = LevelManager.Instance.GameLevel;
        lvl.tutorialInt = 0;
		Time.timeScale = 0;
    }

    void Update()
    {
        btnNext.gameObject.SetActive(lvl.allowNext);

        if (lvl.tutorialInt == 0)
        {
            lvl.allowNext = true;
            tutorialText.text = "The Slaugthr servers are full!\nMark Zuckerberg hired you, Death, to kill off a few of his annoying users.\n\nCareful, or you might scare the rest away from Mark's website...";
        }
        else if (lvl.tutorialInt == 1)
        {
            tutorialText.text = "See how navigating works by holding the right mouse button and moving the network around.\n\nThen, hit Next!";
        }
        else if (lvl.tutorialInt == 2)
        {
            tutorialText.text = "Move around by left clicking on the users in the network!";
        }
        else if (lvl.tutorialInt == 3)
        {
            tutorialText.text = "Let's see what happens when you click on a friend's picture in the selected user's friend list.";
        }
        else if (lvl.tutorialInt == 4)
        {
            tutorialText.text = "Alright, time for our first victim.\nHold down the LEFT mouse button over a user until the X is complete. Beware! The user must be offline (darker picture) to be killed!";
            tutorialText.text += "\n\nIf the user is online, you can hold the mouse button until he goes offline.";
        }
        else if (lvl.tutorialInt == 5)
        {
            tutorialText.text = "Kill 2 more users to see how the color of the links and the borders changes.\n\nRemember, as users realize something is wrong with Slaugthr, the entire network will turn red!";
        }
        else if (lvl.tutorialInt == 6) 
        {
            tutorialText.text = "Finally, notice the big \"Global Awareness\" bar at the bottom right corner of the screen. When it reaches 100%, it means you have been noticed and you lose!";
            tutorialText.text += "\n\nHit next to start playing.";
            lvl.allowNext = true;
        }
        else
        {
            panel.SetActive(false);
        }
    
	
	}

    public void btnClick()
    {
		if (lvl.tutorialInt == 5)
		{
			Time.timeScale = 0f;
		}

        if (lvl.tutorialInt == 6)
        {
			Time.timeScale = 1;
			lvl.tutorialInt = -1;
            Application.LoadLevel("Gameplay");
			return;
        }

        lvl.tutorialInt++;

		if (lvl.tutorialInt == 1)
		{
			Time.timeScale = 1;
		}

        lvl.allowNext = false;
    }
	
}
