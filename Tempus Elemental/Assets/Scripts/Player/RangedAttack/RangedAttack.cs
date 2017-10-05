using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : MonoBehaviour {
	public float ProjectileForce;                           // the speed of the projectile
    public GameObject fireball;
	public string playerNum;
	public float delay = 0.5f;								// ranged attack delay
	public float timepassed;
	private bool waitToCharging = false;

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
    }

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Fire" + gameObject.tag)) {
			if (!waitToCharging) {
				// wait for delay
				timepassed = 0f;
				StartCoroutine ("DelayTime");
			} 
        } else if (timepassed >= delay && Input.GetButtonUp ("Fire" + gameObject.tag)) {

			fire (timepassed);
			waitToCharging = false;
			timepassed = 0f;
		}

	}
	void fire(float time) {
        gameObject.GetComponent<PlayerTime>().timeRemaining -= (int) time + 1;
        GameObject newFireball = Instantiate(fireball, transform.position, transform.rotation);
		newFireball.GetComponent<Rigidbody2D> ().AddRelativeForce (new Vector2 (0f, -ProjectileForce));
	
	}
}
