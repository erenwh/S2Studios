using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public abstract class GameController : MonoBehaviour 
{

    public int numPlayers = 0;

    private bool isFinishedState = false;

    public bool isStarted = false;

    abstract public bool VictoryCondition();
    abstract public void SpawnPlayers();
    abstract public void SpawnObjects();
    abstract public void UpdatePoints();
    abstract protected string VictoryText();


    protected List<GameObject> players;


    private void BackToMenu() 
    {
        SceneManager.LoadScene("Menu");
        Destroy(this);
    }

    private void ShowVictoryMessage() 
    {
        Time.timeScale = 0;
        GameObject.FindWithTag("VictoryMessage").SetActive(true);
        GameObject.FindWithTag("VictoryMessageTxt").GetComponent<Text>().text = 
            VictoryText() + "\n Press any key to continue!";
        isFinishedState = true;
    }

    public void OnStart()
    {
        // settings the players
        numPlayers = Game.Instance.numPlayers;
        players = new List<GameObject>();

		for (int i = 1; i < 5; i++)
		{
			GameObject g = GameObject.FindGameObjectWithTag("Player" + i );
			players.Add(g);
		}

		if (Game.Instance.numPlayers < 4)
		{
            GameObject player;
		    if (Game.Instance.numPlayers == 2)
		    {
                player = players[3];
                players.RemoveAt(3);
		        Destroy(player);

				player = players[2];
				players.RemoveAt(2);
				Destroy(player);
		    }
		    else if (Game.Instance.numPlayers == 3)
		    {
				player = players[3];
				players.RemoveAt(3);
				Destroy(player);
		    }
		    else
		    {
		        Debug.LogError("GameController's number of players is invalid. Less than four but not 2 or 3.");
		    }
		}
        Debug.Log(players.Count);
        // 

        isStarted = true;
    }

    public void Update() 
    {
        if (isFinishedState && Input.anyKey) {
            BackToMenu();
            return;
        }

        if (VictoryCondition()) 
        {
            ShowVictoryMessage();
            return;
        }

        UpdatePoints();
    }
}
