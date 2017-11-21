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
	private const int FILLTHEBAR = 3;

	//references
    public Dropdown selectPlayerDropdown;
    public Text selectedAmountPlayers;
	public Text selectedGameModeText;
    public Text playerStartingTimeText;
    public GameController[] gameControllers;
    public Image selectedMap;
    public Sprite dummyMap;
    public Sprite mageCityMap;

    public Text selectControl;
    
    private List<string> gameControls = 
        new List<string>() { 
        "Keyboard 1", 
        "Keyboard 2", 
        "Joystick 1",
        "Joystick 2" };    

    //variables
    public int numGameModes = 4;
	private int selectedGameMode = 0;

    private List<string> numPlayers = 
        new List<string>() { 
        "4 players selected", 
        "3 players selected", 
        "2 players selected" };

    private const string startingTimeText = "Players' starting time is: ";


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
			selectedGameModeText.text = "King of the Hill";
            Game.Instance.GameController = gameControllers[selectedGameMode];
            Game.Instance.gameModeSelected = 2;
            break;
		case FILLTHEBAR:
			selectedGameModeText.text = "Fill the Bar";
			Game.Instance.GameController = gameControllers[selectedGameMode];
			Game.Instance.gameModeSelected = 3;
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
    }

    public void changeScenes()
    {
        if (Game.Instance.GameController == null) {
            
            Game.Instance.GameController = gameControllers[selectedGameMode];
        }
        SceneManager.LoadScene("Main");
    }

    public void ChangePlayerStartingTime(Slider slider)
    {
        int startingTime = (int)slider.value;
        playerStartingTimeText.text = startingTimeText + startingTime;
        Game.Instance.playersStartingTime = startingTime;
        Debug.Log("players' starting time " + startingTime);
    }

    public void PlayerControlSwitch(string tag)
    {
        int playerNum = 0;
        switch (tag)
        {
            case "player1":
                playerNum = 1;
                break;
            case "Player2":
                playerNum = 2;
                break;
            case "Player3":
                playerNum = 3;
                break;
            case "Player4":
                playerNum = 4;
                break;
            default:
                break;
        }

        selectControl.text = gameControls[playerNum];
    }
}
