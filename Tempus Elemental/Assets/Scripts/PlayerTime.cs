using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTime : MonoBehaviour {

	public int startingTime = 30;
	public int timeRemaining;

	// Use this for initialization
	void Start () {

		//Assuming player's begin immediately after spawning
		BeginCountdown();
	}
	
	// Update is called once per frame
	void Update () {
		
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
			if (timeRemaining < 1) {
				//Call Death animation & play sfx
				//let game controller know
				//Destroy (gameObject);
			}
		}
	}
}
