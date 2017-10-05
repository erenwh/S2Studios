using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : MonoBehaviour {
    public Vector2 speed;                           // the speed of the projectile
    private int projectileHealth = 10;              // the distance of the projectile can travel
    public GameObject fireball;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("FirePlayer1")) {
            GameObject newFireball = Instantiate(fireball, transform.position, transform.rotation);
            newFireball.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0f, -100f));
        }
	}

}
