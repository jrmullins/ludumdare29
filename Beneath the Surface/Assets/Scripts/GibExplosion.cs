using UnityEngine;
using System.Collections;

public class GibExplosion : MonoBehaviour {


	public Vector2 explosionPoint;
	public Rect zone;
	public GameObject gib;
	public int numberOfGibs = 40;
	private bool exploding = false;
	public Sprite[] gibs;

	// Use this for initialization
	void Start () {
		explosionPoint = transform.position;
		zone.width = 10;
		zone.height = 10;
		
	}

	void FixedUpdate() {
		if (!exploding && zone!=null && explosionPoint != null) {
			exploding = true;
			spawnGibs();
		}
	}

	void spawnGibs() {
		Random.seed = (int)System.DateTime.Now.Ticks;
		for(int i = 0; i < numberOfGibs; i++) {
			float xLoc = Random.Range (zone.xMin,zone.xMax);
			float yLoc = Random.Range (zone.yMin,zone.yMax);
			float zLoc = 0;

			Vector3 spawnPoint = new Vector3(xLoc, yLoc, zLoc);
			Vector3 randRotation = Random.insideUnitSphere * 100;
			randRotation.x = 0.0f;
			randRotation.y = 0.0f;
			gib = (GameObject)Instantiate (gib, transform.position, Quaternion.Euler(randRotation));
			gib.rigidbody2D.angularVelocity = Random.Range (-1000, 1000);
			Sprite gibGfx = gibs[Random.Range (0, gibs.Length -1)];
			gib.GetComponent<SpriteRenderer>().sprite = gibGfx;

			Vector2 force = Random.insideUnitCircle * 500;
			gib.rigidbody2D.AddForceAtPosition(force, gib.transform.position);
		}
	}
}
