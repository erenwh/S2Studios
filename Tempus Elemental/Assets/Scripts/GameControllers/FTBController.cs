using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class FTBController : GameController {

	//references
	public GameObject barObj;
	public GameObject timeLeftUI;

	//variables
	private GameObject bar;			//that bar that shows how much time each player has collected
	public float matchTime = 120;	//how many seconds does a match last?
	private float timeLeft;


	//called when the map is loaded
	public override void OnStart () {
		base.OnStart ();
		bar = Instantiate (barObj);
		timeLeft = matchTime;
	}

	protected override void GameLogic() {
		// Check for player death and respawn them.

		// Keep track of match timer, update timeLeftUI
	}

	protected override bool VictoryCondition() {
		if (timeLeft <= 0) {
			return true;
		}

		return false;
	}

	protected override string VictoryText() {
		//Return player with highest bar filled
		if (players.Count == 1) {
			return " WINNER! : " + players[0].tag;
		}

		return "Fire Emblem HEROES!";
	}
}
