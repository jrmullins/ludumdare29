using UnityEngine;
using System.Collections;

public class SharkMover : MonoBehaviour {

	public float swimSpeed = 60.0f;
	public float swimForce = 50.0f;
	public float verticalRotationSpeed;
	public bool isFlying = false;

	private float currentSpeed = 0.0f;
	public bool goingLeft;
	private Vector2 rotator;
	private Vector2 currentDirection;
	private float wantedAngle;
	private int buttonsBeingPressed = 0;

	private GameObject mouthLeft;
	private GameObject mouthRight;
	private GameObject body;

	private IRagePixel ragePixel;

	// Use this for initialization
	void Start () {
		currentSpeed = swimSpeed;
		rotator = Vector2.zero;
		ragePixel = GetComponent<RagePixelSprite> ();
		ragePixel.PlayNamedAnimation ("Idle", false);
		goingLeft = true;
		mouthLeft = GameObject.Find ("Mouth Left");
		mouthRight = GameObject.Find ("Mouth Right");
		body = GameObject.FindGameObjectWithTag ("Body");
		if (!body || !mouthLeft || !mouthRight)
			Debug.LogError ("MISSING MOUTH OR BODY");

		mouthLeft.SetActive (true);
		mouthRight.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		buttonsBeingPressed = 0;

		if (!isFlying) {
			if (upKeyCodes()){
				swimUp ();
			}
			if (leftKeyCodes()){
				swimLeft ();
			}
			if (downKeyCodes()){
				swimDown ();
			}
			if (rightKeyCodes()){
				swimRight ();
			}

			if (Input.GetKeyUp (KeyCode.W) || Input.GetKeyUp (KeyCode.UpArrow))
				wantedAngle = 0.0f;

			if (Input.GetKeyUp (KeyCode.S) || Input.GetKeyUp (KeyCode.DownArrow))
				wantedAngle = 0.0f;

			swimFaster ();
		}
		fixGravity ();

	}

	void FixedUpdate(){
		rotateShark ();
	}

	void swimUp() {
		wantedAngle = 90.0f;
		if(goingLeft)
			wantedAngle = 270.0f;
		swim (Vector2.up);
	}
	void swimDown() {
		wantedAngle =270.0f;
		if(goingLeft)
			wantedAngle = 90.0f;
		swim (-Vector2.up);
	}
	void swimLeft() {
		goingLeft = true;
		wantedAngle = 0.0f;
		mouthLeft.SetActive (true);
		mouthRight.SetActive (false);
		swim (-Vector2.right);
	}
	void swimRight() {
		goingLeft = false;
		wantedAngle = 0.0f;
		mouthLeft.SetActive (false);
		mouthRight.SetActive (true);
		swim (Vector2.right);
	}
	
	void swim(Vector2 direction){
		physicsSwim (direction);
	}

	void physicsSwim(Vector2 direction){
		transform.rigidbody2D.AddForce (direction * (currentSpeed * swimForce) * Time.deltaTime);
	}

	void fixGravity(){
		if(transform.position.y < 157 && rigidbody2D.gravityScale != 0){
			isFlying= false;
			rigidbody2D.gravityScale = 0;
			Debug.LogWarning("Had to fix gravity");
		}
	}

	void rotateShark()
	{
		float targetAngle;
		if (goingLeft) {
			ragePixel.SetHorizontalFlip(false);
		}
		else {
			ragePixel.SetHorizontalFlip(true);
		}

		float angle = Mathf.LerpAngle (transform.eulerAngles.z, wantedAngle, verticalRotationSpeed * Time.deltaTime);
		Vector3 rotationator = new Vector3(0, 0, angle);
		transform.eulerAngles = rotationator;
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
