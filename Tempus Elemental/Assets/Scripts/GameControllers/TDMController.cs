using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDMController : GameController {

    //default with p1 and p2 on teams and p3 and p4 on teams

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
        return false;
    }

    protected override string VictoryText()
    {
        return "";
    }

    // Use this for initialization
    void Start () {
        player4 = GameObject.Find("Player4");
        player3 = GameObject.Find("Player3");
        player2 = GameObject.Find("Player2");
        player1 = GameObject.Find("Player1");
    }
	
	// Update is called once per frame
	void Update () {
        //destory player when they run out of time
		if (!((PlayerTime)player4.GetComponent(typeof(PlayerTime))).IsPlayerAlive())
        {
            Destroy(player4);
            alivePlayers[3] = false;
        }
        else if (!((PlayerTime)player3.GetComponent(typeof(PlayerTime))).IsPlayerAlive())
        {
            Destroy(player3);
            alivePlayers[2] = false;
        }
        else if (!((PlayerTime)player2.GetComponent(typeof(PlayerTime))).IsPlayerAlive())
        {
            Destroy(player2);
            alivePlayers[1] = false;
        }
        else if (!((PlayerTime)player1.GetComponent(typeof(PlayerTime))).IsPlayerAlive())
        {
            Destroy(player1);
            alivePlayers[0] = false;
        }
	}
}
