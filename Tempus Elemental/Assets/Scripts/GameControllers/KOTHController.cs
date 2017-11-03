using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KOTHController : GameController 
{
    public float dealTimeTreshold = 1.0f;
    public int exchangeRate = 5;

    private float timeSinceLastDeal = 0.0f;

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
        timeSinceLastDeal += Time.deltaTime;
        if (timeSinceLastDeal >= dealTimeTreshold) 
        {
            timeSinceLastDeal = 0;
            DealWithPlayerTimes();
        }
    }

    private void DealWithPlayerTimes() {
        // everyone is chilling in the zone or no one is in the zone, not cool
        if (playersInTheZone.Count == players.Count || playersInTheZone.Count == 0) {
            return;
        }

		foreach (var player in playersInTheZone)
		{            
			player.GetComponent<PlayerTime>().AddTime(exchangeRate);
		}

		List<GameObject> playersNotInTheZone = (List<GameObject>)players.Except(playersInTheZone);
		foreach (var player in playersNotInTheZone)
		{
			player.GetComponent<PlayerTime>().DecrementTime(exchangeRate);
		}
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

    public override void KillPlayer(GameObject player)
    {
        playersInTheZone.Remove(player);
        base.KillPlayer(player);
    }
}
