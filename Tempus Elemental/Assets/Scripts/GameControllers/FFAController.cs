using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class FFAController : GameController { // inherits from GameController

    private string winnerName = "";

    public GameObject playerPrefab;

    private bool sdTag = false;
    
    public override void SpawnObjects()
    {
        // Un-needed Function
    }

    public override void SpawnPlayers()
    {
        if (sdTag) {
            for (int i = 0; i < numPlayers; ++i) {
                GameObject newPlayer = Instantiate(playerPrefab);
                newPlayer.GetComponent<PlayerTime>().timeRemaining = 20;
                newPlayer.tag = "Player" + (i + 1);
            }
        }

        return;
    }

    public override void UpdatePoints()
    {        
        // Un-needed Function
    }

    public override bool VictoryCondition()
    {
        int numOfPlayersAlive = 0; // this resets the alive-check each 60th second.
        winnerName = ""; // reset the winner
        foreach(var player in players) // for each player, allocate how many are still alive.
        {
            if (player.GetComponent<PlayerTime>().IsPlayerAlive()) {
                winnerName = player.tag; // if only one player is alive, then it will be the only string to copy.
                numOfPlayersAlive++;
            } else {
                Destroy(player);
            }
        }

        // return when either end-game state is breached.
        if ((numOfPlayersAlive == 0) || (numOfPlayersAlive == 1))
        {
            return true;
        }

        return false; // this means no end-game state has been breached and the game continues
    }

    protected override string VictoryText()
    {
        if (winnerName == "") {
            return "!SUDDEN DEATH!";
        }

        return " WINNER! : " + winnerName;
    }
}
