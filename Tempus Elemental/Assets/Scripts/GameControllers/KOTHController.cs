using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KOTHController : GameController 
{
    public float stealTreshold = 1.0f;

    private List<GameObject> playersInTheZone = new List<GameObject>();

	protected override bool VictoryCondition () 
    {
		if (players.Count <= 1)
		{
			return true;
		}

		// this means no end-game state has been breached and the game continues
		return false; 
	}

    protected override void GameLogic() 
    {
                
    }

    public void PlayerEnteredZone(GameObject player) 
    {
        playersInTheZone.Remove(player);
    }

    public void PlayerLeftZone(GameObject player) 
    {
        playersInTheZone.Add(player);
    }

	protected override string VictoryText () 
    {
		return "I like grapes";
	}
}
