using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDMController : GameController {

    //default with p1 and p3 on teams and p2 and p4 on teams

    private string winningTeam = "";

    //list to check for alive players                  p1    p2    p3    p4
    private List<bool> alivePlayers = new List<bool> { true, true, true, true };
    GameObject player4;
    GameObject player3;
    GameObject player2;
    GameObject player1;

    public override void SpawnObjects()
    {
        
    }

    public override void SpawnPlayers()
    {
        
    }

    public override void UpdatePoints()
    {
        
    }

    public override bool VictoryCondition()
    {
        //destory player when they run out of time
        int i = 0;
        foreach (var player in players)
        {
            if (!player.GetComponent<PlayerTime>().IsPlayerAlive())
            {
                Destroy(player);
                alivePlayers[i] = false;
            }
            i++;
        }

        if (!alivePlayers[0] && !alivePlayers[1] && !alivePlayers[2] && !alivePlayers[3])
        {
            winningTeam = "Draw";
            return true;
        }
        else if (!alivePlayers[0] && !alivePlayers[2])
        {
            winningTeam = "Team 1 Wins!";
            return true;
        }
        else if (!alivePlayers[1] && !alivePlayers[3])
        {
            winningTeam = "Team 2 Wins!";
            return true;
        }
        return false;
    }

    protected override string VictoryText()
    {
        return winningTeam;
    }

    // Use this for initialization
    void Start () {
        player4 = GameObject.Find("Player4");
        player3 = GameObject.Find("Player3");
        player2 = GameObject.Find("Player2");
        player1 = GameObject.Find("Player1");
    }
}
