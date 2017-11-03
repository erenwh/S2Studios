using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerTime : MonoBehaviour {

	public int startingTime; 			// changing to nothing so we may keep changing it as needed
	public int timeRemaining;			// how much time does this player have left?
	public Slider timeIndicator;
	public Text timeText;
	public int TimeRemaining {
		get {
			return this.timeRemaining;
		}
	}
	// Takes a specified amount of time from a player and gives it to another player.
	public static void TransferTime (int amount, GameObject playerFrom, GameObject playerTo) {
		if (playerFrom != null && playerFrom.GetComponent<PlayerTime>().timeRemaining > 0) {
			playerFrom.GetComponent<PlayerTime> ().timeRemaining -= amount;
			playerFrom.GetComponent<PlayerTime> ().timeIndicator.value = playerFrom.GetComponent<PlayerTime> ().timeRemaining;
			playerFrom.GetComponent<PlayerTime> ().timeText.text = playerFrom.GetComponent<PlayerTime> ().timeRemaining.ToString ();
		}
		if (playerTo != null && playerFrom.GetComponent<PlayerTime>().timeRemaining > 0) {
			playerTo.GetComponent<PlayerTime> ().timeRemaining += amount;
			playerTo.GetComponent<PlayerTime> ().timeIndicator.value = playerTo.GetComponent<PlayerTime> ().timeRemaining;
			playerTo.GetComponent<PlayerTime> ().timeText.text = playerTo.GetComponent<PlayerTime> ().timeRemaining.ToString ();
		}
	}

	// Decrements the timeRemaining of this player.
	public void DecrementTime (int timeLost) {
		timeRemaining -= timeLost;
		timeIndicator.value = timeRemaining;
		timeText.text = timeRemaining.ToString ();
	}

	// Adds to the timeRemaining of this player.
	
	public void AddTime (int timeGained) {
		timeRemaining += timeGained;
		timeIndicator.value = timeRemaining;
		timeText.text = timeRemaining.ToString ();
	}

	// Use this for initialization
	void Start () {
		//Assuming player's begin immediately after spawning
		BeginCountdown();

        //Destroy Players if GameController has less than 4
        //if (Game.Instance.numPlayers < 4)
        //{
        //    GameObject player4 = GameObject.Find("Player4");
        //    GameObject player3 = GameObject.Find("Player3");
        //    if (Game.Instance.numPlayers == 2)
        //    {
        //        Destroy(player3);
        //        Destroy(player4);
        //    }
        //    else if (Game.Instance.numPlayers == 3)
        //    {
        //        Destroy(player4);
        //    }
        //    else
        //    {
        //        Debug.LogError("GameController's number of players is invalid. Less than four but not 2 or 3.");
        //    }
        //}
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
