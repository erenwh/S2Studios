using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSlowDown : MonoBehaviour {

	private GameObject callingPlayer;				//keep track of the player who called it
	private float speedDecrementFactor = 0.75f;		//slow other players/attacks that enter the distortion by 50% at default
	public float timeUsedPerSecond = 0.75f;			//how much extra time is used per second of use (rounded down)?

	// Use this for initialization
	void Start () {
		//TODO: Have TimeDistortion script on player instantiate and call this function, rather than in here
		AssignPlayer (GameObject.FindGameObjectWithTag("Player"), 0.75f);
	}

	/// <summary>
	/// The Player calls this function whenever he instantiates a time "slow down" distortion. This will make sure that only the calling player isn't by the slow down, and follows them around the map.
	/// </summary>
	/// <param name="player">Player that created (this.gameObject).</param>
	/// <param name="factor"> How much should the distortion slow down others?</param>
	public void AssignPlayer (GameObject player, float factor) {
		callingPlayer = player;
		speedDecrementFactor = factor;
	}

	/// <summary>
	/// In this instance, used to make sure the time distortion follows the calling player.
	/// </summary>
	void FixedUpdate () {
		transform.position = callingPlayer.transform.position;
	}

	/// <summary>
	/// Upon entering the distortion, slow down other objects (Not Player)
	/// </summary>
	/// <param name="coll">Coll.</param>
	void OnTriggerEnter2D (Collider2D coll) {
		if (coll.CompareTag("Player") && coll.gameObject != callingPlayer) {
			coll.gameObject.GetComponent<PlayerMovement> ().speed *= speedDecrementFactor;
		}
		//TODO: Also slow down attack projectiles, or anything else that thematically makes sense.
	}

	/// <summary>
	/// Return everything inside to its original speed when they leave the distortion.
	/// </summary>
	/// <param name="coll">Coll.</param>
	void OnTriggerExit2D (Collider2D coll) {
		if (coll.CompareTag("Player") && coll.gameObject != callingPlayer) {
			coll.gameObject.GetComponent<PlayerMovement> ().speed /= speedDecrementFactor;
		}
		//TODO: Also restore attack projectile speed, or anything else that thematically makes sense.
	}

	/// <summary>
	/// Return everything inside to its original speed when distortion is deactivated.
	/// </summary>
	void OnDestroy () {
		GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
		foreach (GameObject player in players) {
			if (player.GetComponent<Collider2D>().IsTouching(GetComponent<Collider2D>()) && player != callingPlayer) {
				player.GetComponent<PlayerMovement> ().speed /= speedDecrementFactor;
			}
		}
		//TODO: Also finds and restore attack projectile speed, or anything else that thematically makes sense.
	}
}
