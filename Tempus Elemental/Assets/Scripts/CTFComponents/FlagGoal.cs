using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagGoal : MonoBehaviour {
	public int goalTeam;
	[HideInInspector] public FlagSpawner fs;

	void OnTriggerEnter2D (Collider2D coll) {
		if (coll.tag == "Player2" || coll.tag == "Player4" || coll.tag == "Player1" || coll.tag == "Player3") {
			if (coll.GetComponent<PlayerFlags> ().hasFlag == true) {
				if (goalTeam == 0) {
					if (coll.tag == "Player1" || coll.tag == "Player3") {
						coll.GetComponent<PlayerFlags> ().hasFlag = false;
						coll.GetComponent<PlayerFlags> ().numFlags++;
						coll.gameObject.GetComponent<PlayerMovement> ().speed = coll.gameObject.GetComponent<PlayerMovement> ().speed * 2;
						//Game.Instance.GetComponent<FlagSpawner> ().respawn = 1;
						//FindObjectOfType<FlagSpawner> ().respawn = 1;
						//GetComponent<FlagSpawner> ().respawn = 1;
						fs.respawn = 2;
					}
				} else if (goalTeam == 1) {
					if (coll.tag == "Player2" || coll.tag == "Player4") {
						coll.GetComponent<PlayerFlags> ().hasFlag = false;
						coll.GetComponent<PlayerFlags> ().numFlags++;
						coll.gameObject.GetComponent<PlayerMovement> ().speed = coll.gameObject.GetComponent<PlayerMovement> ().speed * 2;
						//FindObjectOfType<FlagSpawner> ().respawn = 2;
						fs.respawn = 1;
					}
				}
			}
		}
	}
}
