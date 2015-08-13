﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Center : MonoBehaviour
{

    //public float fov = Camera.main.fieldOfView;
    public Vector3 nextPosition;
    public float moveSpeed = 0;

    public FriendshipLink Link;
    public PersonTest Person;
    public int PointsAmount = 50;
    public float SphereRadius = 1f;
    public float rotationSpeed = 0.7f;

    public float torqueForce = 50f;
    private bool dragging = false;
    private Vector3 delta = new Vector3();
    private Rigidbody rb;
    private PersonTest[] people;

    
    private GameObject[] nodes;

    void Awake()
    {
        InstantiateNodes();
        AssignLinks();
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

        //camera zoom
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            Debug.Log("Pressed middle click.");
            //Camera.main.transform.position.z += (moveSpeed * Time.deltaTime);
            //transform.position = Vector3.Lerp(transform.position, nextPosition, Time.deltaTime * moveSpeed);
            //Camera.main.fieldOfView += Input.GetAxis("Mouse ScrollWheel") * 10f;
            //Camera.main.transform
        }
    }

    void FixedUpdate()
    {
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
        rb.AddTorque(Vector3.down * delta.x * torqueForce * Time.fixedDeltaTime, ForceMode.Impulse);
        rb.AddTorque(Vector3.right * delta.y * torqueForce * Time.fixedDeltaTime, ForceMode.Impulse);
        Debug.Log(delta.x + ", " + delta.y);

        
    }

    

    private void InstantiateNodes()
    {
        /* Sphere uniform distribution using the spiral method with the golden angle
         * ~2.39996323 rad, the golden angle (the most irrational angle)
         * is used here to make sure that the sin and cos functions
         * dont end up drawing clusters of points and the spirals are way
         * less visible.
         */
        people = new PersonTest[PointsAmount];

        float goldenAngle = Mathf.PI * (3 - Mathf.Sqrt(5));

        float zDistance = (2f / PointsAmount) * SphereRadius;
        float longitude = 0f;
        float z = SphereRadius;

        for (int i = 0; i < PointsAmount; i++)
        {
            float r = Mathf.Sqrt(SphereRadius * SphereRadius - z * z);

            float x = Mathf.Sin(longitude) * r;
            float y = Mathf.Cos(longitude) * r;

            PersonTest simon = Instantiate(Person, new Vector3(x, y, z), Quaternion.identity) as PersonTest;

            simon.transform.parent = this.transform;

            people[i] = simon;

            z -= zDistance;
            longitude += goldenAngle;
        }
    }

    private void AssignLinks()
    {
        for (int i = 0; i < people.Length / 4; i++)
        {
            FriendshipLink link = Instantiate(Link) as FriendshipLink;

            int destinationIndex = Random.Range(people.Length / 2, people.Length - 1);

            link.AttachToObjects(people[i].gameObject, people[destinationIndex].gameObject);

            // Temporary stuff, for testing
            people[i].AddLink(link);
            people[destinationIndex].AddLink(link);
        }
    }
}
