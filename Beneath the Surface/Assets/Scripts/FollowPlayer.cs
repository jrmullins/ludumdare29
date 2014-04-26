using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

	public float followSpeed = 1.0f;
	public float zdistance;
	private GameObject thePlayer;
	private Vector3 moveTo;

	// Use this for initialization
	void Start () {
		thePlayer = GameObject.FindGameObjectWithTag ("Player");
	}

	void LateUpdate(){
		moveTo = thePlayer.transform.position;
		moveTo.x = 0;
		moveTo.z = zdistance;
		transform.position = Vector3.Lerp(transform.position, moveTo, Time.deltaTime * followSpeed);
	}
}
