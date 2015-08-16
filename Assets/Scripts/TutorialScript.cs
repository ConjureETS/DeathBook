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
    }

    void Update() {


        btnNext.enabled = lvl.allowNext;
        
        
        

        if (lvl.tutorialInt == 0)
        {
            Time.timeScale = 0;
            lvl.allowNext = true;
            tutorialText.text = "The facebook servers are full!\nMark Zuckerberg hired you, Death, to kill off a few of his users.\n\nCareful, or you might scare them away from Mark's website...";
        }
        else if (lvl.tutorialInt == 1)
        {
            tutorialText.text = "See how navigating works by holding the right mouse button and moving the sphere.\n\nThen, hit Next!";
        }
        else if (lvl.tutorialInt == 2)
        {
            tutorialText.text = "Move around by left clicking on the users in the network!";
        }
        else if (lvl.tutorialInt == 3)
        {
            tutorialText.text = "Let's see what happens when you click on a user's friend list.";
        }
        else if (lvl.tutorialInt == 4)
        {
            tutorialText.text = "Alright, time for our first victim.\nHold the LEFT mouse button over a user until the X is complete.\nBeware! The user must be offline to be killed!";
            tutorialText.text += "\n\nYou can hold the mouse button until the user goes offline.";
        }
        else if (lvl.tutorialInt == 5)
        {
            tutorialText.text = "Kill many users to see how the color changes\n\nRemember, as users realize something is wrong with facebook,\nthe entire network will turn red!\n\nHaveFun!";
            lvl.allowNext = true;
        }
        else
        {
            panel.SetActive(false);
        }
    
	
	}

    public void btnClick()
    {
        
        lvl.tutorialInt++;
        //Debug.Log(lvl.tutorialInt + ", aasfasf");
        Time.timeScale = 1;
        tutorialText.text = "eee";
        lvl.allowNext = false;
        //panel.transform.Translate(Vector3.right * 100);
    }
	
}
