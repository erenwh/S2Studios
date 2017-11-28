using UnityEngine;
using UnityEngine.UI;

public class PlayerTime : MonoBehaviour {

	private float timeRemaining;			// how much time does this player have left?
    public int TimeRemaining
    {
        get
        {
            if (timeRemaining < 0)
            {
                return 0;
            }
            else
            {
                return Mathf.RoundToInt(timeRemaining);
            }
        }
        set
        {
            timeRemaining = value;
        }
    }

    // visual cues
	public Slider timeIndicator;
	public Text timeText;
    public Image radialIndicator;

    // added awake because start didn't update fast enough
    void Awake() 
    {
        //Assign player starting time from slider in settings from game object
        timeRemaining = Game.Instance.playersStartingTime;
    }

    void Update()
    {
        DecrementTime(Time.deltaTime);
        if (TimeRemaining == 0) 
        {
			//string y = GetComponent<MenuHandler> ().selectedGameModeText;
			if (Game.Instance.gameModeSelected == 4) {
				if (gameObject.GetComponent<PlayerFlags> ().hasFlag == true) {
					gameObject.GetComponent<PlayerMovement> ().speed = gameObject.GetComponent<PlayerMovement> ().speed * 2;
					gameObject.GetComponent<PlayerFlags> ().hasFlag = false;
				}
			}
            //let game controller know
            Game.Instance.GameController.KillPlayer(gameObject);
        }
    }

    // Decrements the timeRemaining of this player.
    public void DecrementTime(float timeLost)
    {
        timeRemaining -= timeLost;
        if (timeRemaining < 0)
        {
            timeRemaining = 0;
        }
        timeIndicator.value = timeRemaining;
        timeText.text = TimeRemaining.ToString();
        radialIndicator.fillAmount = timeRemaining / Game.Instance.playersStartingTime;
    }

    // Adds to the timeRemaining of this player.

    public void AddTime(int timeGained)
    {
        timeRemaining += timeGained;
        timeIndicator.value = timeRemaining;
        timeText.text = TimeRemaining.ToString();
        radialIndicator.fillAmount = timeRemaining / Game.Instance.playersStartingTime;
    }

    // Takes a specified amount of time from a player and gives it to another player.
    public static void TransferTime(int amount, GameObject playerFrom, GameObject playerTo)
    {
        if (playerFrom != null)
        {
            playerFrom.GetComponent<PlayerTime>().DecrementTime(amount);
        }
        if (playerTo != null)
        {
            playerTo.GetComponent<PlayerTime>().AddTime(amount);
        }
    }
}
