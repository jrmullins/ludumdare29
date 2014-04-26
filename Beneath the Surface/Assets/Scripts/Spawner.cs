using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {


	public float rate;
	public GameObject prefab;
	public GameController gameController;

	private float nextSpawn;
	private GameObject thePlayer;
	private GameObject theEnemy;

	// Use this for initialization
	void Start () {
		thePlayer = GameObject.FindGameObjectWithTag ("Player");
		gameController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
		//nextSpawn = Time.time + rate;
	}
	
	// Update is called once per frame
	void Update () {
//		Debug.Log ("thePlayer: " + thePlayer);
//		Debug.Log ("gameController.canSpawn(prefab): " + gameController.canSpawn(prefab));
//		Debug.Log ("Time.time > nextSpawn:" + (Time.time > nextSpawn));
//		Debug.Log (" ");
		if(thePlayer && Time.time > nextSpawn && gameController.canSpawn(prefab))
		{
			nextSpawn = Time.time + rate;
			//TODO randomize spawnloc within boundary
			//Vector2 spawnLoc;
			theEnemy = (GameObject)Instantiate(prefab, transform.position, Quaternion.identity);
		}
	}
}
