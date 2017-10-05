using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupController : MonoBehaviour {

    private GameObject touchedPlayer;       // keep track of the player who touched it
    private int ptype;                      // powerup type | 0: add time, 1: speed up player
    private bool powerupActive;             // powerup is active
    private float speedMultiplier;          // movement speed multiplier
    private float ogspeed;
    private int flag = 0;

    private float powerupLengthCounter;     // count how long it has been active

	// Use this for initialization
	void Start () {
    }

    // Update is called once per frame
    void Update () {
        if (powerupActive)
        {
            if (flag == 0)
            {
                touchedPlayer.GetComponent<PlayerMovement>().speed *= speedMultiplier;  // speed up
                flag = 1;
            }
            powerupLengthCounter -= Time.deltaTime;                                 // only run for that length
            
            if (powerupLengthCounter <= 0)               
            {
                if (ptype == 1)                                                     // if powerup type is speed up
                {

                    touchedPlayer.GetComponent<PlayerMovement>().speed = ogspeed;   // return to original speed
                }
                powerupActive = false;
                flag = 0;
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
        ogspeed = touchedPlayer.GetComponent<PlayerMovement>().speed;               // keep track of speed of player

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
