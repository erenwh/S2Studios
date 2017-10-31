using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

	public float speed = 3f;
	//how quickly the player moves
	public bool charging = false;
	// this is for the circular collider for attack
	public float circleOffsetCoefficient = .18f;

	private CircleCollider2D cc2d;
	private Rigidbody2D rb2d;
	private Vector2 lastDirection;

	public float dashSpeed = 6f;
	public float dashTime;
	public bool dashing;

	public float dashCD = 3f;
	public bool dashOnCD;

	/*
	public int sameDirectionKeyCount = 0;
	public float lastDirectionKey;
	public int horOrVer = -1;
	public bool dash = false;
	public float dashTime = 0;
	public float dashVel;
	public float delay;*/

	public int ButtonCount = 0;

	void Start ()
	{
		charging = false;
		rb2d = GetComponent<Rigidbody2D> ();
		cc2d = GetComponent<CircleCollider2D> ();

	}

	public Vector2 FacingDirection ()
	{
		return lastDirection;
	}

	private void FixedUpdate ()
	{
		
		Dash ();

		if (charging) {
			rb2d.velocity = Vector2.zero;
		} else {
			if (dashing) {
				

				Vector2 movement = Utils.GetPlayerMovement (tag);

				if (Utils.IsPlayerMoving (tag)) {
					lastDirection = movement;
				}

				rb2d.velocity = movement * (speed + dashSpeed);

				cc2d.offset = lastDirection * circleOffsetCoefficient;	
			} else if (!dashing) {
				Vector2 movement = Utils.GetPlayerMovement (tag);

				if (Utils.IsPlayerMoving (tag)) {
					lastDirection = movement;
				}

				rb2d.velocity = movement * speed;

				cc2d.offset = lastDirection * circleOffsetCoefficient;
			}
			

		} 
	}

	public void Dash ()
	{
		if (Input.GetButtonDown ("Distort" + gameObject.tag)) {
			dashing = true;
		}
		if (Input.GetButtonUp ("Distort" + gameObject.tag)) {
			dashing = false;
		}
		if (dashing) {
			dashTime += Time.deltaTime;
			dashOnCD = true;
		}
		if (dashTime >= .5) {
			dashing = false;
			dashTime = 0;
		}


		/*if (Input.GetButtonDown ("Horizontal" + gameObject.tag)) {
			if (lastDirectionKey == Input.GetAxis ("Horizontal" + gameObject.tag) && horOrVer == 0) {
				sameDirectionKeyCount++;
			} else {
				lastDirectionKey = Input.GetAxis ("Horizontal" + gameObject.tag);
				sameDirectionKeyCount = 0;
				horOrVer = 0;  //horizontal
			}
		}
		if (Input.GetButtonDown ("Vertical" + gameObject.tag)) {
			if (lastDirectionKey == Input.GetAxis ("Vertical" + gameObject.tag) && horOrVer == 1) {
				sameDirectionKeyCount++;
			} else {
				lastDirectionKey = Input.GetAxis ("Vertical" + gameObject.tag);
				sameDirectionKeyCount = 0;
				horOrVer = 1;
			}

		}

		if (Input.GetButtonUp ("Horizontal" + gameObject.tag)) {
			dashVel = 0;
			//rb2d.velocity = new Vector2 (0, 0);
		}
		if (Input.GetButtonUp ("Vertical" + gameObject.tag)) {
			dashVel = 0;
			//rb2d.velocity = new Vector2 (0, 0);
		}
		if ((sameDirectionKeyCount == 1) && (delay < .5)) {
			delay += Time.deltaTime;
		}
		if ((sameDirectionKeyCount == 1) && (delay >= .5)) { //reset
			delay = 0;
			sameDirectionKeyCount = 0;
		}
		if ((sameDirectionKeyCount == 2) && (delay < .5)) { // dash
			dash = true;
			dashVel = 6f;
			sameDirectionKeyCount = 0;
			horOrVer = -1;
			//Dash
		}
		if (delay >= .5) { // dash end
			dashVel = 0;
			sameDirectionKeyCount = 0;
			delay = 0;
			dash = false;
		}*/

	}
}
