using UnityEngine;
using System.Collections;

public class GibExplosion : MonoBehaviour {


	public Vector2 explosionPoint;
	public Rect zone;
	public GameObject gib;
	public int numberOfGibs = 40;
	private bool exploding = false;
	public Sprite[] gibs;

	private float xLoc;
	private float yLoc;
	private float zLoc;
	private Vector3 spawnPoint;
	private Vector3 randRotation;
	private Sprite gibGfx;
	private Vector2 force;

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
			xLoc = Random.Range (zone.xMin,zone.xMax);
			yLoc = Random.Range (zone.yMin,zone.yMax);
			zLoc = 0;

			spawnPoint = new Vector3(xLoc, yLoc, zLoc);
			randRotation = Random.insideUnitSphere * 100;
			randRotation.x = 0.0f;
			randRotation.y = 0.0f;

			gib = (GameObject)Instantiate (gib, transform.position, Quaternion.Euler(randRotation));
			gib.rigidbody2D.angularVelocity = Random.Range (-1000, 1000);
			gibGfx = gibs[Random.Range (0, gibs.Length -1)];
			gib.GetComponent<SpriteRenderer>().sprite = gibGfx;

			force = Random.insideUnitCircle * 500;
			gib.rigidbody2D.AddForceAtPosition(force, gib.transform.position);
		}
	}
}
