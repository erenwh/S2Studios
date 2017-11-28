using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class FFAController : GameController {

    protected void SDReset()
    {
        for (int i = 0; i < numPlayers; ++i)
        {
            // set new time and set active each player, then start the game again
        }
        // let the games begin again.
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
        }
        // the only other case that is not 1 is zero, meaning everyone died. Sudden Death Activates
        SDReset();
        return "Sudden Death Activated.";
    }
}
