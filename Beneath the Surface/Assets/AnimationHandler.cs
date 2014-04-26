using UnityEngine;
using System.Collections;

public class AnimationHandler : MonoBehaviour {

	public float munchTime;
	private IRagePixel ragePixel;
	private bool playing;
	private float stopTime;
	private string currentAnimation;
	private HealthSystem hs;

	// Use this for initialization
	void Start () {
		ragePixel = GetComponent<RagePixelSprite> ();
		hs = GetComponent < HealthSystem> ();
	
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
			ragePixel.PlayNamedAnimation("Idle", false);
		}
	}

	public void playEat(float healthSystemAmount){
		if(!playing){
			playing = true;
			stopTime = Time.time + munchTime;
			ragePixel.PlayNamedAnimation("Eat", false);
			currentAnimation = "Eat";
			hs.addHealth(healthSystemAmount);
		}

	}
}
