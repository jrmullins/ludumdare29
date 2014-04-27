using UnityEngine;
using System.Collections;

public class MouthCollider : MonoBehaviour {

	public GameObject gibs;
	private HarlemShake hs;
	private AnimationHandler ah;
	private GameObject thePlayer;
	private GameObject theEnemy;

	// Use this for initialization
	void Start () {
		hs = Camera.main.GetComponent<HarlemShake> ();
		thePlayer = GameObject.FindGameObjectWithTag ("Player");
		ah = thePlayer.GetComponent<AnimationHandler> ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnCollisionEnter2D(Collision2D collision) {
		//Debug.Log ("Touched the mouth");
		if(collision.gameObject.tag == "Enemy") {

			theEnemy = collision.gameObject;
			float dmg = theEnemy.GetComponent<FishyBehavior>().eatDamage;
			float health = theEnemy.GetComponent<FishyBehavior>().healthSystemAmount;
			theEnemy.GetComponent<HealthSystem>().removeHealth(dmg);
			thePlayer.GetComponent<HealthSystem>().addHealth(health);
			hs.startShake();
			ah.playEat();
			ContactPoint2D contactPoint = collision.contacts[collision.contacts.Length-1];
			//Vector2 blah = this.gameObject.transform.position;
			Instantiate(gibs, contactPoint.point, Quaternion.identity);
		}
	}
}
