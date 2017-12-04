using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TDMController : GameController 
{

    //default with p1 and p3 on teams and p2 and p4 on teams
    private string winningTeam = "";
	//public Text guiText;

    protected override void GameLogic()
    {
        
    }

    protected override bool VictoryCondition()
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
    }

	private void ShowSuddenDeath(int oneTime) {
		StartCoroutine (ShowMessage ("SUDDEN DEATH!", 2));
	}

	IEnumerator ShowMessage(string message, float delay) {
		guiText.text = message;
		guiText.enabled = true;
		yield return new WaitForSeconds (delay);
		guiText.enabled = false;
	}

    protected override string VictoryText()
    {
        return winningTeam;
    }
}
