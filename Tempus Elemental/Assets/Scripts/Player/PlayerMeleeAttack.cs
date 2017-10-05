using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttack : MonoBehaviour {

    public int damage = 5; //damage default to remove 2 seconds of time from player
    private int thisTime;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire" + gameObject.tag))
        {

        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        // controller.AssignPlayer(coll.gameObject);
        // controller.ActivatePowerup(ptype, powerupLength, speedMultiplier, amountTimeAdd);
        if (coll.CompareTag("Player1") || coll.CompareTag("Player2") || coll.CompareTag("Player3") || coll.CompareTag("Player4"))
        {
            coll.gameObject.GetComponent<PlayerTime>().timeRemaining -= damage;
            //gameObject.GetComponent<PlayerTime>().timeRemaining += damage;
        }
    }
}
