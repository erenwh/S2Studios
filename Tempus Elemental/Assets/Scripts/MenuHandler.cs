using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{
	//const
	private const int FREEFORALL = 0;
    private const int TEAMDEATHMATCH = 1;
    private const int KINGOFTHEHILL = 2;

	//references
    public Dropdown selectPlayerDropdown;
    public Text selectedAmountPlayers;
	public Text selectedGameModeText;
	public GameController[] gameControllers;
    public Image selectedMap;
    public Sprite dummyMap;
    public Sprite mageCityMap;

    //variables
    public int numGameModes = 3;
	private int selectedGameMode = 0;

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
		selectedGameMode++;
		if (selectedGameMode >= numGameModes) {
			selectedGameMode = 0;
		}
		SetGameModeText ();
	}

	//left arrow selecting the game mode
	public void PrevGameMode () {
		selectedGameMode--;
		if (selectedGameMode < 0) {
			selectedGameMode = numGameModes - 1;
		}
		SetGameModeText ();
	}

	//sets the text in the menu so that the player knows
	void SetGameModeText () {
		switch (selectedGameMode) {
		case FREEFORALL:
			selectedGameModeText.text = "Free For All";
            Game.Instance.GameController = gameControllers[selectedGameMode];
            Game.Instance.gameModeSelected = 0;
            break;
        case TEAMDEATHMATCH:
            selectedGameModeText.text = "Team Death Match";
            Game.Instance.GameController = gameControllers[selectedGameMode];
            Game.Instance.gameModeSelected = 1;
            break;
        case KINGOFTHEHILL:
			selectedGameModeText.text = "King of the Hell";
            Game.Instance.GameController = gameControllers[selectedGameMode];
            Game.Instance.gameModeSelected = 2;
            break;
		}
	}

    public void ChangeMap ()
    {
        if (selectedMap.sprite == dummyMap)
        {
            selectedMap.sprite = mageCityMap;
            selectedMap.color = Color.gray;
            Game.Instance.mapSelected = 1;
        }
        else if (selectedMap.sprite == mageCityMap)
        {
            selectedMap.sprite = dummyMap;
            selectedMap.color = Color.magenta;
            Game.Instance.mapSelected = 2;
        }
    }

    public void SelectPlayer(int index)
    {
        selectedAmountPlayers.text = numPlayers[index];
        Game.Instance.numPlayers = 4 - index;
        Debug.Log("index " + (4 - index));
    }

    public void changeScenes()
    {
        if (Game.Instance.GameController == null) {
            
            Game.Instance.GameController = gameControllers[selectedGameMode];
        }
        SceneManager.LoadScene("Main");
    }
}
