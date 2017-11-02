using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeFreeze : MonoBehaviour {

	public float transparency;											//how transparent is the clock
	private GameObject callingPlayer;									//keep track of the player who called it
	public float radius;												//how big is this time freeze?
	private List<GameObject> caughtObjects = new List<GameObject>();	//all items caught in the time freeze
	private List<float> caughtSpeeds = new List<float>();				//all speeds of items caught in the time freeze

	// make sure the proper animation is playing
	void Start () {
		GetComponent<Animator> ().SetInteger ("Type", 2);
	}

	// The Player calls this function whenever he instantiates a time "slow down" distortion. This will make sure that only the calling player isn't by the slow down, and follows them around the map.
	public void AssignPlayer (GameObject player, float radius) {
		callingPlayer = player;
		caughtObjects.Add (callingPlayer);
		caughtSpeeds.Add (callingPlayer.GetComponent<PlayerMovement> ().speed);
		callingPlayer.GetComponent<PlayerMovement> ().speed = 0;
		transform.localScale = new Vector3 (radius, radius, 1);
		SpriteRenderer[] srs = GetComponentsInChildren<SpriteRenderer> ();
		foreach (SpriteRenderer sr in srs) {
			sr.color = new Color(player.GetComponent<PlayerColor> ().color.r, player.GetComponent<PlayerColor> ().color.g, player.GetComponent<PlayerColor> ().color.b, transparency);
		}
	}

	// In this instance, used to make sure the time distortion stays with the calling player.
	void FixedUpdate () {
		transform.position = callingPlayer.transform.position;
	}

	// Upon entering the distortion, stop other objects
	void OnTriggerEnter2D (Collider2D coll) {
		if ((coll.CompareTag("Player1") || coll.CompareTag("Player2") || coll.CompareTag("Player3") || coll.CompareTag("Player4")) && coll.gameObject != callingPlayer && !coll.isTrigger) {
			caughtObjects.Add (coll.gameObject);
			caughtSpeeds.Add (coll.gameObject.GetComponent<PlayerMovement> ().speed);
			coll.gameObject.GetComponent<PlayerMovement> ().speed = 0;
		}
		if (coll.CompareTag ("Fire")) {
			caughtObjects.Add (coll.gameObject);
			caughtSpeeds.Add (coll.gameObject.GetComponent<Projectile> ().speed);
			coll.gameObject.GetComponent<Projectile> ().speed = 0;
		}
	}

	// Return everything inside to its original speed when distortion is deactivated.
	void OnDestroy () {
		for (int i = 0; i < caughtObjects.Count; i++) {
			if (caughtObjects [i] != null) {
				if (caughtObjects [i].CompareTag ("Fire")) {
					caughtObjects [i].GetComponent<Projectile> ().speed = caughtSpeeds [i];
				} else {
					caughtObjects [i].GetComponent<PlayerMovement> ().speed = caughtSpeeds [i];				//there's going to be a bug if they deactivate their speedup/slowdown while frozen...
				}
			}
		}
	}
}
