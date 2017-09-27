using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayer : MonoBehaviour {

    public float speed;
    public float rotation;

    private Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        //x
        float moveHorizontal = Input.GetAxis("Horizontal");
        //y
        float moveVertical = Input.GetAxis("Vertical");

        //Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb2d.angularVelocity = moveHorizontal * rotation;
        rb2d.velocity = transform.up * moveVertical * speed;
    }
}
