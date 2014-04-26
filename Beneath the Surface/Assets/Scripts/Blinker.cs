using UnityEngine;
using System.Collections;

public class Blinker : MonoBehaviour {

	public int totalBlinks;
	public float blinkRate;
	public Color blinkColor;

	private float nextBlink;
	private int blinksLeft;

	private bool blinking;
	private bool isColored;

	private IRagePixel rps;

	// Use this for initialization
	void Start () {
		blinking = false;
		isColored = false;
		rps = GetComponent<RagePixelSprite> ();

	}
	
	// Update is called once per frame
	void Update () {
		if(blinking && blinksLeft > 0 && Time.time > nextBlink) {
			nextBlink = Time.time + blinkRate;
			blink();
			blinksLeft--;
		}

		if(blinksLeft <= 0 && blinking)
		{
			blinkClear ();
			blinking = false;
		}
	
	}

	public void startBlinking(){
		isColored = false;
		blinking = true;
		nextBlink = Time.time;
		blinksLeft = totalBlinks;
	}

	void blink(){
		if (isColored) {
			blinkClear();
		}
		else 
		{
			blinkColored();
		}
	}

	void blinkClear(){
		rps.SetTintColor (Color.white);
		isColored = false;
	}

	void blinkColored(){
		rps.SetTintColor (blinkColor);
		isColored = true;
	}
}
