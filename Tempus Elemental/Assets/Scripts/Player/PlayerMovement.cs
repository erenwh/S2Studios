using UnityEngine;
using System.Collections;

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
	private DistortionCreator dc;

	public float dashSpeed = 6f;			//how much faster do you move when dashing?
	public float dashTime = 0.25f;			//how long do you dash?
	public int dashCost = 1;				//how much time do you lose for dashing?

	private bool dashing;
	private bool dashDown = false;
	private float dashDownTime = 0.0f;

    public bool frozen = false;

	void Start ()
	{
        frozen = false;
        charging = false;
		rb2d = GetComponent<Rigidbody2D> ();
		cc2d = GetComponent<CircleCollider2D> ();
		dc = GetComponent<DistortionCreator> ();
	}

	public Vector2 FacingDirection ()
	{
		return lastDirection;
	}

	void Update () {
		if (!charging && !dashing) {
			Dash ();
		}
        if (frozen && dashing) {
            //rb2d.velocity = Vector2.zero;
        }
	}

	private void FixedUpdate ()
	{
		if (charging) {
			rb2d.velocity = Vector2.zero;
		} else {
			if (!dashing) {
				Vector2 movement = Utils.GetPlayerMovement (tag);
				if (Utils.IsPlayerMoving (tag)) {
					lastDirection = movement;
				}
                if (frozen)
                {
                    rb2d.velocity = Vector2.zero;
                }
                else
                {
                    rb2d.velocity = movement * speed;
                }
				cc2d.offset = lastDirection * circleOffsetCoefficient;
			}
		} 
	}

	// Performs the dash. Continue the player's momentum for a specified amount of time. Costs a second to perform.
	IEnumerator PerformDash () {
		dashing = true;
		SFXHandler.DashSFX ();
		Vector2 movement = Utils.GetPlayerMovement (tag);
		rb2d.velocity = movement * (speed + dashSpeed);
		dashDownTime = 0.0f;	//shouldn't need, just assuring that it is 0
		GetComponent<PlayerTime> ().DecrementTime(dashCost);
		yield return new WaitForSeconds (dashTime);
		dashing = false;
	}

	// Calculate if we should use a dash or a time distortion right now.
	void Dash ()
	{
		if (Input.GetButtonDown ("Distort" + gameObject.tag)) {
			dashDown = true;
			dashDownTime = 0.0f;
		}
		if (Input.GetButtonUp ("Distort" + gameObject.tag)) {
			dashDown = false;
			if (dashDownTime < 0.5f && !frozen) {
				StartCoroutine ("PerformDash");
			} else {
				dc.EndDistortion();
			}
			dashDownTime = 0.0f;
		}
		if (dashDown) {
			dashDownTime += Time.deltaTime;
		}
		if (dashDownTime >= 0.5f) {
			dc.Distort();
		}
	}
}
