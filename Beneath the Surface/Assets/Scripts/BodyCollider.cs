using UnityEngine;
using System.Collections;

public class BodyCollider : MonoBehaviour {

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
	

	void OnCollisionEnter2D(Collision2D collision) {
		//Debug.Log ("Touched the body");
		if(collision.gameObject.tag == "Enemy") {
			//			playerAnimation.playDamage (healthSystemAmount);
			//			attacking = false;
			//			returningToSpawnPoint = true;


			theEnemy = collision.gameObject;
			float dmg = theEnemy.GetComponent<FishyBehavior>().damageToPlayer;
			float health = theEnemy.GetComponent<FishyBehavior>().healthSystemAmount;
			thePlayer.GetComponent<HealthSystem>().removeHealth(dmg);
			hs.startShake();
			ah.playDamage();
			ContactPoint2D contactPoint = collision.contacts[collision.contacts.Length -1];
			Instantiate(gibs, contactPoint.point, Quaternion.identity);
			FishyBehavior fb = theEnemy.GetComponent<FishyBehavior>();
			if (fb.attacking){
				fb.attacking = false;
				fb.returningToSpawnPoint = true;
			}
		}
	}
}
