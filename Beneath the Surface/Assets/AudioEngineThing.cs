using UnityEngine;
using System.Collections;

public class AudioEngineThing : MonoBehaviour {

	public AudioClip birdCall;
	public AudioClip splash;

	public AudioClip[] munches;

	public float audioCoolDown = 1.0f;
	private float nextAudio;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void munch(){
		Debug.Log ("Should be munching audio");
		AudioClip munch = munches [Random.Range (0, munches.Length - 1)];
		Debug.Log (munch.name);
		AudioSource.PlayClipAtPoint (munch, this.transform.position);
	}

	public void playSound(string name)
	{
		Debug.Log ("Play " + name);
		if (name.Contains("Bird"))
			AudioSource.PlayClipAtPoint (birdCall, this.transform.position);
	}

	public void playSplash()
	{
		if (Time.time > nextAudio){
			nextAudio = Time.time + audioCoolDown;
			AudioSource.PlayClipAtPoint (splash, this.transform.position);
		}
	}
}
