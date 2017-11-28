using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public abstract class GameController : MonoBehaviour
{
    public int victoryMessageWaitInSeconds = 3;
    private float victoryMessageWaitInRealTime = 0;

    public int numPlayers = 0;

    private bool isFinishedState = false;

    public bool isStarted = false;

    abstract protected bool VictoryCondition();
    abstract protected void GameLogic();
    abstract protected string VictoryText();

    private GameObject victoryMessage;
    private GameObject inGameMenu;

    protected List<GameObject> players;


    public void BackToMenu() 
    {
        SceneManager.LoadScene("Menu");
        ResetValues();
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
        victoryMessage = null;
        victoryMessageWaitInRealTime = float.PositiveInfinity;
    }

    private void ShowVictoryMessage() 
    {
        victoryMessage.SetActive(true);
		victoryMessage.GetComponentInChildren<Text>().text =
			VictoryText() + "\n Press any key to continue!";
		if (victoryMessageWaitInRealTime > Time.realtimeSinceStartup)
		{
			victoryMessage.GetComponentInChildren<Text>().text +=
			    " (in " + Math.Ceiling(victoryMessageWaitInRealTime -
                Time.realtimeSinceStartup) + " )";
		}
    }

    virtual public void OnStart()
    {
        victoryMessageWaitInRealTime = float.PositiveInfinity;
        //performing actions to switch the maps
        //Find the maps' gameObjects
        GameObject map1, map2, map3;
        map1 = GameObject.Find("DummyMap");
        map2 = GameObject.Find("DummyMap2");
        map3 = GameObject.Find("Dynamic Map Walls");
        Utils.SelectMap(map1, map2, map3);

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

        victoryMessage = GameObject.FindWithTag("VictoryMessage");
        victoryMessage.SetActive(false);
		inGameMenu = GameObject.FindWithTag("InGameMenu");
		inGameMenu.SetActive(false);
        isStarted = true;
        isFinishedState = false;
    }

    public void OnUpdate() 
    {
        

        if (isFinishedState) {
            ShowVictoryMessage();
            if (victoryMessageWaitInRealTime < Time.realtimeSinceStartup && Input.anyKey) {
                BackToMenu();    
            }
            return;
        }

		if (VictoryCondition())
		{
            Time.timeScale = 0;
            victoryMessageWaitInRealTime = Time.realtimeSinceStartup + victoryMessageWaitInSeconds;
            isFinishedState = true;
			ShowVictoryMessage();
			return;
		}

        if (Input.GetButtonDown("Pause"))
        {
            inGameMenu.GetComponent<InGameMenuHandler>().ToggleState();
        }
			
		GameLogic ();
    }

    virtual public void KillPlayer(GameObject player)
    {
        players.Remove(player);
        player.SetActive(false);
    }

}