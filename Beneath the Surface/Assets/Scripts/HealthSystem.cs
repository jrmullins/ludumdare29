﻿using UnityEngine;
using System.Collections;

public class HealthSystem : MonoBehaviour {
	
	public float health = 100.0f;
	public float damageCooldownRate = 1.0f;
	public bool godMode = false;
	public bool hasHunger = false;

	public float hungerTicks = 20.0f;
	public float hungerCooldownRate = 1.0f;
	public GameObject gibs;

	private float nextDamage;
	private float nextHunger;
	private Blinker blinky;
	private bool dead;
	private bool isEnemy;
	private GameController gc;
	private FishyBehavior fishy;

	void Start() {
		blinky = GetComponent<Blinker> ();
		gc = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
		fishy = transform.GetComponent<FishyBehavior> ();
		dead = false;
		isEnemy = false;
		if (this.gameObject.tag == "Enemy")
			isEnemy = true;

	}

	void Update(){
		if(hasHunger && Time.time > nextHunger){
			nextHunger = Time.time + hungerCooldownRate;
			hungery();
		}
	}

	void LateUpdate(){
		if(!godMode && health <= 0){
			dead = true;
			GameObject go = Instantiate(Resources.Load("Gibs")) as GameObject;
			go.transform.position = transform.position;

			if(isEnemy)
				gc.addScore(fishy.scoreValue);


			if (isEnemy)
				gc.currentEnemies--;
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

	private void hungery() {
		health -= hungerTicks;
	}

	public bool isDead(){
		return dead;
	}
}
