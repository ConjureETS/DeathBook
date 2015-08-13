using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DeathBook.Model;

public class SphereSR : MonoBehaviour
{
    public FriendshipLink LinkObj;
    public PersonTest PersonObj;
    public int NumPeople = 50;
	public int AvgNumFriends = 20;
	public float FriendshipLikeliness = 0.4f;
    public float SphereRadius = 1f;
    public float rotationSpeed = 0.7f;

    public float torqueForce = 50f;
    private bool dragging = false;
    private Vector3 delta = new Vector3();
    private Rigidbody rb;

    private PersonTest[] peopleNodes;
	//TODO private Friendship[] friendships;
    private GameObject[] nodes;

    void Awake()
    {
		LevelGenerator lGen = new LevelGenerator();
		Level lvl = lGen.GenerateLevel(NumPeople, AvgNumFriends, FriendshipLikeliness, SphereRadius);

        InstantiateNodes(lvl);
        AssignLinks(lvl);
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 screenMousePos = Input.mousePosition;

        screenMousePos.z = transform.position.z - Camera.main.transform.position.z;

        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(screenMousePos);

        // If the world position of the mouse is greater than the radius of the sphere, we are outside
        if (Mathf.Sqrt(worldMousePos.x * worldMousePos.x + worldMousePos.y * worldMousePos.y) > SphereRadius + 1f)
        {
            transform.Rotate(Vector3.one * Time.deltaTime * rotationSpeed);
        }
        
        //when right btn clicked, call the chnge rotation
        if (Input.GetMouseButtonDown(1))
        {
            dragging = true;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            dragging = false;
            delta = new Vector3();
        }
        
        if (dragging)
        {
            MoveSphere();
        }
    }

    void MoveSphere()
    {
        float deltaX = Input.GetAxis("Mouse X");
        float deltaY = Input.GetAxis("Mouse Y");
        if (deltaX == 0 && deltaY == 0)
        {
            delta = new Vector3();
            rb.angularVelocity *= 0.8f;
        }
        delta += new Vector3(deltaX, deltaY, 0);
        //rigidbody.AddTorque();
        rb.AddTorque(Vector3.down * delta.x * torqueForce * Time.deltaTime, ForceMode.Impulse);
        rb.AddTorque(Vector3.right * delta.y * torqueForce * Time.deltaTime, ForceMode.Impulse);
        Debug.Log(delta.x + ", " + delta.y);

        
    }

    private void InstantiateNodes(Level lvl)
    {
        /* Sphere uniform distribution using the spiral method with the golden angle
         * ~2.39996323 rad, the golden angle (the most irrational angle)
         * is used here to make sure that the sin and cos functions
         * dont end up drawing clusters of points and the spirals are way
         * less visible.
         */
        peopleNodes = new PersonTest[lvl.people.Count];

		int ctr = 0;
		foreach (Person p in lvl.people)
		{

			PersonTest pInst = Instantiate(PersonObj, p.initialPosition, Quaternion.identity) as PersonTest;

			pInst.transform.parent = this.transform;

			peopleNodes[ctr++] = pInst;
		}
    }

    private void AssignLinks(Level lvl)
    {
		foreach (Friendship f in lvl.friendships)
		{
			FriendshipLink link = Instantiate(LinkObj) as FriendshipLink;
			int id1 = f.friend1.id;
			int id2 = f.friend2.id;
			link.AttachToObjects(peopleNodes[id1].gameObject, peopleNodes[id2].gameObject);

            // Temporary stuff, for testing
            peopleNodes[id1].AddLink(link);
            peopleNodes[id2].AddLink(link);
		}
    }
}
