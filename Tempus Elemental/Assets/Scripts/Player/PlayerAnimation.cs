using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

	Animator animator;

	// Use this for initialization
	void Start () 
    {
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
    {
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");
		bool fire = Input.GetButtonDown("Fire1");

        bool isMoving = h * v < Mathf.Epsilon;
        int oldDirection = animator.GetInteger("Direction");
        int direction;
        if (Mathf.Abs(h) < Mathf.Epsilon) 
        {
            direction = oldDirection;
        } else 
        {
            direction = Mathf.Sign(h) < 0 ? 3 : 1;
        }

		if (Mathf.Abs(v) > Mathf.Epsilon)
		{
			direction = Mathf.Sign(v) < 0 ? 2 : 0;
		}

        animator.SetBool("Moving", isMoving);

        animator.SetInteger("Direction", direction);
		animator.SetBool("Fire", fire);	
	}
}

