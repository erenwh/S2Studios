using UnityEngine;

public class PlayerAnimation : MonoBehaviour 
{
	//constants
	private const int DOWN = 0;
	private const int LEFT = 1;
	private const int UP = 2;
	private const int RIGHT = 3;

	private Animator animator;
    private bool change = false;
    private States state = States.idle;

    PlayerMovement playerMovement;

    enum States
    {
        idle = 0,
        movingUp, movingRight, movingDown, movingLeft,
        attackUp, attackRight, attackDown, attackLeft
    }

	void Start () 
    {
		animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
	}
	
	void Update () 
    {
        change = true;
        States nextState = CalculateNextState();

        if (nextState == state) {
            change = false;
        }

        state = nextState;

		animator.SetBool("change", change);
		animator.SetInteger("state", (int)state);

	}

    States CalculateNextState() 
    {
		Vector2 movement = Utils.GetPlayerMovement(tag);
        Vector2 facingDirection = playerMovement.FacingDirection();
        bool isAttacking = Input.GetButton("Fire" + gameObject.tag);
		bool justAttacked = Input.GetButtonDown("Fire" + gameObject.tag);

        if (justAttacked) 
        {
            if (Mathf.Abs(facingDirection.x) > Mathf.Epsilon)
			{
				if (Mathf.Sign(facingDirection.x) > 0)
				{
					animator.SetInteger ("lastFacing", RIGHT);
                    return States.attackRight;
				}
				else
				{
					animator.SetInteger ("lastFacing", LEFT);
                    return States.attackLeft;
				}
			}

			if (Mathf.Abs(facingDirection.y) > Mathf.Epsilon)
			{
				if (Mathf.Sign(facingDirection.y) > 0)
				{
					animator.SetInteger ("lastFacing", UP);
                    return States.attackUp;
				}
				else
				{
					animator.SetInteger ("lastFacing", DOWN);
                    return States.attackDown;
				}
			}   
        }

		if (isAttacking && !justAttacked) 
		{
			if (Mathf.Abs(facingDirection.x) > Mathf.Epsilon)
			{
				if (Mathf.Sign(facingDirection.x) > 0)
				{
					return States.attackRight;
				}
				else
				{
					return States.attackLeft;
				}
			}

			if (Mathf.Abs(facingDirection.y) > Mathf.Epsilon)
			{
				if (Mathf.Sign(facingDirection.y) > 0)
				{
					return States.attackUp;
				}
				else
				{
					return States.attackDown;
				}
			}   
		}

		// check for movement
		if (!Utils.IsPlayerMoving(tag))
		{
			return States.idle;
			
		}

		if (Mathf.Abs(movement.x) > Mathf.Epsilon)
		{
			if (Mathf.Sign(movement.x) > 0)
			{
				animator.SetInteger ("lastFacing", RIGHT);
				return States.movingRight;
			}
			else
			{
				animator.SetInteger ("lastFacing", LEFT);
				return States.movingLeft;
			}
		}

		if (Mathf.Abs(movement.y) > Mathf.Epsilon)
		{
			if (Mathf.Sign(movement.y) > 0)
			{
				animator.SetInteger ("lastFacing", UP);
				return States.movingUp;
			}
			else
			{
				animator.SetInteger ("lastFacing", DOWN);
				return States.movingDown;
			}
		}

        return States.idle;

	}
}

