using UnityEngine;
using System.Collections;

public class AnimationHandler : MonoBehaviour {

	public float munchTime;
	private IRagePixel ragePixel;
	private bool playing;
	private float stopTime;
	private string currentAnimation;

	// Use this for initialization
	void Start () {
		ragePixel = GetComponent<RagePixelSprite> ();
		ragePixel.PlayNamedAnimation("Swim", false);
	
	}
	
	// Update is called once per frame
	void Update () {
		if(playing)
		{
			checkAnimations();
		}
	
	}

	void checkAnimations(){
		if(playing && Time.time >= stopTime) {
			playing = false;
			ragePixel.PlayNamedAnimation("Swim", false);
		}
	}

	public void playEat(){
		if(!playing){
			playing = true;
			stopTime = Time.time + munchTime;
			ragePixel.PlayNamedAnimation("Eat", false);
			currentAnimation = "Eat";
		}

	}

	public void playDamage(){
		if(!playing){
			playing = true;
			stopTime = Time.time + 1.0f;
		}
	}
}
