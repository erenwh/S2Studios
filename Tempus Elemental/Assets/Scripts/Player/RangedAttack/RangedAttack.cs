using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : MonoBehaviour {
	//public float ProjectileForce = 150f;                           // the speed of the projectile
    public GameObject fireball;
	public string playerNum;
	public float delay = 0.5f;								// ranged attack delay
	public float timepassed;
	private bool waitToCharging = false;
	public Vector2 aimDirc;
	private PlayerMovement pm;

	IEnumerator DelayTime () {
		waitToCharging = true;
		while (timepassed <= delay) {
			yield return new WaitForSeconds (0.1f);
			timepassed += 0.1f;
		}
	}
    // Use this for initialization
    void Start () {
		timepassed = 0f;
		pm = GetComponent<PlayerMovement> ();
    }

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonUp ("Fire" + gameObject.tag)) 
		{
			pm.charging = false;	
		}

		if (Input.GetButton ("Fire" + gameObject.tag)) {
			if (!waitToCharging) {
				// wait for delay
				timepassed = 0f;
				StartCoroutine ("DelayTime");
				pm.charging = true;
				//aimDirc = new Vector2(Input.GetAxis("Horizontal" + gameObject.tag), Input.GetAxis("Vertical" + gameObject.tag)).normalized;
			} 
            else {

                // change direction while aming, player cannot move
				aimDirc = new Vector2(Input.GetAxis("Horizontal" + gameObject.tag), Input.GetAxis("Vertical" + gameObject.tag)).normalized;
				if (Mathf.Abs (aimDirc.x) < Mathf.Epsilon && Mathf.Abs (aimDirc.y) < Mathf.Epsilon) {
					aimDirc = pm.FacingDirection ();
				} else {
					pm.lastDirection = aimDirc;
				}

			}
		} else if (timepassed >= delay && Input.GetButtonUp ("Fire" + gameObject.tag)) {
            fire(timepassed);
            waitToCharging = false;
			timepassed = 0f;
			pm.charging = false;
		}

	}

	void fire(float time) {
        gameObject.GetComponent<PlayerTime>().timeRemaining -= (int) time + 1;

		GameObject newFireball = Instantiate(fireball, transform.position, Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.down, aimDirc)));
		//Debug.Log (newFireball.GetComponent<ProjectileScript> ());
//		Debug.Log(aimDirc);
		newFireball.GetComponent<ProjectileScript> ().setAim(aimDirc);
        newFireball.GetComponent<ProjectileScript>().setPlayer(gameObject.tag);
        //		Debug.Log (newFireball.GetComponent<ProjectileScript> ().direction);
        //        newFireball.GetComponent<Rigidbody2D>().velocity = aimDirc * 50;
        //GameObject newFireball = Instantiate(fireball, transform.position, transform.rotation);
        //newFireball.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0f, -ProjectileForce));
    }
}
