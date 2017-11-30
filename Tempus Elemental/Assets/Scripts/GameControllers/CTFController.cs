using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CTFController : GameController {
	//public int t1score = 0;
	//public int t2score = 0;
	//public int numberPlayers = 0;
	private string wTeam = "";
	private float[] deadPlayers;							//how long has each player been dead
	private bool[] respawningPlayers;						//which players are respawning
	public float timeToRespawn = 5.0f;						//how long does it take a player to respawn

	//upon starting the match, call each goal's onGameStart public function for setup
	public override void OnStart ()
	{
		base.OnStart ();
		deadPlayers = new float[numPlayers];
		respawningPlayers = new bool[numPlayers];
		for (int i = 0; i < numPlayers; i++) {
			deadPlayers [i] = 0;
			respawningPlayers [i] = false;
		}
	}

	protected override void GameLogic()
	{
		for (int i = 0; i < numPlayers; i++) {
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
	}

	public override void KillPlayer (GameObject player) {
		deadPlayers [player.GetComponent<PlayerColor> ().playerNum] = timeToRespawn;
		respawningPlayers [player.GetComponent<PlayerColor> ().playerNum] = true;
		player.SetActive (false);
	}

	protected override bool VictoryCondition () 
	{
		int t1score = 0;
		int t2score = 0;
		for (int i = 0; i < players.Count; i++) {
			if (players [i].tag == "Player1" || players [i].tag == "Player3") {
				t1score += players [i].GetComponent<PlayerFlags> ().numFlags;
			}
			else if (players [i].tag == "Player2" || players [i].tag == "Player4") {
				t2score += players [i].GetComponent<PlayerFlags> ().numFlags;
			}
		}
		
		if (t1score >= 3) {
			wTeam = "Team 1 Scored First!";
			return true;
		}
		else if (t2score >= 3) {
			wTeam = "Team 2 Scored First YAYEET!";
			return true;
		}
		return false;
	}

	public void respawnPlayer(GameObject player) {
		
	}

	protected override string VictoryText()
	{
		return wTeam;
	}
}
