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

//	void OnTrigger2D(Collider2D other){
//		Debug.Log ("Ontrigger2d");
//		if(other.gameObject.tag == "Player") {
//			if (other.rigidbody2D.velocity.y > 0){
//				Debug.Log ("Going up");
//			}
//		}
//	}

	void OnTriggerEnter2D(Collider2D other)
	{
		GameObject actualObject = other.gameObject;
		if (other.gameObject.transform.parent)
			actualObject= other.gameObject.transform.parent.gameObject;

		//Debug.Log ("Trigger enter");
		if ( actualObject.tag == "Player") {
			GameObject go = Instantiate(Resources.Load("Splash")) as GameObject;
			go.transform.position = actualObject.transform.position;
			thePlayer.GetComponent<AudioEngineThing>().playSplash();
		}

	}

	void OnTriggerExit2D(Collider2D other) {
		GameObject actualObject = other.gameObject;
		if (other.gameObject.transform.parent)
			actualObject= other.gameObject.transform.parent.gameObject;

		if ( actualObject.tag == "Player") {
//			GameObject go = Instantiate(Resources.Load("Splash")) as GameObject;
//			go.transform.position = actualObject.transform.position;
			
			if(!m.isFlying && actualObject.rigidbody2D.velocity.y > 0) {
				m.isFlying= true;
				actualObject.rigidbody2D.gravityScale = gravityInAir;
			}

			if ( m.isFlying && 
			    actualObject.rigidbody2D.velocity.y < 0) {
				
				//Flying, Falling, in water
				
				actualObject.rigidbody2D.gravityScale = 0;
				m.isFlying= false;
			}
		}



	}
}
