using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DeathBook.Model;

public class TutorialScript : MonoBehaviour {

    //public int tutorialInt = 0;
    public GameObject panel;
    public Text tutorialText;
    public Button btnNext;
    private Level lvl;


	// Use this for initialization
    void Start()
    {
        lvl = LevelManager.Instance.GameLevel;
    }

    void Update() {

        
        if (lvl.tutorialInt == 0)
        {
            Time.timeScale = 0;
            tutorialText.text = "The facebook servers are full!\nMark Zuckerberg hired you, Death, to kill off a few of his users.\nCareful, or you might scare them away from Mark's website...";
            //btnNext.onClick

        }
        else if (lvl.tutorialInt == 1)
        {
            tutorialText.text = "See how navigating works by holding the right mouse button and moving the sphere.\n\nThen, hit Next!";
        }
        else if (lvl.tutorialInt == 2)
        {
            tutorialText.text = "Move around by clicking on the users in the network!";
        }
        else if (lvl.tutorialInt == 3)
        {
            tutorialText.text = "Let's see what happens when you click on a user's friend list.";
        }
        else if (lvl.tutorialInt == 4)
        {
            tutorialText.text = "Alright, time for our first victim.\nHold the right mouse button over a user until the X appears completely.";
        }
        else
        {
            panel.SetActive(false);
        }
    
	
	}

    public void btnClick()
    {
        lvl.tutorialInt++;
        Debug.Log(lvl.tutorialInt + ", aasfasf");
        Time.timeScale = 1;
        tutorialText.text = "eee";
        //panel.transform.Translate(Vector3.right * 100);
    }
	
}
