using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSpeedUp : MonoBehaviour {

	//Speed up code is very similar to slow down code, if we don't like that it affects others then we can refactor it. But it may cause otherwise unnecessary cooperation between rivals to speed each other along the map!

	public float transparency;						//how transparent is the clock
	private GameObject callingPlayer;				//keep track of the player who called it
	private float speedIncrementFactor = 1.50f;		//speed up other players/attacks that enter the distortion by 150% at default

	// make sure the proper animation is playing
	void Start () {
		GetComponent<Animator> ().SetInteger ("Type", 1);
	}

	// The Player calls this function whenever he instantiates a time "speed up" distortion. This will make sure it follows them around the map.
	public void AssignPlayer (GameObject player, float factor) {
		callingPlayer = player;
		speedIncrementFactor = factor;
		player.GetComponent<PlayerMovement> ().speed *= speedIncrementFactor;
		SpriteRenderer[] srs = GetComponentsInChildren<SpriteRenderer> ();
		foreach (SpriteRenderer sr in srs) {
			sr.color = new Color(player.GetComponent<PlayerColor> ().color.r, player.GetComponent<PlayerColor> ().color.g, player.GetComponent<PlayerColor> ().color.b, transparency);
		}
	}

	// In this instance, used to make sure the time distortion follows the calling player.
	void FixedUpdate () {
		transform.position = callingPlayer.transform.position;
	}
		
	// Upon entering the distortion, slow down other objects (Not Player)
	void OnTriggerEnter2D (Collider2D coll) {
		if ((coll.CompareTag("Player1") || coll.CompareTag("Player2") || coll.CompareTag("Player3") || coll.CompareTag("Player4")) && coll.gameObject != callingPlayer && !coll.isTrigger) {
			coll.gameObject.GetComponent<PlayerMovement> ().speed *= speedIncrementFactor;
		}
		if (coll.CompareTag ("Fire")) {
			coll.gameObject.GetComponent<Projectile> ().speed *= speedIncrementFactor;
		}
	}

	// Return everything inside to its original speed when they leave the distortion.
	void OnTriggerExit2D (Collider2D coll) {
		if ((coll.CompareTag("Player1") || coll.CompareTag("Player2") || coll.CompareTag("Player3") || coll.CompareTag("Player4")) && coll.gameObject != callingPlayer && !coll.isTrigger) {
			coll.gameObject.GetComponent<PlayerMovement> ().speed /= speedIncrementFactor;
		}
		if (coll.CompareTag ("Fire")) {
			coll.gameObject.GetComponent<Projectile> ().speed /= speedIncrementFactor;
		}
	}

	// Return everything inside to its original speed when distortion is deactivated.
	void OnDestroy () {
		callingPlayer.GetComponent<PlayerMovement> ().speed /= speedIncrementFactor;
		for(int i = 1; i < 5; i++) {
			GameObject player = GameObject.FindGameObjectWithTag ("Player" + i.ToString ());
			if (player != null && player.GetComponent<Collider2D>().IsTouching(GetComponent<PolygonCollider2D>()) && player != callingPlayer) {
				player.GetComponent<PlayerMovement> ().speed /= speedIncrementFactor;
			}
		}
		GameObject[] projectiles = GameObject.FindGameObjectsWithTag ("Fire");
		foreach (GameObject projectile in projectiles) {
			if (projectile.GetComponent<Collider2D>().IsTouching(GetComponent<Collider2D>())) {
				projectile.GetComponent<Projectile> ().speed /= speedIncrementFactor;
			}
		}
	}
}
