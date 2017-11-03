using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class FFAController : GameController { // inherits from GameController

    public GameObject playerPrefab;

    private bool sdTag = false;
    
    public override void SpawnObjects()
    {
        // Un-needed Function
    }

    public override void SpawnPlayers()
    {
        //if (sdTag) {
        //    for (int i = 0; i < numPlayers; ++i) {
        //        GameObject newPlayer = Instantiate(playerPrefab);
        //        newPlayer.GetComponent<PlayerTime>().timeRemaining = 20;
        //        newPlayer.tag = "Player" + (i + 1);
        //    }
        //}

        //return;
    }

    public override void UpdatePoints()
    {        
        // Un-needed Function
    }

    public override bool VictoryCondition()
    {
        
        if (players.Count <= 1) {
            return true;
        }

        return false; // this means no end-game state has been breached and the game continues
    }

    protected override string VictoryText()
    {
        if (players.Count == 1) {
            return " WINNER! : " + players[0].tag;
        }

        return " Y'all Lame!";		//but... why? :'(
    }
}
