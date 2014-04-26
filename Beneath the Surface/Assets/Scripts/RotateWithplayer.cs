using UnityEngine;
using System.Collections;

public class RotateWithplayer : MonoBehaviour {

	GameObject thePlayer;
	SharkMover mover;
	// Use this for initialization
	void Start () {
		thePlayer = GameObject.FindGameObjectWithTag ("Player");
		mover = thePlayer.GetComponent<SharkMover> ();
	}
	
	// Update is called once per frame
	void Update () {
		//transform.localRotation = Quaternion.identity;
//		Vector2 currentPosition = transform.position;
//		if(mover.goingLeft){
//			currentPosition.x += -16;
//		} else {
//			currentPosition.x += 16;
//		}
//		transform.localPosition = currentPosition;
	}
}
