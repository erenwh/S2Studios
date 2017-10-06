using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTime : MonoBehaviour {

	public int startingTime; // changing to nothing so we may keep changing it as needed
	public int timeRemaining;
	public Slider timeIndicator;

	/// <summary>
	/// Takes a specified amount of time from a player and gives it to another player.
	/// </summary>
	/// <param name="amount">Amount of time transfered (int).</param>
	/// <param name="playerFrom">The player losing time.</param>
	/// <param name="playerTo">The player gaining time (The attacking player).</param>
	public static void TransferTime (int amount, GameObject playerFrom, GameObject playerTo) {
		if (playerFrom != null) {
			playerFrom.GetComponent<PlayerTime> ().timeRemaining -= amount;
			playerFrom.GetComponent<PlayerTime> ().timeIndicator.value = playerFrom.GetComponent<PlayerTime> ().timeRemaining;
		}
		if (playerTo != null) {
			playerTo.GetComponent<PlayerTime> ().timeRemaining += amount;
			playerTo.GetComponent<PlayerTime> ().timeIndicator.value = playerTo.GetComponent<PlayerTime> ().timeRemaining;
		}
	}

	// Use this for initialization
	void Start () {
		//Assuming player's begin immediately after spawning
		BeginCountdown();
	}

	/// <summary>
	/// Has the player's time start its 1 per second decrement, and resets the initial time to whatever it should start with. 
	/// </summary>
	void BeginCountdown () {
		timeRemaining = startingTime;
		StartCoroutine("DecrementTime");
	}

	/// <summary>
	/// Decrements the timeRemaining by 1 every second, calls relevant death methods upon going under 1 second of timeRemaining.
	/// </summary>
	/// <returns>The time.</returns>
	IEnumerator DecrementTime () {
		while (true) {
			yield return new WaitForSeconds (1f);
			timeRemaining--;
			timeIndicator.value = timeRemaining;
			if (timeRemaining < 1) {
				//Call Death animation & play sfx
				//let game controller know
				Destroy(gameObject);
			}
		}
	}
}
