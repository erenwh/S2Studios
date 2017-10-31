using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{
    public Dropdown selectPlayerDropdown;
    public Text selectedAmountPlayers;

    private List<string> numPlayers = 
        new List<string>() { 
        "4 players selected", 
        "3 players selected", 
        "2 players selected" };
	
	public void OnGameSetup()
    {
		//Debug.Log ("Hi Eli");
	}

	//right arrow selecting the game mode
	public void NextGameMode () {
		
	}

	//left arrow selecting the game mode
	public void PrevGameMode () {
		
	}

    public void SelectPlayer(int index)
    {
        selectedAmountPlayers.text = numPlayers[index];
        Game.Instance.numPlayers = 4 - index;
    }

    public void changeScenes()
    {
        SceneManager.LoadScene("Main");
    }
}
