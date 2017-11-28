using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CTFController : GameController {
	//public int t1score = 0;
	//public int t2score = 0;
	//public int numberPlayers = 0;
	private string wTeam = "";

	public override void OnStart ()
	{
		base.OnStart ();
	}

	protected override void GameLogic()
	{

	}

	protected override bool VictoryCondition () 
	{
		int t1score = 0;
		int t2score = 0;
		for (int i = 0; i < numPlayers; i++) {
			if (players [i].tag == "Player1" || players [i].tag == "Player3") {
				t1score += players [i].GetComponent<PlayerFlags> ().numFlags;
			}
			else if (players [i].tag == "Player2" || players [i].tag == "Player4") {
				t2score += players [i].GetComponent<PlayerFlags> ().numFlags;
			}
		}
		
		if (t1score >= 3) {
			wTeam = "Team 1 Wins CTF Bitch!";
			return true;
		}
		else if (t2score >= 3) {
			wTeam = "Team 2 Wins CTF YAYEET!";
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
