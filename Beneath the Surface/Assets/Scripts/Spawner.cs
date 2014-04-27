using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {


	public float rate;
	public GameObject prefab;
	public GameController gameController;
	public float randomYRange = 15.0f;
	public float randomXRange = 15.0f;
	public float randomSpawnRange = 15.0f;

	private float nextSpawn;
	private float randomDelay;
	private GameObject thePlayer;
	private GameObject theEnemy;

	private Vector2 spawnLoc;

	// Use this for initialization
	void Start () {
		thePlayer = GameObject.FindGameObjectWithTag ("Player");
		gameController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
		randomDelay = Random.Range (0, 2);
		nextSpawn = Time.time + randomDelay;
	}

	void Update () {
		if(thePlayer && Time.time > nextSpawn && gameController.canSpawn(prefab))
		{
			randomDelay = Random.Range (0, randomSpawnRange);
			nextSpawn = Time.time + rate + randomDelay;
			spawnLoc = transform.position;
			spawnLoc.x += Random.Range(-randomXRange, randomXRange);
			spawnLoc.y += Random.Range(-randomYRange, randomYRange);
			theEnemy = (GameObject)Instantiate(prefab, spawnLoc, Quaternion.identity);
		}
	}	
}
