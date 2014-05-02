using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public float score;
	public bool retryable;
	private float health;
	private HealthSystem hs;
	private GameObject gameOverMan;

	public int maxEnemies=10;
	public int currentEnemies = 0;

	// Use this for initialization
	void Start () {
		retryable = false;
		score = 0;
		hs = (HealthSystem) GameObject.FindGameObjectWithTag ("Player").GetComponent<HealthSystem>();
		gameOverMan = GameObject.FindGameObjectWithTag ("GameOverLayer");
		gameOverMan.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (retryable) {
			if(Input.GetKeyDown(KeyCode.R)){
				Application.LoadLevel (Application.loadedLevel);
			}
		}

		if (Input.GetKeyDown (KeyCode.Escape)){
			Application.Quit();
		}
	}

	void LateUpdate() {
		health = hs.health;
	}

	public void addScore(float value){
		score += value;
	}

	public void showScore(){
		Vector3 position = Camera.main.transform.position;
		Camera.main.GetComponent<AudioListener> ().enabled = true;
		position.y += 200;
		gameOverMan.SetActive (true);
		gameOverMan.transform.position = position;
		TextMesh text = (TextMesh)GameObject.FindGameObjectWithTag ("ScoreText").GetComponent<TextMesh> ();
		text.text = "Score: " + score;
		retryable = true;
	}


	public bool canSpawn(GameObject go)
	{
		if (currentEnemies < maxEnemies){
			currentEnemies ++;
			return true;
		}
		return false;
	}

	void OnGUI() {
		Rect scoreRect = new Rect(10,10,1000,200);
		Rect healthRect = new Rect(10,80,1000,200);
		GUI.skin.label.fontSize = 40;
		GUI.Label(scoreRect, "Score: " + score);

		if (health <= 50)
			GUI.color = Color.red;
		else
			GUI.color = Color.white;

		GUI.Label(healthRect, "Health: " + health);
	}

}
