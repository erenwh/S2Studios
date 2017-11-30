using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class FTBController : GameController {

	//references
	public GameObject barObj;

	//variables
	private Bar bar;										//that bar that shows how much time each player has collected
	public float timeToRespawn = 5.0f;						//how long does it take a player to respawn
	public int maxTime = 230;								//how many seconds do the players have to collect until the match ends
	private int[] timeCollected;							//how much time has been collected by each player
	private float[] deadPlayers;							//how long has each player been dead
	private bool[] respawningPlayers;						//which players are respawning

	//called when the map is loaded
	public override void OnStart () {
		base.OnStart ();
		bar = Instantiate (barObj).GetComponent<Bar>();
		bar.maxTime = maxTime;
		timeCollected = new int[4];
		deadPlayers = new float[4];
		respawningPlayers = new bool[4];
		for (int i = 0; i < 4; i++) {
			timeCollected[i] = 0;
			deadPlayers [i] = 0;
			respawningPlayers [i] = false;
		}
	}

	protected override void GameLogic() {
		// Check for player death and respawn them.
		for (int i = 0; i < 4; i++) {
			if (deadPlayers [i] > 0) {
				deadPlayers [i] -= Time.deltaTime;
				players [i].GetComponent<PlayerTime> ().radialIndicator.fillAmount = (timeToRespawn - deadPlayers [i]) / timeToRespawn;
			} else if (respawningPlayers[i] == true) {
				respawningPlayers [i] = false;
				deadPlayers [i] = 0;
				players [i].GetComponent<PlayerTime> ().TimeRemaining = Game.Instance.playersStartingTime;
				players [i].SetActive (true);
			}
		}

		// Keep track of time collected, update bar
	}

	// Add the amount of time collected to the bar
	public override void CollectTime (int playerNum, int amount) {
		timeCollected [playerNum] += amount;
		bar.UpdateBars (timeCollected [0], timeCollected [1], timeCollected [2], timeCollected [3]);
	}

	//respawn players rather than killing them off
	public override void KillPlayer (GameObject player) {
		deadPlayers [player.GetComponent<PlayerColor> ().playerNum] = timeToRespawn;
		respawningPlayers [player.GetComponent<PlayerColor> ().playerNum] = true;
		player.SetActive (false);
	}

	//Stop the match upon enough collection of time, when the bar is filled
	protected override bool VictoryCondition() {
		if ((timeCollected[0] + timeCollected[1] + timeCollected[2] + timeCollected[3]) >= maxTime) {
			return true;
		}

		return false;
	}

	//Let the user know who one the round
	protected override string VictoryText() {
		//Return player with highest bar filled
		int winner = 0;
		int max = 0;
		bool draw = false;
		for (int i = 0; i < 4; i++) {
			if (timeCollected [i] == max) {
				draw = true;
			}
			if (timeCollected [i] > max) {
				winner = i;
				draw = false;
				max = timeCollected[i];
			}
		}

		if (draw) {
			return "It's a tie!";
		} else {
			return "Winner! : " + players [winner].tag;
		}
			
		//return "Stay Hydrated!";
	}
}
