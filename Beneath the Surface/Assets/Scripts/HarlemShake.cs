using UnityEngine;
using System.Collections;

public class HarlemShake : MonoBehaviour {
	public float shakeIntensity;
	public int shakeCount;
	public bool isShaking;

	public int shakesLeft;

	private Vector3 originalPosition;
	private Vector3 shakeVector;
	// Use this for initialization

	void Start () {
		isShaking = false;
		shakeVector = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		if(isShaking && shakesLeft > 0){
			shakesLeft --;
			if (shakesLeft % 2 == 0)
				shake ();
			else
				transform.position = originalPosition;
		}
		if (shakesLeft <= 0)
			isShaking = false;
	}
	

	void shake()
	{

		float xshake = Mathf.PerlinNoise (-1, 1) * shakeIntensity;
		float yshake = Mathf.PerlinNoise (-1, 1) * shakeIntensity; 
		float zshake = Mathf.PerlinNoise (-1, 1) * shakeIntensity;
		shakeVector.x = originalPosition.x + xshake;
		shakeVector.y = originalPosition.y + yshake;
		shakeVector.z = originalPosition.z;//Vector3 (originalPosition.x + xshake, originalPosition.y + yshake,originalPosition.z);
		//Debug.Log (shakeVector);
		transform.position = shakeVector;

	}

	public void startShake()
	{
		if(!isShaking){
			isShaking = true;
			shakesLeft = shakeCount;
			originalPosition = transform.position;
		}
	}
}
