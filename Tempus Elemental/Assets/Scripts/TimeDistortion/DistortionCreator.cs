using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistortionCreator : MonoBehaviour {

	//constants
	const int SLOWDOWN = 0;

	//references
	public GameObject[] distortions;			//all the different time distortion prefabs that a player can make

	private PlayerTime pt;

	//variables
	public float timeUsedPerSecond = 0.75f;		//how much extra time is used per second of use (rounded down)?
	public float slowDownFactor = 0.75f;		//how much should other players be slowed down by a slow down time distortion
	public int distortionType = 0;				//based on the constants, what kind of time distortion can the player currently make (default SLOWDOWN)

	private bool distorting = false;			//is the player currently performing a time distortion
	private GameObject createdDistortion;		//the current distortion created by the player
	private float timeDistorted;				//how long the player has been distorting for

	// Use this for initialization
	void Start () {
		distorting = false;
		distortionType = SLOWDOWN;
		timeDistorted = 0.0f;
		pt = GetComponent<PlayerTime> ();
	}
	
	// Update is called once per frame
	void Update () {

		//create distortion
		if (!distorting && Input.GetButtonDown("Distort" + gameObject.tag)) {
			distorting = true;
			timeDistorted = 0.0f;
			switch (distortionType) {
			case SLOWDOWN:
				createdDistortion = Instantiate (distortions [SLOWDOWN], transform);
				createdDistortion.GetComponent<TimeSlowDown> ().AssignPlayer (gameObject, slowDownFactor);
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
