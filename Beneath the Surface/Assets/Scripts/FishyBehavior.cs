using UnityEngine;
using System.Collections;

public class FishyBehavior : MonoBehaviour {

	public float scoreValue=10.0f;
	public float fishSpeed;
	public float patrolRange = 2.0f;
	
	public int bitePain;
	public float damageToPlayer;
	public float healthSystemAmount;

	private float currentSpeed = 5.0f;
	private Vector2 startPosition;
	private Vector2 nextTurnPoint;
	private bool goingLeft;
	private GameObject thePlayer;
	private HealthSystem healthSystem;
	private AnimationHandler playerAnimation;


	//Aggressiveness
	public bool aggressive = false;
	public float aggroRange = 5;
	public float attackTime = 5;
	public float attackSwimSpeed = 1.0f;
	private float stopAttackTime;
	public bool attacking = false;
	private float distanceToPlayer;
	public bool returningToSpawnPoint=false;

	// Use this for initialization
	void Start () {
		startPosition = transform.position;
		nextTurnPoint = transform.position;
		attacking = false;
		returningToSpawnPoint = false;
		currentSpeed = fishSpeed;
		thePlayer = GameObject.FindGameObjectWithTag ("Player");
		healthSystem = GetComponent<HealthSystem> ();
		playerAnimation = thePlayer.GetComponent<AnimationHandler> ();

		randomDirection ();
	}
	
	// Update is called once per frame
	void Update () {
		if(thePlayer) {
			if (aggressive)
				checkAggression ();

			if(attacking){
				attack();
			} else if (returningToSpawnPoint) {
				returnToSpawnPoint ();
			} else if (!isFalling()){
				patrol ();
			}
		}

		//Debug.Log ("Attacking: " + attacking + " Returning: " + returningToSpawnPoint);
	}

	void patrol(){
		Vector2 directionVector = Vector2.zero;
		if (goingLeft)
			directionVector = -Vector2.right;
		else
			directionVector = -Vector2.right;
		turner ();
		transform.Translate (directionVector * currentSpeed * Time.deltaTime);
	}

	bool isFalling(){
		if(transform.rigidbody2D)
			return (transform.rigidbody2D.velocity.y < 0);
		else
			return false;
	}

	void attack(){
		//Debug.Log ("ATTACKING!");
		float verticalRotationSpeed = 10.0f;
		float wantedAngle = Vector2.Angle (transform.position, thePlayer.transform.position);
		float angle = Mathf.LerpAngle (transform.eulerAngles.z, wantedAngle, verticalRotationSpeed * Time.deltaTime);
		//Debug.Log ("Current rotation: " + transform.eulerAngles.z + " Wanted Angle: " + wantedAngle + " Calculated Angle: " + angle);
		transform.eulerAngles = new Vector3(0, transform.rotation.y, angle);
		transform.position = Vector2.MoveTowards (transform.position, thePlayer.transform.position, attackSwimSpeed);
	}

	void returnToSpawnPoint(){
		float distanceToSpawnPoint = Vector2.Distance (transform.position, startPosition);
		transform.position = Vector2.MoveTowards (transform.position, startPosition, attackSwimSpeed/2);
		if(distanceToSpawnPoint <= 0.1)
		{
			attacking = false;
			returningToSpawnPoint = false;
			randomDirection();
		}
	}

	void checkAggression(){
		distanceToPlayer = Vector2.Distance(transform.position, thePlayer.transform.position);
		//Debug.Log ("Distance to player is " + distanceToPlayer);
		//Give up attacking
		if(attacking && Time.time > stopAttackTime){
			attacking = false;
			returningToSpawnPoint = true;
		}

		if(!attacking && !returningToSpawnPoint){

			if(goingLeft && thePlayer.transform.position.x < transform.position.x) {
				if(distanceToPlayer <= aggroRange){
					if(!attacking)
						stopAttackTime = Time.time + attackTime;
					attacking = true;
				}
			}
			if(!goingLeft && thePlayer.transform.position.x > transform.position.x) {
				if(distanceToPlayer <= aggroRange){
					if(!attacking)
						stopAttackTime = Time.time + attackTime;
					attacking = true;
				}
			}
		}
	}

	void turner() {
		float currentXPos = transform.position.x;
		//Debug.Log (currentXPos +  " is less than " + nextTurnPoint.x);
		if (goingLeft && currentXPos < nextTurnPoint.x){
			nextTurnPoint.x = transform.position.x + patrolRange;
			transform.eulerAngles = new Vector3(0, 180.0f, 0);
			goingLeft = false;

			//Debug.Log ("RoTATE RIGHT");

		}

		if (!goingLeft && currentXPos > nextTurnPoint.x){
			goingLeft = true;
			nextTurnPoint.x = transform.position.x - patrolRange;
			//Debug.Log ("RoTATE LEFT");
			transform.eulerAngles = new Vector3(0, 0.0f, 0);
		}

	}

	void randomDirection(){
		float randomValue = Random.value;
		int newTarget = Mathf.RoundToInt(randomValue);
		if (newTarget == 0)
		{
			//Debug.Log ("Chose left");
			goingLeft = true;
			nextTurnPoint.x = transform.position.x - patrolRange;
			transform.eulerAngles = new Vector3(0, 0.0f, 0);
		} else {
			//Debug.Log ("Chose right");
			goingLeft = false;
			nextTurnPoint.x = transform.position.x + patrolRange;
			transform.eulerAngles = new Vector3(0, 180.0f, 0);
		}
	}

	//	void OnCollisionEnter2D(Collision2D collision)
	//	{
	//		Debug.Log ("collision.gameObject.name: " + collision.gameObject.name);
	//		Debug.Log ("collision.gameObject.tag: " + collision.gameObject.tag);
	//		if (collision.gameObject.tag == "Mouth") {
	//			HarlemShake hs = Camera.main.GetComponent<HarlemShake>();
	//			hs.startShake();
	//			if (!attacking) {
	//				playerAnimation.playEat (healthSystemAmount);
	//				healthSystem.removeHealth(eatDamage);
	//			}
	//		}
	//		
	//		if(collision.gameObject.tag == "Player") {
	//			playerAnimation.playDamage (healthSystemAmount);
	//			attacking = false;
	//			returningToSpawnPoint = true;
	//		}
	//		
	//	}

}
