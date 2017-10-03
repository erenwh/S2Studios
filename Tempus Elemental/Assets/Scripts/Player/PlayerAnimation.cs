﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

	Animator animator;

    bool change = false;
    States state = States.idle;

    enum States
    {
        idle = 0,
        movingUp, movingRight, movingDown, movingLeft,
        attackUp, attackRight, attackDown, attackLeft
    }

	// Use this for initialization
	void Start () 
    {
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        change = true;
        States nextState = CalculateNextState();

        if (nextState == state) {
            change = false;
        }

        state = nextState;

		animator.SetBool("idle", change);
		animator.SetInteger("state", (int)state);

	}

    States CalculateNextState() 
    {
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");


		// check for movement
		if (Mathf.Abs(h) < Mathf.Epsilon && Mathf.Abs(v) < Mathf.Epsilon)
		{
			return States.idle;
			
		}

		if (Mathf.Abs(h) > Mathf.Epsilon)
		{
			if (Mathf.Sign(h) > 0)
			{
				return States.movingRight;
			}
			else
			{
				return States.movingLeft;
			}
		}

		if (Mathf.Abs(v) > Mathf.Epsilon)
		{
			if (Mathf.Sign(v) > 0)
			{
				return States.movingUp;
			}
			else
			{
				return States.movingDown;
			}
		}

        return States.idle;

	}
}
