using UnityEngine;

public class PowerupController : MonoBehaviour 
{
    private GameObject touchedPlayer;       // keep track of the player who touched it
    private int ptype;                      // powerup type | 0: add time, 1: speed up player, 2: freeze time, 3: slow down time
    private bool powerupActive;             // powerup is active
    private float speedMultiplier;          // movement speed multiplier
    private float ogspeed;
    private int flag = 0;

    private float powerupLengthCounter;     // count how long it has been active

    void Update () 
    {
        if (powerupActive)
        {
            if (flag == 0)
            {
				if (ptype == 0) { // have to change this so that later it changes the distortion instead of the powerup changing the properties
					touchedPlayer.GetComponent<DistortionCreator>().distortionType = 0;
					flag = 1;
				}
                else if (ptype == 1) // have to change this so that later it changes the distortion instead of the powerup changing the properties
                {
                    // touchedPlayer.GetComponent<PlayerMovement>().speed += speedMultiplier;  // speed up
                    touchedPlayer.GetComponent<DistortionCreator>().distortionType = 1;
                    flag = 1;
                }
                else if (ptype == 2) // have to change this so that later it changes the distortion instead of the powerup changing the properties
                {   // freeze time
                    touchedPlayer.GetComponent<DistortionCreator>().distortionType = 2;
                    flag = 1;
                }
            }
            /*powerupLengthCounter -= Time.deltaTime;                                 // only run for that length
            
            if (powerupLengthCounter <= 0)               
            {
                if (ptype == 1)                                                     // if powerup type is speed up
                {
                    touchedPlayer.GetComponent<PlayerMovement>().speed = ogspeed;   // return to original speed
                }
                else if (ptype == 2)
                {

                }
                else if (ptype == 3)
                {

                }
                powerupActive = false;
                flag = 0;
            }*/
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

        powerupActive = true;

        /*if (ptype == 0) // if powerup type is add time 
        {
			touchedPlayer.GetComponent<PlayerTime>().AddTime(time2add);           // add time
        }
        else            // if different powerup type
        {
            powerupActive = true; 
        }*/
    }
}
