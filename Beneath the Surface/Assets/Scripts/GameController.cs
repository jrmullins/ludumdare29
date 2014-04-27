﻿using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public float score;
	private float health;
	private HealthSystem hs;

	public int maxEnemies=10;
	public int currentEnemies = 0;

//	public int maxFish = 2;
//	public int currentFish = 0;
//
//	public int maxBirds = 2;
//	public int currentBirds = 0;
//
//	public int maxDivers = 2;
//	public int currentDivers = 0;
//
//	public int maxOrcas = 2;
//	public int currentOrcas = 0;
//
//	public int maxCrabs = 2;
//	public int currentCrabs = 0;

	// Use this for initialization
	void Start () {
		score = 0;
		hs = (HealthSystem) GameObject.FindGameObjectWithTag ("Player").GetComponent<HealthSystem>();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void LateUpdate() {
		health = hs.health;
	}

	public void addScore(float value){
		score += value;
	}


	public bool canSpawn(GameObject go)
	{
		if (currentEnemies < maxEnemies){

			currentEnemies ++;

//			if(go.name == "Odd Fish"){
//				if (currentFish < maxFish) {
//					currentFish++;
//				}
//			}
//
//			if(go.name == "Bird"){
//				if (currentBirds < maxBirds) {
//					currentBirds++;
//				}
//			}
//
//			if(go.name == "Diver"){
//				if (currentDivers < maxDivers) {
//					currentDivers++;
//				}
//			}
//
//			if(go.name == "Orca"){
//				if (currentOrcas < maxOrcas) {
//					currentOrcas++;
//				}
//			}
//
//			if(go.name == "Crab"){
//				if (currentCrabs < maxCrabs ) {
//					currentFish++;
//				}
//			}
			return true;
		}

		return false;
	}

	void OnGUI() {
		//GUIContent gc = new GUIContent ();
		//gc
		Rect scoreRect = new Rect(10,10,100,20);
		Rect healthRect = new Rect(10,30,100,20);
		GUI.Label(scoreRect, "Score: " + score);
		GUI.Label(healthRect, "Hunger: " + health);


//		if(GUI.Button (Rect ( 400, 200, 150, 30), "Retry Level" ))
//		{
//			
//			Application.LoadLevel(Application.loadedLevel);
//			Time.timeScale = 1;
//			//AudioListener.volume = 100;
//			//Destroy(gameMusic);
//			
//		}

	}

}
