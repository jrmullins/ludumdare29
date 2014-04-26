using UnityEngine;
using System.Collections;

public class FishyBehavior : MonoBehaviour {

	public float fishSpeed;
	private float currentSpeed = 5.0f;

	public float distanceBeforeTurn = 2.0f;
	private Vector2 startPosition;
	private Vector2 nextTurnPoint;
	private bool goingLeft;

	// Use this for initialization
	void Start () {
		startPosition = transform.position;
		nextTurnPoint = transform.position;
		currentSpeed = fishSpeed;

		//TODO you can randomize these two calls
		nextTurnPoint.x = transform.position.x - distanceBeforeTurn;
		goingLeft = true;
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 directionVector = Vector2.zero;
		if (goingLeft)
			directionVector = - Vector2.right;
		else
			directionVector = -Vector2.right;

		turner ();
		transform.Translate (directionVector * currentSpeed * Time.deltaTime);
	}

	void turner() {
		float currentXPos = transform.position.x;
		//Debug.Log (currentXPos +  " is less than " + nextTurnPoint.x);
		if (goingLeft && currentXPos < nextTurnPoint.x){
			nextTurnPoint.x = transform.position.x + distanceBeforeTurn;
			transform.eulerAngles = new Vector3(0, 180.0f, 0);
			goingLeft = false;

			//Debug.Log ("RoTATE RIGHT");

		}

		if (!goingLeft && currentXPos > nextTurnPoint.x){
			goingLeft = true;
			nextTurnPoint.x = transform.position.x - distanceBeforeTurn;
			//Debug.Log ("RoTATE LEFT");
			transform.eulerAngles = new Vector3(0, 0.0f, 0);
		}

	}
}
