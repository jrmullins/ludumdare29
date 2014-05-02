using UnityEngine;
using System.Collections;

public class GameStartController : MonoBehaviour {

	GameObject title;
	GameObject instructions;

	bool instructionsShown = false;

	Rect rect;
	Rect area;

	float buttonWidth = 200f;
	float buttonHeight = 50;
	float spacing = 15;

	// Use this for initialization
	void Start () {
		title = GameObject.FindGameObjectWithTag ("Title");
		instructions = GameObject.FindGameObjectWithTag ("Instructions");
		instructions.SetActive (false);
		area = new Rect (Screen.width / 2 - buttonWidth/2, Screen.height / 2 - buttonHeight, buttonWidth, 400);
	}

	void OnGUI(){
		GUILayout.BeginArea (area);

		if(GUILayout.Button ("Play!", GUILayout.Height (buttonHeight)))
		{
			if(!instructionsShown){
				instructionsShown = true;
				title.SetActive(false);
				instructions.SetActive(true);
			}
			else if(instructionsShown){
				Application.LoadLevel ("dev");
			}
		}

		GUILayout.Space (spacing);

		if(GUILayout.Button ("Exit", GUILayout.Height (buttonHeight)))
			Application.Quit();

		GUILayout.EndArea ();
	}
}
