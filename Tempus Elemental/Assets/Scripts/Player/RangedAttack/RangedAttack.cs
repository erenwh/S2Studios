using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : MonoBehaviour {
	//public float ProjectileForce = 150f;                  // the speed of the projectile
    public GameObject fireball;
	//public string playerNum;
	public float delay = 0.5f;								// ranged attack delay
	public int costToThrow = 1;								// how much time does it take to throw an attack
	public float timepassed;
	private bool waitToCharging = false;
	public Vector2 aimDirc;
	private PlayerMovement pm;

//	IEnumerator DelayTime () {
//		while (timepassed <= delay) {
//			yield return new WaitForSeconds (0.1f);
//			timepassed += 0.1f;
//		}
//	}

    // Use this for initialization
    void Start () {
		timepassed = 0f;
		pm = GetComponent<PlayerMovement> ();
    }

	// Update is called once per frame
	void Update () {
		if (Input.GetButton ("Fire" + gameObject.tag)) {
			if (!waitToCharging) {
				// wait for delay
				timepassed = 0f;
				waitToCharging = true;
				//StartCoroutine ("DelayTime");
				pm.charging = true;
			} 
            else {
                // change direction while aiming, player cannot move
				aimDirc = new Vector2(Input.GetAxis("Horizontal" + gameObject.tag), Input.GetAxis("Vertical" + gameObject.tag)).normalized;
				if (Mathf.Abs (aimDirc.x) < Mathf.Epsilon && Mathf.Abs (aimDirc.y) < Mathf.Epsilon) {
					aimDirc = pm.FacingDirection ();
				} else {
					pm.lastDirection = aimDirc;
				}
			}
		} else if (Input.GetButtonUp ("Fire" + gameObject.tag)) {
			if (timepassed >= delay) {
            	fire();
			}
            waitToCharging = false;
			timepassed = 0f;
			pm.charging = false;
		}

		//DelayTime
		if (waitToCharging) {
			timepassed += Time.deltaTime;
		}
	}

	void fire() {
		gameObject.GetComponent<PlayerTime>().DecrementTime(costToThrow);
		GameObject newFireball = Instantiate(fireball, transform.position, Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.down, aimDirc)));
		newFireball.GetComponent<ProjectileScript> ().setAim(aimDirc);
        newFireball.GetComponent<ProjectileScript>().setPlayer(gameObject.tag);
    }
}
