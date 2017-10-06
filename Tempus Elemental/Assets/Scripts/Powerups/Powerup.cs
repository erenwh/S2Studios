using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour {

    public int ptype;
    public float powerupLength;
    public float speedMultiplier;
    public int amountTimeAdd;
    public float duration = 7f;            // the duration of powerup on map

    private PowerupController controller;

    // Use this for initialization
    void Start () {
        controller = FindObjectOfType<PowerupController>();

    }
	
	// Update is called once per frame
	void Update () {
        duration -= Time.deltaTime;
        if (duration <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        // controller.AssignPlayer(coll.gameObject);
        // controller.ActivatePowerup(ptype, powerupLength, speedMultiplier, amountTimeAdd);
        if (coll.CompareTag("Player1") || coll.CompareTag("Player2") || coll.CompareTag("Player3") || coll.CompareTag("Player4"))
        {
            controller.AssignPlayer(coll.gameObject);
            controller.ActivatePowerup(ptype, powerupLength, speedMultiplier, amountTimeAdd);
        }

        gameObject.SetActive(false);
    }
}
