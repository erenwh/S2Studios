using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : MonoBehaviour {
	public float ProjectileForce;                           // the speed of the projectile
    private int projectileHealth;              // the distance of the projectile can travel
    public GameObject fireball;
	public string playerNum;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("FirePlayer" + playerNum)) {
            GameObject newFireball = Instantiate(fireball, transform.position, transform.rotation);
			newFireball.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0f, -ProjectileForce));
        }
	}

}
