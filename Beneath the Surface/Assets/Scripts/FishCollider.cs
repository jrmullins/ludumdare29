using UnityEngine;
using System.Collections;

public class FishCollider : MonoBehaviour {

	public float healthSystemAmount;
	private GameObject thePlayer;

	void Start() {
		thePlayer = GameObject.FindGameObjectWithTag ("Player");
	}
	

	//TODO This could go in fishy behavior
	void OnCollisionEnter2D(Collision2D collision)
	{
		AnimationHandler ah = thePlayer.GetComponent<AnimationHandler> ();
		ah.playEat (healthSystemAmount);
		Destroy (this.gameObject);
	}
}
