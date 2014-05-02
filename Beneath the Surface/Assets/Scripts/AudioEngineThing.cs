using UnityEngine;
using System.Collections;

public class AudioEngineThing : MonoBehaviour {

	public AudioClip birdCall;
	public AudioClip splash;
	public AudioClip orca;
	public AudioClip selfDamage;
	public AudioClip explosion;
	public AudioClip chain;
	public AudioClip diver;

	public AudioClip[] munches;

	public bool test;

	public float audioCoolDown = 1.0f;
	private float nextAudio;

	// Update is called once per frame
	void Update () {
		if (test) {
			test=false;
			munch ();
		}
	}

	public void munch(){
		AudioClip munch = munches [Random.Range (0, munches.Length - 1)];
		AudioSource.PlayClipAtPoint (munch, this.transform.position);
		AudioSource.PlayClipAtPoint (chain, this.transform.position);
	}

	public void chainPlay(){
		AudioSource.PlayClipAtPoint (chain, this.transform.position);
	}

	public void damage(){
		if (Time.time > nextAudio){
			nextAudio = Time.time + audioCoolDown;
			AudioSource.PlayClipAtPoint (selfDamage, this.transform.position);
		}
	}

	public void explosionPlay(){
		AudioSource.PlayClipAtPoint (explosion, this.transform.position);
	}

	public void playSound(string name)
	{
		if (name.Contains("Bird"))
			AudioSource.PlayClipAtPoint (birdCall, this.transform.position);
		if (name.Contains("Orca"))
			AudioSource.PlayClipAtPoint (orca, this.transform.position);

		if (name.Contains("Diver"))
			AudioSource.PlayClipAtPoint (diver, this.transform.position);
	}

	public void playSplash()
	{
		if (Time.time > nextAudio){
			nextAudio = Time.time + audioCoolDown;
			AudioSource.PlayClipAtPoint (splash, this.transform.position);
		}
	}
}
