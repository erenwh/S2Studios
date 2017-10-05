﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : MonoBehaviour {
	public float ProjectileForce;                           // the speed of the projectile
    public GameObject fireball;
	public string playerNum;
	public float delay = 1f;										// ranged attack delay
	public float timepassed;
	private bool waitToCharging = false;
	private Vector3 aimDirc;
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
		if (Input.GetButtonDown ("Fire" + gameObject.tag)) {
			if (!waitToCharging) {
				// wait for delay
				timepassed = 0f;
				StartCoroutine ("DelayTime");
			} else {
				// change direction while aming
				pm.charging = true;
				aimDirc = new Vector3(Input.GetAxis("Horizontal" + gameObject.tag), Input.GetAxis("Vertical" + gameObject.tag), 0).normalized;
			}

		} else if (timepassed >= delay && Input.GetButtonUp ("Fire" + gameObject.tag)) {
			fire ();
			waitToCharging = false;
			timepassed = 0f;
			pm.charging = false;
		}

	}
	void fire() {
		GameObject newFireball = Instantiate(fireball, transform.position, Quaternion.Euler(aimDirc));
		newFireball.GetComponent<Rigidbody2D> ().velocity =  aimDirc * ProjectileForce;
	}
}
