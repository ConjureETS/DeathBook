using UnityEngine;
using System.Collections;

public class Sphere : MonoBehaviour
{
    public FriendshipLink Link;
    public GameObject SpherePrototype;
    public int PointsAmount = 50;
    public float SphereRadius = 1f;
    public float rotationSpeed = 0.7f;

    private GameObject[] nodes;

    void Awake()
    {
        InstantiateNodes();
        AssignLinks();
    }

    void Update()
    {
        transform.Rotate(Vector3.one * Time.deltaTime * rotationSpeed);
    }

    private void InstantiateNodes()
    {
        /* Sphere uniform distribution using the spiral method with the golden angle
         * ~2.39996323 rad, the golden angle (the most irrational angle)
         * is used here to make sure that the sin and cos functions
         * dont end up drawing clusters of points and the spirals are way
         * less visible.
         */
        nodes = new GameObject[PointsAmount];

        float goldenAngle = Mathf.PI * (3 - Mathf.Sqrt(5));

        float zDistance = (2f / PointsAmount) * SphereRadius;
        float longitude = 0f;
        float z = SphereRadius;

        for (int i = 0; i < PointsAmount; i++)
        {
            float r = Mathf.Sqrt(SphereRadius * SphereRadius - z * z);

            float x = Mathf.Sin(longitude) * r;
            float y = Mathf.Cos(longitude) * r;

            GameObject simon = Instantiate(SpherePrototype, new Vector3(x, y, z), Quaternion.identity) as GameObject;

            simon.transform.parent = this.transform;

            nodes[i] = simon;

            z -= zDistance;
            longitude += goldenAngle;
        }
    }

    private void AssignLinks()
    {
        for (int i = 0; i < nodes.Length / 4; i++)
        {
            FriendshipLink link = Instantiate(Link) as FriendshipLink;

            int destinationIndex = Random.Range(nodes.Length / 2, nodes.Length - 1);

            link.AttachToObjects(nodes[i], nodes[destinationIndex]);
        }
    }
}
