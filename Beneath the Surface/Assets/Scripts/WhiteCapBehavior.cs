using UnityEngine;
using System.Collections;

public class WhiteCapBehavior : MonoBehaviour {

	public float gravityInAir;
	public float yOffset;
	public GameObject thePlayer;
	SharkMover m;

	// Use this for initialization
	void Start () {
		thePlayer = GameObject.FindGameObjectWithTag ("Player");
		m = thePlayer.GetComponent<SharkMover> ();
		m.isFlying = false;
	}
	
	// Update is called once per frame
	void LateUpdate () {
//		if (thePlayer.transform.position.y < transform.position.y) {
//			m.isFlying= false;
//			thePlayer.rigidbody2D.gravityScale = 0;
//		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player") {

			if(!m.isFlying && other.rigidbody2D.velocity.y > 10) {
				m.isFlying= true;
				other.rigidbody2D.gravityScale = gravityInAir;
			} 
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			if ( m.isFlying && 
			           //other.rigidbody2D.velocity.y < 0 && 
			           other.gameObject.transform.position.y < this.transform.position.y + yOffset) {
				
				//Flying, Falling, in water
				
				other.rigidbody2D.gravityScale = 0;
				m.isFlying= false;
			}
		}

	}
}
