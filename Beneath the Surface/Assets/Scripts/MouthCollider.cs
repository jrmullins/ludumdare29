using UnityEngine;
using System.Collections;

public class MouthCollider : MonoBehaviour {

	public GameObject gibs;
	private HarlemShake hs;
	private AnimationHandler ah;
	private GameObject thePlayer;
	private GameObject theEnemy;
	private AudioEngineThing aet;

	private float nextChain;
	public float chainRate=.25f;
	private ContactPoint2D contactPoint;

	private float dmg;
	private float health;
	private FishyBehavior fb;

	// Use this for initialization
	void Start () {
		hs = Camera.main.GetComponent<HarlemShake> ();
		thePlayer = GameObject.FindGameObjectWithTag ("Player");
		ah = thePlayer.GetComponent<AnimationHandler> ();
		aet = thePlayer.GetComponent<AudioEngineThing> ();
	}

	void OnCollisionEnter2D(Collision2D collision) {

		if(collision.gameObject.tag == "Enemy") {
			theEnemy = collision.gameObject;
			fb = theEnemy.GetComponent<FishyBehavior>();
			dmg = fb.bitePain;
			health = fb.healthSystemAmount;
			theEnemy.GetComponent<HealthSystem>().removeHealth(dmg);
			thePlayer.GetComponent<HealthSystem>().addHealth(health);
			hs.startShake();
			ah.playEat();
			contactPoint = collision.contacts[collision.contacts.Length-1];
			Instantiate(gibs, contactPoint.point, Quaternion.identity);
			if(Time.time > nextChain){
				nextChain = Time.time + chainRate;
				aet.chainPlay();
			}
		}
	}
}
