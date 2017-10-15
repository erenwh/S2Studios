using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

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

	// Use this for initialization
	void Start () 
    {
		animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
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

		animator.SetBool("change", change);
		animator.SetInteger("state", (int)state);

	}

    States CalculateNextState() 
    {
		Vector2 movement = Utils.GetPlayerMovement(tag);
        Vector2 facingDirection = playerMovement.FacingDirection();
        bool isAttacking = Input.GetButton("Fire" + gameObject.tag);


        if (isAttacking) 
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
				return States.movingRight;
			}
			else
			{
				return States.movingLeft;
			}
		}

		if (Mathf.Abs(movement.y) > Mathf.Epsilon)
		{
			if (Mathf.Sign(movement.y) > 0)
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

