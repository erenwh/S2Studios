using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTime : MonoBehaviour {

	public int startingTime; 			// changing to nothing so we may keep changing it as needed
	private int timeRemaining;			// how much time does this player have left?
	public Slider timeIndicator;
	public Text timeText;

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
			playerFrom.GetComponent<PlayerTime> ().timeText.text = playerFrom.GetComponent<PlayerTime> ().timeRemaining.ToString ();
		}
		if (playerTo != null) {
			playerTo.GetComponent<PlayerTime> ().timeRemaining += amount;
			playerTo.GetComponent<PlayerTime> ().timeIndicator.value = playerTo.GetComponent<PlayerTime> ().timeRemaining;
			playerTo.GetComponent<PlayerTime> ().timeText.text = playerTo.GetComponent<PlayerTime> ().timeRemaining.ToString ();
		}
	}

	/// <summary>
	/// Decrements the timeRemaining of this player.
	/// </summary>
	/// <param name="timeLost">How much time will this player lose? (int).</param>
	public void DecrementTime (int timeLost) {
		timeRemaining -= timeLost;
		timeIndicator.value = timeRemaining;
		timeText.text = timeRemaining.ToString ();
	}

	/// <summary>
	/// Adds to the timeRemaining of this player.
	/// </summary>
	/// <param name="timeGained">Time gained.</param>
	public void AddTime (int timeGained) {
		timeRemaining += timeGained;
		timeIndicator.value = timeRemaining;
		timeText.text = timeRemaining.ToString ();
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
		StartCoroutine("DecrementOverTime");
	}

	/// <summary>
	/// Decrements the timeRemaining by 1 every second, calls relevant death methods upon going under 1 second of timeRemaining.
	/// </summary>
	/// <returns>The time.</returns>
	IEnumerator DecrementOverTime () {
		while (true) {
			yield return new WaitForSeconds (1f);
			DecrementTime (1);
			if (timeRemaining < 1) {
				//Call Death animation & play sfx
				//let game controller know
				Destroy(gameObject);
			}
		}
	}
}
