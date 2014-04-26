using UnityEngine;
using System.Collections;

public class HealthSystem : MonoBehaviour {

	public float health = 100.0f;

	private Blinker blinky;
	private bool dead;

	void Start() {
		blinky = GetComponent<Blinker> ();
		dead = false;
	}

	void LateUpdate(){
		if(health <= 0)
			dead = true;
	}

	public void addHealth(float amount){
		health += amount;
		blinky.blinkColor = Color.green;
		blinky.startBlinking ();
	}

	public void removeHealth(float amount){
		health -= amount;
		blinky.blinkColor = Color.red;
		blinky.startBlinking ();
	}

	public bool isDead(){
		return dead;
	}
}
