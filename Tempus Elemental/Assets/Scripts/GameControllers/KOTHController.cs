using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KOTHController : GameController {

	// Use this for initialization
	void Start () {
		
	}

	public override bool VictoryCondition () {
		return false;
	}

	public override void SpawnPlayers() {
	
	}

	public override void SpawnObjects() {
	
	}

	public override void UpdatePoints() {
		
	}

	protected override string VictoryText () {
		return "I like grapes";
	}
}
