using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour {

    public int ptype;
    public float powerupLength;
    public int amountTimeAdd;
    public float speedMultiplier;

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
        if (coll.CompareTag("Player"))
        {
            controller.ActivatePowerup(ptype, powerupLength, speedMultiplier, amountTimeAdd);
        }

        gameObject.SetActive(false);
    }
}
