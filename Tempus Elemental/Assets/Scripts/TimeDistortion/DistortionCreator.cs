using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistortionCreator : MonoBehaviour {

	//constants
	const int SLOWDOWN = 0;
	const int SPEEDUP = 1;
	const int FREEZE = 2;

	//references
	public GameObject[] distortions;			//all the different time distortion prefabs that a player can make

	private PlayerTime pt;

	//variables
	public float timeUsedPerSecond = 0.75f;		//how much extra time is used per second of use (rounded down)?
	public float slowDownFactor = 0.75f;		//how much should other players be slowed down by a slow down time distortion
	public int distortionType = 0;				//based on the constants, what kind of time distortion can the player currently make (default SLOWDOWN)
	private float speedUpFactor = 1.25f;		//how much faster should the player become after using speedup time distortion

	private bool distorting = false;			//is the player currently performing a time distortion
	private GameObject createdDistortion;		//the current distortion created by the player
	private float timeDistorted;				//how long the player has been distorting for

	private float multiplier = 1;					//the distortion multiplier that affects the distortion when multiple powerups are picked up

	// Use this for initialization
	void Start () {
		distorting = false;
		distortionType = SLOWDOWN;
		timeDistorted = 0.0f;
		pt = GetComponent<PlayerTime> ();
		multiplier = 1;
	}

	void applyMultiplier() {
		if (multiplier > 1) {
			switch (distortionType) {
			case SLOWDOWN:
				for (int i = 0; i < multiplier - 1; i++) {
					slowDownFactor *= slowDownFactor;
				}
				break;
			case SPEEDUP:
				for (int i = 0; i < multiplier - 1; i++) {
					speedUpFactor *= speedUpFactor;
				}
				break;
			case FREEZE:
				// not sure how the freeze time distortion would become more powerful when stacked
				break;
			}
		}
	}

	void resetMultiplier() {
		multiplier = 1;
	}
	
	// Update is called once per frame
	void Update () {

		//check what previous type the distortion is and then increase the multiplier


		//create distortion
		//call the applyMultiplier function in here somewhere
		if (!distorting && Input.GetButtonDown("Distort" + gameObject.tag)) {
			distorting = true;
			timeDistorted = 0.0f;
			switch (distortionType) {
			case SLOWDOWN:
				createdDistortion = Instantiate (distortions [SLOWDOWN], transform);
				createdDistortion.GetComponent<TimeSlowDown> ().AssignPlayer (gameObject, slowDownFactor);
				break;
			case SPEEDUP:
				break;
			case FREEZE:
				break;
			}
			//TODO: Add additional distortion types for later Sprints
		}

		//destroy distortion
		if (distorting && Input.GetButtonUp ("Distort" + gameObject.tag)) {
			distorting = false;
			Destroy (createdDistortion);
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
