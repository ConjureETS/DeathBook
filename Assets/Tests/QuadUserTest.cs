using UnityEngine;
using System.Collections;

public class QuadUserTest : MonoBehaviour {

    public float awareness;
    public Color startingColor;
    public Color middleColor;
    public Color endColor;

    public bool isAlive;
    public bool isOnline;
    public GameObject internQuad;
    public GameObject xQuad;


	// Use this for initialization
	void Start () {
        awareness = 0;
        startingColor = Color.green;
        middleColor = Color.yellow;
        endColor = Color.red;

        xQuad.GetComponent<Renderer>().enabled = false;
        setPicOnline(internQuad);
        isAlive = true;
        isOnline = true;
	
	}
	
	// Update is called once per frame
	void Update () {
 
        //awareness += 1 / 5f * Time.deltaTime;  //awareness from 0 to 1 in 5 seconds
        //set color for awareness
        if (awareness<0.5)
            gameObject.GetComponent<Renderer>().material.color = Color.Lerp(startingColor, middleColor, awareness*2);
        else if (awareness >= 0.5)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.Lerp(middleColor, endColor, awareness * 2 - 1);
            //isAlive = false;
        }

        //set greyed out for offline
        if (isOnline)
            setPicOnline(internQuad);
        else
            setPicOffline(internQuad);
       
        //If dead -> set offline until all friends are aware, then add a big red X to profile pic
        if(isAlive)
            xQuad.GetComponent<Renderer>().enabled = false;
        else
        {
            isOnline = false;
            //TODO: check if all friends are aware
            xQuad.GetComponent<Renderer>().enabled = true;
            gameObject.GetComponent<Renderer>().material.color = new Color32(50, 50, 50, 1);
        }


        //TODO: (optional) effect for frequent poster users
        	
	}


    void setPicOnline(GameObject quad)
    {
        quad.GetComponent<Renderer>().material.color = new Color32(255, 255, 255, 1);
    }

    void setPicOffline(GameObject quad)
    {
        quad.GetComponent<Renderer>().material.color = new Color32(80, 80, 80, 1);
    }


}
