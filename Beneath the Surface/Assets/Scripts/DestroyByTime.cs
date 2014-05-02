using UnityEngine;
using System.Collections;

public class DestroyByTime : MonoBehaviour {


	public float someTime = 10.0f;

	// Use this for initialization
	void Start () {
		Destroy (this.gameObject, someTime);
	}
}
