using UnityEngine;
using System.Collections;

public class BoundaryBehavior : MonoBehaviour {

	Collider2D myCollider;
	float minX;
	float maxX;
	float minY;
	float maxY;
	float xClamp;
	float yClamp;
	GameObject thing;

	// Use this for initialization
	void Start () {
		myCollider = GetComponent<Collider2D> ();
		minX = myCollider.collider.bounds.center.x - (myCollider.collider.bounds.min.x / 2);
		maxX = myCollider.collider.bounds.center.x + (myCollider.collider.bounds.min.x / 2);

		minY = myCollider.collider.bounds.center.y - (myCollider.collider.bounds.min.y / 2);
		minX = myCollider.collider.bounds.center.y + (myCollider.collider.bounds.min.y / 2);
	}

	void OnCollisionExit2D(Collision2D coll)
	{
		thing = coll.gameObject;
		xClamp = Mathf.Clamp (thing.transform.position.x, minX, maxX);
		yClamp = Mathf.Clamp (thing.transform.position.y, minY, maxY);
		float zVal = thing.transform.position.z;
		Vector3 blah = new Vector3(xClamp, yClamp, zVal);
		thing.transform.position = blah;
	}
}
