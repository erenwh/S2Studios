using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public abstract class GameController : MonoBehaviour 
{

    private bool isFinishedState = false;

    abstract public bool VictoryCondition();
    abstract public void SpawnPlayers();
    abstract public void SpawnObjects();
    abstract public void UpdatePoints();
    abstract protected string VictoryText();


    protected List<GameObject> players;

    public GameController() {
        players = new List<GameObject>();
    }

    private void BackToMenu() {
        SceneManager.LoadScene("Menu");
    }

    private void ShowVictoryMessage() {
        Time.timeScale = 0;
        GameObject.FindWithTag("VictoryMessage").SetActive(true);
        GameObject.FindWithTag("VictoryMessageTxt").GetComponent<Text>().text = 
            VictoryText() + "\n Press any key to continue!";
        isFinishedState = true;
    }

    public void StartGame() 
    {
        SpawnPlayers();
        SpawnObjects();
    }

    void Update() 
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

        SpawnPlayers();
        SpawnObjects();

        UpdatePoints();
    }
}
