using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KOTHZone : MonoBehaviour {

	//keep track of players in zone
	void OnTriggerEnter2D (Collider2D coll) {
		if (coll.tag == "Player1" || coll.tag == "Player2" || coll.tag == "Player3" || coll.tag == "Player4") {
			((KOTHController) Game.Instance.GameController).PlayerEnteredZone (coll.gameObject);
		}
	}

	//keep track of players leaving zone
	void OnTriggerExit2D (Collider2D coll) {
		if (coll.tag == "Player1" || coll.tag == "Player2" || coll.tag == "Player3" || coll.tag == "Player4") {
			((KOTHController) Game.Instance.GameController).PlayerLeftZone (coll.gameObject);
		}
	}
}
