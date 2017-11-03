using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDMController : GameController {

    //default with p1 and p3 on teams and p2 and p4 on teams

    private string winningTeam = "";

    //list to check for alive players                  p1    p2    p3    p4
    private List<bool> alivePlayer = new List<bool> { false, false, false, false };


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
        if (players.Count == 0) 
        {
			winningTeam = "Draw";
			return true;
        } else if (players.Count == 1)
		{
            string theTag = players[0].tag;
            if ( theTag == "Player1" || theTag == "Player3") 
            {
				winningTeam = "Team 1 Wins!";
            } else {
                winningTeam = "Team 2 Wins!";
            }
            return true;
        } else if (players.Count == 2) 
        {
            string tag1 = players[0].tag;
            string tag2 = players[1].tag;
            if (tag1 == "Player1" && tag2 == "Player3" ||
                tag1 == "Player3" && tag2 == "Player1") 
            {
                winningTeam = "Team 1 Wins!";
                return true;
			}
			else if (tag1 == "Player2" && tag2 == "Player4" ||
			  tag1 == "Player4" && tag2 == "Player2") 
            {
				winningTeam = "Team 2 Wins!";
				return true;
            } else 
            {
                return false;
            }
        }

        return false;
        ////destory player when they run out of time
        //GameObject player;
        //for (int i = 0; i < players.Count; i++)
        //{
        //    if (!players[i].GetComponent<PlayerTime>().IsPlayerAlive())
        //    {
        //        player = players[i];
        //        players.Remove(players[i]);
        //        Destroy(player);
        //        alivePlayer[i] = false;
        //    }
        //    else
        //    {
        //        alivePlayer[i] = true;
        //    }
        //}

        //if (!alivePlayer[0] && !alivePlayer[1] && !alivePlayer[2] && !alivePlayer[3])
        //{
        //    winningTeam = "Draw";
        //    return true;
        //}
        //else if (!alivePlayer[0] && !alivePlayer[2])
        //{
        //    winningTeam = "Team 1 Wins!";
        //    return true;
        //}
        //else if (!alivePlayer[1] && !alivePlayer[3])
        //{
        //    winningTeam = "Team 2 Wins!";
        //    return true;
        //}
        //return false;
    }

    protected override string VictoryText()
    {
        return winningTeam;
    }
}
