using UnityEngine;
using System.Collections;

public class FishCollider : MonoBehaviour {

	public float healthSystemAmount;
	private GameObject thePlayer;

	void Start() {
		thePlayer = GameObject.FindGameObjectWithTag ("Player");
	}
	

	//TODO This could go in fishy behavior

}
