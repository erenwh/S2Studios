using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagGoal : MonoBehaviour {
	public int goalTeam;
	public Vector3 altPosition;						//allow the flag goals to be reachable on maps 3 and 4
	[HideInInspector] public FlagSpawner fs;


	void Start () {
		if (Game.Instance.mapSelected > 1) {
			transform.position = altPosition;
		}
	}

	void OnTriggerEnter2D (Collider2D coll) {
		if (coll.tag == "Player2" || coll.tag == "Player4" || coll.tag == "Player1" || coll.tag == "Player3") {
			if (coll.GetComponent<PlayerFlags> ().hasFlag == true) {
				if (goalTeam == 0) {
					coll.GetComponent<PlayerFlags> ().hasFlag = false;
					coll.GetComponent<PlayerFlags> ().numFlags++;
					coll.gameObject.GetComponent<PlayerMovement> ().speed = coll.gameObject.GetComponent<PlayerMovement> ().speed * 2;
					if (coll.tag == "Player1" || coll.tag == "Player3") {
						fs.respawn = 2;
					}
				} else if (goalTeam == 1) {
					if (coll.tag == "Player2" || coll.tag == "Player4") {
						fs.respawn = 1;
					}
				}
			}
		}
	}
}
