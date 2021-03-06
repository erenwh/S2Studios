﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour {
	public int flagTeam;
	public Vector3 altPosition;			//used on maps 3 and 4
	private FlagSpawner fs;

	void Start () {
		if (Game.Instance.mapSelected > 1) {
			transform.position = altPosition;
		}
		fs = GameObject.FindGameObjectWithTag ("FlagSpawner").GetComponent<FlagSpawner>();
	}

	void OnTriggerEnter2D (Collider2D coll) {
		if (coll.tag == "Player2" || coll.tag == "Player4" || coll.tag == "Player1" || coll.tag == "Player3") {
			if (flagTeam == 0) {
				if (coll.tag == "Player2" || coll.tag == "Player4") {
					coll.GetComponent<PlayerFlags> ().hasFlag = true;
					coll.GetComponent<PlayerFlags> ().holdingFlag (coll.gameObject);
					Destroy (gameObject);
				} else if (coll.tag == "Player1" || coll.tag == "Player3") {
					Destroy (gameObject);
					fs.respawn = 1;
				}
			} else if (flagTeam == 1) {
				if (coll.tag == "Player3" || coll.tag == "Player1") {
					coll.GetComponent<PlayerFlags> ().hasFlag = true;
					coll.GetComponent<PlayerFlags> ().holdingFlag (coll.gameObject);
					Destroy (gameObject);
				} else if (coll.tag == "Player2" || coll.tag == "Player4") {
					Destroy (gameObject);
					fs.respawn = 2;
				}
			}
		}
		//Destroy (gameObject);
	}
}
