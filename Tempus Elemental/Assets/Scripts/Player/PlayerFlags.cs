using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlags : MonoBehaviour {
	public int numFlags = 0;
	public bool hasFlag = false;

	public void holdingFlag(GameObject player) {
		player.GetComponent<PlayerMovement> ().speed = player.GetComponent<PlayerMovement> ().speed / 2;
		player.GetComponent<PlayerTime> ().TimeRemaining = player.GetComponent<PlayerTime> ().TimeRemaining / 2;
	}

	void Update () {
		if (hasFlag == true) {
			// put flag above the players head
		}	
	}
}
