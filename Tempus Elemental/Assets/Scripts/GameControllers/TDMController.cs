using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDMController : GameController {

    //default with p1 and p3 on teams and p2 and p4 on teams

    private string winningTeam = "";

    //list to check for alive players                  p1    p2    p3    p4
    private List<bool> alivePlayer = new List<bool> { false, false, false, false };

    void Start()
    {
        Debug.Log("In TDM start"); //this line does not run
    }

    private void Awake()
    {
        Debug.Log("In TDM awake"); //neither does this one
    }

    // Update is called once per frame
    //void Update () {

    //}

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
        GameObject player;
        for (int i = 0; i < players.Count; i++)
        {
            if (!players[i].GetComponent<PlayerTime>().IsPlayerAlive())
            {
                player = players[i];
                players.Remove(players[i]);
                Destroy(player);
                alivePlayer[i] = false;
            }
            else
            {
                alivePlayer[i] = true;
            }
        }

        if (!alivePlayer[0] && !alivePlayer[1] && !alivePlayer[2] && !alivePlayer[3])
        {
            winningTeam = "Draw";
            return true;
        }
        else if (!alivePlayer[0] && !alivePlayer[2])
        {
            winningTeam = "Team 1 Wins!";
            return true;
        }
        else if (!alivePlayer[1] && !alivePlayer[3])
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
}
