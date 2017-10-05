using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour {

    public int ptype;
    public float powerupLength;
    public float speedMultiplier;
    public int amountTimeAdd;

    private PowerupController controller;

    // Use this for initialization
    void Start () {
        controller = FindObjectOfType<PowerupController>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        controller.AssignPlayer(coll.gameObject);
        controller.ActivatePowerup(ptype, powerupLength, speedMultiplier, amountTimeAdd);
        /*if (coll.CompareTag("Player*"))
        {
            controller.AssignPlayer(coll.gameObject);
            controller.ActivatePowerup(ptype, powerupLength, speedMultiplier, amountTimeAdd);
        }*/

        gameObject.SetActive(false);
    }
}
