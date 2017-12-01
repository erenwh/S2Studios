using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXHandler : MonoBehaviour {

	//references
	public AudioClip hurt;
	public AudioClip dash;
	public AudioClip collect;

	void Start () {
		DontDestroyOnLoad(transform.gameObject);
	}

	public static void HurtSFX() {
		SFXHandler sfx = GameObject.FindGameObjectWithTag ("SFXHandler").GetComponent<SFXHandler> ();
		AudioSource aus = sfx.gameObject.GetComponent<AudioSource> ();
		aus.PlayOneShot (sfx.hurt, Game.Instance.soundEffectsVolume / 100.0f);
	}
	
	public static void DashSFX() {
		SFXHandler sfx = GameObject.FindGameObjectWithTag ("SFXHandler").GetComponent<SFXHandler> ();
		AudioSource aus = sfx.gameObject.GetComponent<AudioSource> ();
		aus.PlayOneShot (sfx.dash, Game.Instance.soundEffectsVolume / 100.0f);
	}

	public static void CollectSFX() {
		SFXHandler sfx = GameObject.FindGameObjectWithTag ("SFXHandler").GetComponent<SFXHandler> ();
		AudioSource aus = sfx.gameObject.GetComponent<AudioSource> ();
		aus.PlayOneShot (sfx.collect, Game.Instance.soundEffectsVolume / 100.0f);
	}
}
