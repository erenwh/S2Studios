using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagSpawner : MonoBehaviour {
	public GameObject flag1;
	public GameObject flag2;
	public GameObject goal1;
	public GameObject goal2;
	public int respawn = 0;
	Vector3 flagPosition1 = new Vector3(-4, -3, 1);
	Vector3 flagPosition2 = new Vector3(5, 5, 1);
	Vector3 goalPosition1 = new Vector3(-5, -3, 1);
	Vector3 goalPosition2 = new Vector3(6, 5, 1);
	public int isCTF = 0;

	// Use this for initialization
	void Start () {
		if (Game.Instance.gameModeSelected == 4) {
			isCTF = 1;
		}
		if (isCTF == 1) {
			//local declaration
			GameObject goal;

			Instantiate (flag1, flagPosition1 + transform.TransformPoint (0, 0, 0), gameObject.transform.rotation);
			Instantiate (flag2, flagPosition2 + transform.TransformPoint (0, 0, 0), gameObject.transform.rotation);
			goal = Instantiate (goal1, goalPosition1 + transform.TransformPoint (0, 0, 0), gameObject.transform.rotation);
			goal.GetComponent<FlagGoal> ().fs = this;
			goal = Instantiate (goal2, goalPosition2 + transform.TransformPoint (0, 0, 0), gameObject.transform.rotation);
			goal.GetComponent<FlagGoal> ().fs = this;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (isCTF == 1) {
			if (respawn == 1) {
				Instantiate (flag1, flagPosition1 + transform.TransformPoint (0, 0, 0), gameObject.transform.rotation);
			} else if (respawn == 2) {
				Instantiate (flag2, flagPosition2 + transform.TransformPoint (0, 0, 0), gameObject.transform.rotation);
			}
			respawn = 0;
		}
	}
}
