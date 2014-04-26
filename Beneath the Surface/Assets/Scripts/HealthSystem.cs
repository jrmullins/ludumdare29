using UnityEngine;
using System.Collections;

public class HealthSystem : MonoBehaviour {

	public float health = 100.0f;
	public float damageCooldownRate = 1.0f;
	public bool godMode = false;

	private float nextDamage;
	private Blinker blinky;
	private bool dead;

	void Start() {
		blinky = GetComponent<Blinker> ();
		dead = false;

	}

	void LateUpdate(){
		if(!godMode && health <= 0){
			dead = true;
			Destroy(this.gameObject);
		}
	}

	public void addHealth(float amount){
		if (Time.time > nextDamage){
			nextDamage = Time.time + damageCooldownRate;
			health += amount;
			blinky.blinkColor = Color.green;
			blinky.startBlinking ();
		}
	}

	public void removeHealth(float amount){
		if (Time.time > nextDamage){
			nextDamage = Time.time + damageCooldownRate;
			health -= amount;
			blinky.blinkColor = Color.red;
			blinky.startBlinking ();
		}
	}

	public bool isDead(){
		return dead;
	}
}
