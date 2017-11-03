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
        ResetValues();
        SceneManager.LoadScene("Menu");
    }

    private void ResetValues() {
		//Reset global variables for game
		Game.Instance.numPlayers = 4;
		Game.Instance.mapSelected = 1;
		Game.Instance.gameModeSelected = 0;

		//Reset global variables for gamecontroller
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

        ////disabling renderer instead of setting inactive so we don't have to store reference
        //GameObject.Find("Victory Background").GetComponent<Renderer>().enabled = true;
        //GameObject.Find("Victory Message Txt").GetComponent<Text>().enabled = true;
        //GameObject.FindWithTag("VictoryMessageTxt").GetComponent<Text>().text = 

            VictoryText() + "\n Press any key to continue!";
        isFinishedState = true;
    }

    public void OnStart()
    {
        //reset bools for gamecontroller
        isFinishedState = false;
        isStarted = false;

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

		//make sure that the game controller only does upate functionality when Main scene is active
		//if (SceneManager.GetActiveScene().name == "Main")
		if (VictoryCondition()) 
        {
            if (isFinishedState && Input.anyKey)
            {
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

    public void KillPlayer(GameObject player)
    {
        players.Remove(player);
        player.SetActive(false);
    }
}
