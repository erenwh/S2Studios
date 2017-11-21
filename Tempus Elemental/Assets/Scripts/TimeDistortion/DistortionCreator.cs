using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistortionCreator : MonoBehaviour 
{

    //references
    public GameObject[] distortions;			//all the different time distortion prefabs that a player can make
    public Image distortionIndicator;           //indicator of current distortion
    public Sprite[] distortionSprites;          //sprites for each distortion powerup

	private PlayerTime pt;

	//variables
	public float timeUsedPerSecond = 0.75f;		//how much extra time is used per second of use (rounded down)?
	public float slowDownFactor = 0.5f;		    //how much should other players be slowed down by a slow down time distortion
    public DistortionType distortionType = DistortionType.SlowDown;				//based on the constants, what kind of time distortion can the player currently make (default SLOWDOWN)
	public float speedUpFactor = 2f;		    //how much faster should the player become after using speedup time distortion
	public float freezeRadius = 0.5f;		    //how big is the freeze time distortion

	private bool distorting = false;			//is the player currently performing a time distortion
	private GameObject createdDistortion;		//the current distortion created by the player
	private float timeDistorted;				//how long the player has been distorting for

	private float multiplier = 1;				//the distortion multiplier that affects the distortion when multiple powerups are picked up

	// Use this for initialization
	void Start () 
    {
		distorting = false;
        distortionType = DistortionType.SpeedUp;
		timeDistorted = 0.0f;
		pt = GetComponent<PlayerTime> ();
		multiplier = 1;
	}

	void applyMultiplier() {
		if (multiplier > 1) {
			switch (distortionType) {
                case DistortionType.SlowDown:
				for (int i = 0; i < multiplier - 1; i++) {
					slowDownFactor *= slowDownFactor;
				}
				break;
                case DistortionType.SpeedUp:
				for (int i = 0; i < multiplier - 1; i++) {
					speedUpFactor *= speedUpFactor;
				}
				break;
                case DistortionType.Freeze:
				// Brian: not sure how the freeze time distortion would become more powerful when stacked 
				// Parr: We could make the Radius bigger?
				for (int i = 0; i < multiplier - 1; i++) {
					freezeRadius += freezeRadius;
				}
				break;
			}
		}
	}

	void resetMultiplier() {
		multiplier = 1;
	}

	// check and then start a distortion
	public void Distort () {
		if (distorting) {
			return;
		}
		distorting = true;
		timeDistorted = 0.0f;
        createdDistortion = Instantiate(distortions[(int)distortionType], transform);
		switch (distortionType) {
            case DistortionType.SlowDown:
			    createdDistortion.GetComponent<TimeSlowDown> ().AssignPlayer (gameObject, slowDownFactor);
			    break;
            case DistortionType.SpeedUp:			
			    createdDistortion.GetComponent<TimeSpeedUp> ().AssignPlayer (gameObject, speedUpFactor);
			    break;
            case DistortionType.Freeze:			
			createdDistortion.GetComponent<TimeFreeze> ().AssignPlayer (gameObject, freezeRadius);
			    break;
            case DistortionType.Reverse:			
			    break;
		}
	}

	// end the running distortion
	public void EndDistortion () 
    {
		if (!distorting) {
			return;
		}
		distorting = false;
		Destroy (createdDistortion);
	}

	// Update is called once per frame
	void Update () 
    {
		//creating and ending distortions are now in PlayerMovement to account for the dash mechanic

		//call the applyMultiplier function in here somewhere
		if (!distorting && Input.GetButtonDown("Distort" + gameObject.tag)) {

		}

		//cost to distort
		if (distorting) {
			timeDistorted += (Time.deltaTime * timeUsedPerSecond);
			if (timeDistorted >= 1.0f) {
				pt.DecrementTime (1);
				timeDistorted = 0.0f;
			}
		}
	}
}
