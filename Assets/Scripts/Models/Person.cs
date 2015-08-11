using UnityEngine;
using System.Collections;

public class Person 
{
	private string name;
	private Person[] friendList;
	private int friendCount; //lazy
	private int timeBetweenPosts; // f = 1/T;
	private int connectionTime;
	private int disconnectionTime;
	private int awarenessLevel;
	private bool alive;
	
	private int happiness;
	private bool connected;

	//private Node node;

	
	private bool isConnected(int time)
	{
		return disconnectionTime > time && time > connectionTime;
	}
	
	private int calculateWeight()
	{
		//friendCount * ____ + 1/timeBetweenPosts + }
		return 0;
	}
}
