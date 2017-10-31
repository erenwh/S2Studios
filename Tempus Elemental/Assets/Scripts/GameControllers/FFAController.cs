using System.Collections;
using System.Collections.Generic;

public class FFAController : GameController {

    private string winnerName = "";
    
    public override void SpawnObjects()
    {
    }

    public override void SpawnPlayers()
    {
        //
    }

    public override void UpdatePoints()
    {        
    }

    public override bool VictoryCondition()
    {
        int numOfPlayersAlive = 0;
        foreach(var player in players)
        {
            if (player.GetComponent<PlayerTime>().IsPlayerAlive()) {
                numOfPlayersAlive++;
                winnerName = player.tag;
            }
        }

        if (numOfPlayersAlive == 0)
        {
            winnerName = "";
            return true;
        }

        if (numOfPlayersAlive == 1) 
        {
            return true;
        }

        return false;
    }

    protected override string VictoryText()
    {
        if (winnerName == "") {
            return "It's a tie!";
        }

        return winnerName + " is the winner!";
    }
}
