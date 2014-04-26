using UnityEngine;
using System.Collections;

public class SharkMover : MonoBehaviour {

	public float swimSpeed = 60.0f;
	public float verticalRotationSpeed;


	private float currentSpeed = 0.0f;
	private bool goingLeft;
	private Vector2 rotator;
	private Vector2 currentDirection;
	private float wantedAngle;

	private IRagePixel ragePixel;

	// Use this for initialization
	void Start () {
		currentSpeed = swimSpeed;
		rotator = Vector2.zero;
		ragePixel = GetComponent<RagePixelSprite> ();
		ragePixel.PlayNamedAnimation ("Idle", false);
	}
	
	// Update is called once per frame
	void Update () {

		if (upKeyCodes()){
			swimUp ();
		}
		else if (leftKeyCodes()){
			swimLeft ();
		}
		else if (downKeyCodes()){
			swimDown ();
		}
		else if (rightKeyCodes()){
			swimRight ();
		}

		//TODO Clean this up
		if (Input.GetKeyUp (KeyCode.W) || Input.GetKeyUp (KeyCode.UpArrow))
			wantedAngle = 0.0f;

		if (Input.GetKeyUp (KeyCode.S) || Input.GetKeyUp (KeyCode.DownArrow))
			wantedAngle = 0.0f;

		swimFaster ();

	}

	void FixedUpdate()
	{
		rotateShark ();
	}

	void swimUp() {
		wantedAngle = 270.0f;
		swim (Vector2.up);
	}
	void swimDown() {
		wantedAngle =90.0f;
		swim (-Vector2.up);
	}
	void swimLeft() {
		goingLeft = true;
		wantedAngle = 0.0f;
		swim (-Vector2.right);
	}
	void swimRight() {
		goingLeft = false;
		wantedAngle = 0.0f;
		swim (Vector2.right);
	}
	
	void swim(Vector2 direction)
	{
		transform.Translate (-Vector2.right * currentSpeed * Time.deltaTime);
	}

	void physicsSwim(Vector2 direction)
	{
		currentDirection = direction;

		transform.rigidbody2D.AddForce (direction * (currentSpeed * 10) * Time.deltaTime);

	}

	void rotateShark()
	{
		float targetAngle;

		if (goingLeft) {
			rotator.y = 0.0f;
		}
		else {
			rotator.y = 180.0f;
		}

		float angle = Mathf.LerpAngle (transform.eulerAngles.z, wantedAngle, verticalRotationSpeed * Time.deltaTime);
		//Debug.Log ("Current rotation: " + transform.eulerAngles.z + " Wanted Angle: " + wantedAngle + " Calculated Angle: " + angle);
		transform.eulerAngles = new Vector3(0, rotator.y, angle);
	}

	void swimFaster(){
		if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
			currentSpeed = swimSpeed * 2;
		
		if(Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
			currentSpeed = swimSpeed;
	}


	bool upKeyCodes() {
		return Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.UpArrow);
	}
	
	bool downKeyCodes() {
		return Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.DownArrow);
	}
	
	bool rightKeyCodes() {
		return Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow);
	}
	
	bool leftKeyCodes() {
		return Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow);
	}
}
