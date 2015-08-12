using UnityEngine;
using System.Collections;

public class Sphere : MonoBehaviour
{
    public GameObject SpherePrototype;
    public int PointsAmount = 50;
    public float SphereRadius = 1f;

    void Awake()
    {
        // First test (Orion Elenzil)
        /*
        for (int i = 0; i < PointsAmount; i++)
        {
            float theta = (360f / PointsAmount) * i;
            float phi = (Mathf.PI / 2 / PointsAmount) * i;

            float x = Mathf.Cos(Mathf.Sqrt(phi)) * Mathf.Cos(theta);
            float y = Mathf.Cos(Mathf.Sqrt(phi)) * Mathf.Sin(theta);
            float z = (UnityEngine.Random.value < 0.5f ? -1 : 1) * Mathf.Sin(Mathf.Sqrt(phi));
            Debug.Log(UnityEngine.Random.value);
            Instantiate(SpherePrototype, new Vector3(x, y, z), Quaternion.identity);
        }*/

        // Second test (default unit sphere random distribution)
        /*
        for (int i = 0; i < PointsAmount; i++)
        {
            float u = UnityEngine.Random.Range(-1f, 1f);
            float a = UnityEngine.Random.Range(0f, 2 * Mathf.PI);
            
            float x = Mathf.Sqrt(1 - u * u) * Mathf.Cos(a);
            float y = Mathf.Sqrt(1 - u * u) * Mathf.Sin(a);
            float z = u;

            Instantiate(SpherePrototype, new Vector3(x, y, z), Quaternion.identity);
        }*/

        // Third test (unit sphere semi-uniform distribution)
        for (int i = 0; i < PointsAmount; i++)
        {
            float u = (SphereRadius * 2 / PointsAmount) * i - SphereRadius;
            float a = UnityEngine.Random.Range(0f, 2 * Mathf.PI); // ((2 * Mathf.PI) / PointsAmount) * i;

            Debug.Log(a);

            float x = Mathf.Sqrt(SphereRadius * SphereRadius - u * u) * Mathf.Cos(a);
            float y = Mathf.Sqrt(SphereRadius * SphereRadius - u * u) * Mathf.Sin(a);
            float z = u;

            Instantiate(SpherePrototype, new Vector3(x, y, z), Quaternion.identity);
        }
    }
}
