using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWall : MonoBehaviour {

    public float speed = 1f;
    private Rigidbody2D rb2D;

	// Use this for initialization
	void Start () {
        rb2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //rb2D.velocity = Vector2.zero;
        rb2D.MoveRotation(rb2D.rotation + speed * Time.fixedDeltaTime);
        //rb2D.velocity = Vector2.zero;
    }
}
