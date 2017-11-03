using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

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

    private GameObject victoryMessage;

    protected List<GameObject> players;


    private void BackToMenu() 
    {
        SceneManager.LoadScene("Menu");
        numPlayers = 0;
        isFinishedState = false;
        isStarted = false;
        Time.timeScale = 1;
    }

    private void ShowVictoryMessage() 
    {
        Time.timeScale = 0;
        victoryMessage.SetActive(true);
        victoryMessage.GetComponentInChildren<Text>().text = 
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
		        player.SetActive(false);

				player = players[2];
				players.RemoveAt(2);
				player.SetActive(false);
		    }
		    else if (Game.Instance.numPlayers == 3)
		    {
				player = players[3];
				players.RemoveAt(3);
				player.SetActive(false);
		    }
		    else
		    {
		        Debug.LogError("GameController's number of players is invalid. Less than four but not 2 or 3.");
		    }
		}
        Debug.Log(players.Count);
        // 

        victoryMessage = GameObject.FindWithTag("VictoryMessage");
        victoryMessage.SetActive(false);
        isStarted = true;
    }

    public void Update() 
    {
        if (isFinishedState) {
            if (Input.anyKey) {
                BackToMenu();    
            }
            return;
        }

        if (VictoryCondition()) 
        {
            ShowVictoryMessage();
            return;
        }

        UpdatePoints();
    }

    public void KillPlayer(GameObject player)
    {
        players.Remove(player);
        player.SetActive(false);
    }
}
