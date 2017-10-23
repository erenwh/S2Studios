using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{
    public Dropdown selectPlayerDropdown;
    public Text selectedAmountPlayers;

    List<string> numPlayers = new List<string>() { "4 players selected", "3 players selected", "2 players selected" };

	// Use this for initialization
	void Start ()
    {

	}
	
	public void OnGameSetup()
    {
		//Debug.Log ("Hi Eli");
	}

    public void SelectPlayer(int index)
    {
        selectedAmountPlayers.text = numPlayers[index];
        GameController.numPlayers = 4 - index;
    }

    public void changeScenes()
    {
        Application.LoadLevel("Main");
    }
}
