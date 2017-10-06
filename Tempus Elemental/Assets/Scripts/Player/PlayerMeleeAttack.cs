using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttack : MonoBehaviour {

    public int damage = 1; //damage default to remove 1 seconds of time from player
    public float timeBtwnAttacks = 2.33f;
    //private int thisTime;
    private Collider2D playerAttacked;
    private bool hasAttacked = false;
    private float delay;

	// Use this for initialization
	void Start () {
        hasAttacked = false;
	}

    // Update is called once per frame
    void Update() {
        if (playerAttacked == null)
        {
            return;
        }

        if (Input.GetButton("Fire" + gameObject.tag) && !hasAttacked)
        {
            Attack();
        }

        if (Input.GetButtonUp("Fire" + gameObject.tag))
        {
            hasAttacked = false;
        }
    }

    void Attack()
    {
        hasAttacked = true;
        if (playerAttacked.CompareTag("Player1") || playerAttacked.CompareTag("Player2") || playerAttacked.CompareTag("Player3") || playerAttacked.CompareTag("Player4"))
        {
			PlayerTime.TransferTime (damage, playerAttacked.gameObject, gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        // controller.AssignPlayer(coll.gameObject);
        // controller.ActivatePowerup(ptype, powerupLength, speedMultiplier, amountTimeAdd);
        playerAttacked = coll;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerAttacked = null;
    }
}
