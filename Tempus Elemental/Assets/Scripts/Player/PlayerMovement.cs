using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed = 3f;					//how quickly the player moves
	public bool charging = false;
	// this is for the circular collider for attack
	public float circleOffsetCoefficient = .18f; 

	private CircleCollider2D cc2d;
	private Rigidbody2D rb2d;
    private Vector2 lastDirection;

	void Start()
	{
		charging = false;
		rb2d = GetComponent<Rigidbody2D>();
		cc2d = GetComponent<CircleCollider2D>();

	}

	public Vector2 FacingDirection() {
		return lastDirection;
	}

	private void FixedUpdate()
	{
        if (charging) 
        {
            rb2d.velocity = Vector2.zero;
        } 
        else 
        {
            Vector2 movement = Utils.GetPlayerMovement(tag);

            if (Utils.IsPlayerMoving(tag)) 
			{
				lastDirection = movement;
			}

			rb2d.velocity = movement * speed;
			cc2d.offset = lastDirection * circleOffsetCoefficient;			
		} 
	}
}
