using UnityEngine;
using System.Collections;

public class WhiteCapBehavior : MonoBehaviour {

	public float gravityInAir;
	public float yOffset;
	public GameObject thePlayer;
	SharkMover m;
	private GameObject actualObject;
	private GameObject go;

	// Use this for initialization
	void Start () {
		thePlayer = GameObject.FindGameObjectWithTag ("Player");
		m = thePlayer.GetComponent<SharkMover> ();
		m.isFlying = false;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		actualObject = other.gameObject;
		if (other.gameObject.transform.parent)
			actualObject= other.gameObject.transform.parent.gameObject;

		if ( actualObject.tag == "Player") {
			go = Instantiate(Resources.Load("Splash")) as GameObject;
			go.transform.position = actualObject.transform.position;
			thePlayer.GetComponent<AudioEngineThing>().playSplash();
		}

	}

	void OnTriggerExit2D(Collider2D other) {
		GameObject actualObject = other.gameObject;
		if (other.gameObject.transform.parent)
			actualObject= other.gameObject.transform.parent.gameObject;

		if ( actualObject.tag == "Player") {	
			if(!m.isFlying && actualObject.rigidbody2D.velocity.y > 0) {
				m.isFlying= true;
				actualObject.rigidbody2D.gravityScale = gravityInAir;
			}

			if ( m.isFlying && 
			    actualObject.rigidbody2D.velocity.y < 0) {
				actualObject.rigidbody2D.gravityScale = 0;
				m.isFlying= false;
			}
		}
	}
}
