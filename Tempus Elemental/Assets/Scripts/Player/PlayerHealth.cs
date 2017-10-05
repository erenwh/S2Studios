using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
    public int initTime = 60;                                   // The amount of Time the player starts the game with.
    public int currTime;                                        // The current health the player has.
    public Slider healthSlider;                                 // Reference to the UI's health/time bar.
    // public AudioClip deathClip;                              // The audio clip to play when the player dies.


    Animator anim;                                              // Reference to the Animator component.
    PlayerMovement playerMovement;                              // Reference to the player's movement.
    bool isDead;                                                // Whether the player is dead.
    bool damaged;                                               // True when the player gets damaged.
    int tickDown;                                               // Used to help with every 60 frames

    void Start () {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();

        // we do not set the bool values because they are false by default, as per Microsoft Docs

        // Set the initial time of the player
        currTime = initTime;
    }
	
	// Update is called once per frame
	void Update () {
        // currTime -= 1; for ticking down. needs revising for once per second rather than frame
        /* Temp Fix Requires way not tied to framerate */
        tickDown++;
        if ((tickDown % 60) == 0) { currTime--; tickDown = 0; }
        // I am wondering if this can be done even if a frame drops.
        if(currTime == 0)
        {
            isDead = true;
        }
    }

    public void TakeDamage(int amount)
    {
        damaged = true;

        // Reduce the current time by the damage amount
        currTime -= amount;

        // Set the health bar's value to the current time
        healthSlider.value = currTime;

        // dead
        if (currTime <= 0 && !isDead)
        {
            Death();
        }
    }

    void Death()
    {
        // Set the death flag so this function won't be called again.
        isDead = true;

        // Tell the animator that the player is dead.

        // Turn off the movement and shooting scripts.
        playerMovement.enabled = false;

        Object.Destroy(gameObject);
    }

}
