using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class FFAController : GameController 
{ // inherits from GameController

    //public override void SpawnPlayers()
    //{
    //    //if (sdTag) {
    //    //    for (int i = 0; i < numPlayers; ++i) {
    //    //        GameObject newPlayer = Instantiate(playerPrefab);
    //    //        newPlayer.GetComponent<PlayerTime>().timeRemaining = 20;
    //    //        newPlayer.tag = "Player" + (i + 1);
    //    //    }
    //    //}

    //    //return;
    //}

    protected void suddenDeathReset()
    {
        // remove the old players
        // spawn the players set their reset times for sudden death
        // let the games begin.
    }

    protected override void GameLogic() 
    {
        // Nothing here for Free for all   
    }

    protected override bool VictoryCondition()
    {
        
        if (players.Count <= 1) {
            return true;
        }

        return false; // this means no end-game state has been breached and the game continues
    }

    protected override string VictoryText()
    {
        if (players.Count == 1) {
            return "The Sole Survivor ... " + players[0].tag;
        } else if (players.Count == 0)
        {
            suddenDeathReset();
        }

        // return "REEEEEE Get off my board normies REEEEEE!"; // motivate the player for forcing a stalemate.
    }
}
