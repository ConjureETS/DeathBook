using UnityEngine;
using System.Collections;

public class TestSphereSR : MonoBehaviour {

	public GameObject cube;
	public GameObject sphere1, sphere2;
	public float hSpeed;
	public float vSpeed;
	public float speed;
	public float spawnTime;
	private float lastTime = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float dt = Time.deltaTime;

		lastTime -= dt;
		if (lastTime < 0)
		{
			Spawn();
			lastTime += spawnTime;
		}

		this.transform.Rotate(Vector3.up, vSpeed * speed * dt);
		sphere1.transform.Rotate(Vector3.forward, hSpeed * speed * dt);
	}

	private void Spawn()
	{
		Instantiate(cube, sphere2.transform.position, Quaternion.identity);
	}
}
