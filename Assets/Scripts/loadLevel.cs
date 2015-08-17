using UnityEngine;
using System.Collections;

public class loadLevel : MonoBehaviour
{
    public int timeToWait = 2;
    // Use this for initialization

    void Start()
    {
        Invoke("NextScene", timeToWait);
    }

    void NextScene()
    {
        Application.LoadLevel("Tutoriel");
    }

}
