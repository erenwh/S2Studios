using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttack : MonoBehaviour {

    public int damage = 5; //damage default to remove 2 seconds of time from player
    private int thisTime;
    private bool isAttacking = false;
    private Collider2D playerAttacked;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire" + gameObject.tag))
        {
            if (playerAttacked.CompareTag("Player1") || playerAttacked.CompareTag("Player2") || playerAttacked.CompareTag("Player3") || playerAttacked.CompareTag("Player4"))
            {
                playerAttacked.gameObject.GetComponent<PlayerTime>().timeRemaining -= damage;
                gameObject.GetComponent<PlayerTime>().timeRemaining += damage;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        // controller.AssignPlayer(coll.gameObject);
        // controller.ActivatePowerup(ptype, powerupLength, speedMultiplier, amountTimeAdd);
        isAttacking = true;
        playerAttacked = coll;
    }
}
