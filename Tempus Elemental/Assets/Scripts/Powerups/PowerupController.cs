using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupController : MonoBehaviour {

    private GameObject touchedPlayer;       // keep track of the player who touched it
    private int ptype;                      // powerup type | 0: add time, 1: speed up player
    private bool powerupActive;             // powerup is active
    private float speedMultiplier;          // movement speed multiplier
    private float ogspeed;

    private float powerupLengthCounter;     // count how long it has been active
    const string powerupsPath = "Prefabs/Powerups/Powerup_";

	// Use this for initialization
	void Start () {
        ogspeed = touchedPlayer.GetComponent<PlayerMovement>().speed;               // keep track of speed of player
    }

    // Update is called once per frame
    void Update () {
        if (powerupActive)
        {
            powerupLengthCounter -= Time.deltaTime;                                 // only run for that length
            touchedPlayer.GetComponent<PlayerMovement>().speed *= speedMultiplier;  // speed up
            if (powerupLengthCounter <= 0)               
            {
                if (ptype == 1)                                                     // if powerup type is speed up
                {

                    touchedPlayer.GetComponent<PlayerMovement>().speed = ogspeed;   // return to original speed
                }
                powerupActive = false;
            }
        }

    }

    public void AssignPlayer(GameObject player)
    {
        touchedPlayer = player;
    }

    public void ActivatePowerup(int type, float time, float multiplier, int time2add)
    {
        ptype = type;
        powerupLengthCounter = time;
        speedMultiplier = multiplier;

        if (ptype == 0) // if powerup type is add time 
        {
            touchedPlayer.GetComponent<PlayerTime>().timeRemaining += time2add;           // add time
        }
        else            // if different powerup type
        {
            powerupActive = true; 
        }
    }
}
