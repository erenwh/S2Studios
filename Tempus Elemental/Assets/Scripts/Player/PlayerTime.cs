using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerTime : MonoBehaviour {

	public int startingTime; 			// changing to nothing so we may keep changing it as needed
	public int timeRemaining;			// how much time does this player have left?
	public Slider timeIndicator;
	public Text timeText;
    public Image radialIndicator;
	public int TimeRemaining {
		get {
			return this.timeRemaining;
		}
	}
	// Takes a specified amount of time from a player and gives it to another player.
	public static void TransferTime (int amount, GameObject playerFrom, GameObject playerTo) {
        //variable and check to prevent negative values 
        int difference = playerFrom.GetComponent<PlayerTime>().timeRemaining - amount;
        if (difference < 0) difference = 0;
		if (playerFrom != null) {
			playerFrom.GetComponent<PlayerTime> ().timeRemaining = difference;
			playerFrom.GetComponent<PlayerTime> ().timeIndicator.value = playerFrom.GetComponent<PlayerTime> ().timeRemaining;
			playerFrom.GetComponent<PlayerTime> ().timeText.text = playerFrom.GetComponent<PlayerTime> ().timeRemaining.ToString ();
            playerFrom.GetComponent<PlayerTime>().radialIndicator.fillAmount = (float)playerFrom.GetComponent<PlayerTime> ().TimeRemaining / Game.Instance.playersStartingTime;
		}
		if (playerTo != null) {
			playerTo.GetComponent<PlayerTime> ().timeRemaining += amount;
			playerTo.GetComponent<PlayerTime> ().timeIndicator.value = playerTo.GetComponent<PlayerTime> ().timeRemaining;
			playerTo.GetComponent<PlayerTime> ().timeText.text = playerTo.GetComponent<PlayerTime> ().timeRemaining.ToString ();
            playerTo.GetComponent<PlayerTime>().radialIndicator.fillAmount = (float)playerTo.GetComponent<PlayerTime>().TimeRemaining / Game.Instance.playersStartingTime;
        }
	}

	// Decrements the timeRemaining of this player.
	public void DecrementTime (int timeLost) {
		timeRemaining -= timeLost;
		timeIndicator.value = timeRemaining;
		timeText.text = timeRemaining.ToString ();
        radialIndicator.fillAmount = (float)TimeRemaining / Game.Instance.playersStartingTime;
	}

	// Adds to the timeRemaining of this player.
	
	public void AddTime (int timeGained) {
		timeRemaining += timeGained;
		timeIndicator.value = timeRemaining;
		timeText.text = timeRemaining.ToString ();
	}

    private void Awake() //added awake because start didn't update fast enough
    {
        //Assign player starting time from slider in settings from game object
        startingTime = Game.Instance.playersStartingTime;
    }

    // Use this for initialization
    void Start ()
    {
        //Assuming player's begin immediately after spawning
		BeginCountdown();
	}

    public bool IsPlayerAlive() {
        if (timeRemaining < 1) {
            return false;
        }
        return true;
    }

	
	// Has the player's time start its 1 per second decrement, and resets the initial time to whatever it should start with. 
	void BeginCountdown () {
		timeRemaining = startingTime;
		StartCoroutine("DecrementOverTime");
	}

	// Decrements the timeRemaining by 1 every second, calls relevant death methods upon going under 1 second of timeRemaining.
	IEnumerator DecrementOverTime () {
		while (true) {
			yield return new WaitForSeconds (1f);
			if (timeRemaining > 0) DecrementTime (1); //fix to prevent numbers going negative
			if (timeRemaining < 1) {
                //Call Death animation & play sfx
                //let game controller know
                Game.Instance.GameController.KillPlayer(gameObject);
            }
		}
	}
}
