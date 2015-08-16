using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DeathBook.Model;

public class NetworkingSphere : MonoBehaviour
{
    public FriendshipLink LinkObj;
    public PersonNode PersonObj;
    public int NumPeople = 50;
    public int AvgNumFriends = 20;
    public float FriendshipLikeliness = 0.4f;
    public float SphereRadius = 1f;
    public float rotationSpeed = 0.7f;

    public float torqueForce = 50f;

    public PersonDetailsPanel DetailsPanel;

    private bool dragging = false;
    private Vector3 delta = new Vector3();
    private Rigidbody rb;

    private PersonNode[] peopleNodes;
    //TODO private Friendship[] friendships;

    private PersonNode _selectedNode;

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
        //TEMPORARY QUICK FIX: Even though we are never moving the sphere, it starts moving as soon as it stops rotating
        transform.position = Vector3.zero;

        Vector3 screenMousePos = Input.mousePosition;

        screenMousePos.z = transform.position.z - Camera.main.transform.position.z;

        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(screenMousePos);

        // If the world position of the mouse is greater than the radius of the sphere, we are outside
        if (Mathf.Sqrt(worldMousePos.x * worldMousePos.x + worldMousePos.y * worldMousePos.y) > SphereRadius + 1f)
        {
            transform.Rotate(Vector3.one * Time.deltaTime * rotationSpeed);
        }

        //when right btn clicked, call MoveSphere
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

        //scroll
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
           // if (Camera.main.ScreenToViewportPoint(Input.mousePosition) < new Vector3(1,1,1))
            if (Camera.main.ScreenToViewportPoint(Input.mousePosition).x < 1)
            {
                Camera.main.fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * 10f;
            }
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

        rb.AddTorque(Vector3.down * delta.x * torqueForce * Time.deltaTime, ForceMode.Impulse);
        rb.AddTorque(Vector3.right * delta.y * torqueForce * Time.deltaTime, ForceMode.Impulse);
    }

    private void InstantiateNodes(Level lvl)
    {
        peopleNodes = new PersonNode[lvl.people.Count];

        for (int i = 0; i < lvl.people.Count; i++)
        {
            Person person = lvl.people[i];

            PersonNode pInst = Instantiate(PersonObj, person.initialPosition, Quaternion.identity) as PersonNode;

            pInst.OnClicked += OnNodeClicked;

            pInst.Model = person;
            pInst.transform.parent = this.transform;

            peopleNodes[i] = pInst;
        }
    }

    private void OnNodeClicked(PersonNode node)
    {
        if (_selectedNode != null)
        {
            _selectedNode.Select(false);
        }

        DetailsPanel.SetNode(node);
        node.Select(true);

        _selectedNode = node;

        // Testing to see how it looks and feels like
        FocusOnNode(node);
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

    public void FocusOnNode(PersonNode node)
    {
        StopCoroutine("RotateTowardsNodeCoroutine");
        StartCoroutine("RotateTowardsNodeCoroutine", node);

        /*
        Debug.Log(node.transform.eulerAngles);

        float xangle = (Mathf.Atan2(finalPos.z, finalPos.y) - Mathf.Atan2(initialPos.z, initialPos.y)) * Mathf.Rad2Deg;

        Debug.Log(node.transform.position);
        transform.Rotate(new Vector3(90, 0, 0));
        //transform.rotation = transform.rotation * Quaternion.AngleAxis(xangle, Vector3.right);
        Debug.Log(xangle);
        float yAngle = (Mathf.Atan2(finalPos.x, finalPos.z) - Mathf.Atan2(node.transform.position.x, node.transform.position.z)) * Mathf.Rad2Deg;
        Debug.Log(yAngle);
        //transform.Rotate(new Vector3(xangle, yAngle, 0));

        //float zAngle = (Mathf.Atan2(finalPos.y, finalPos.x) - Mathf.Atan2(initialPos.y, initialPos.x)) * Mathf.Rad2Deg;

        //transform.rotation = transform.rotation * Quaternion.AngleAxis(xangle, Vector3.right) * Quaternion.AngleAxis(yAngle, Vector3.up) * Quaternion.AngleAxis(zAngle, Vector3.forward);
        */
    }

    private IEnumerator RotateTowardsNodeCoroutine(PersonNode node)
    {
        //Vector3 finalPos = new Vector3(0f, 0f, -SphereRadius);

        Quaternion initialRot = transform.localRotation;


        transform.localRotation = Quaternion.identity; // Temporary hack for the game jam


        Vector3 nodePos = node.transform.position;

        Vector3 longDir = nodePos;
        longDir.y = 0;

        float longitude = Vector3.Angle(-Vector3.forward, longDir) * (longDir.x < 0 ? -1 : 1);
        float latitude = Mathf.Asin(nodePos.normalized.y) * Mathf.Rad2Deg;

        Quaternion finalRot = Quaternion.AngleAxis(-latitude, Vector3.right) * Quaternion.AngleAxis(longitude, Vector3.up);

        float ratio = 0f;

        while (ratio < 1f)
        {
            ratio += Time.deltaTime / 1.5f;

            transform.localRotation = Quaternion.Lerp(initialRot, finalRot, Mathf.SmoothStep(0f, 1f, ratio));

            yield return null;
        }
    }
}
