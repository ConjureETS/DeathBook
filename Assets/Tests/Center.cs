using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Center : MonoBehaviour
{
    public FriendshipLink Link;
    public PersonTest Person;
    public int PointsAmount = 50;
    public float SphereRadius = 1f;
    public float rotationSpeed = 0.7f;

    private PersonTest[] people;

    void Awake()
    {
        InstantiateNodes();
        AssignLinks();
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
