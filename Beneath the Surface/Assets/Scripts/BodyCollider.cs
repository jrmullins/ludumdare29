using UnityEngine;
using System.Collections;

public class BodyCollider : MonoBehaviour {

	public GameObject gibs;
	private HarlemShake hs;
	private AnimationHandler ah;
	private GameObject thePlayer;
	private GameObject theEnemy;
	private HealthSystem playerHealthSystem;
	FishyBehavior fb;
	
	// Use this for initialization
	void Start () {
		hs = Camera.main.GetComponent<HarlemShake> ();
		thePlayer = GameObject.FindGameObjectWithTag ("Player");
		ah = thePlayer.GetComponent<AnimationHandler> ();
		playerHealthSystem = thePlayer.GetComponent<HealthSystem> ();
	}
	

	void OnCollisionEnter2D(Collision2D collision) {

		if(collision.gameObject.tag == "Enemy") {
			theEnemy = collision.gameObject;
			fb = theEnemy.GetComponent<FishyBehavior>();
			float dmg = fb.damageToPlayer;
			float health = fb.healthSystemAmount;
			playerHealthSystem.removeHealth(dmg);
			hs.startShake();
			ah.playDamage();
			ContactPoint2D contactPoint = collision.contacts[collision.contacts.Length -1];
			Instantiate(gibs, contactPoint.point, Quaternion.identity);
			if (fb.attacking){
				fb.attacking = false;
				fb.returningToSpawnPoint = true;
			}
		}
	}
}
