using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public float forcePush;

    private void OnCollisionEnter2D(Collision2D coll2d) // this part is reusable to detect collision
    {
        if (coll2d.gameObject.tag == "Walls") // push back after reaching wall
        {
            Rigidbody2D rb2d = coll2d.gameObject.GetComponent<Rigidbody2D>();
            float x = rb2d.position.x;
            float y = rb2d.position.y;

            rb2d.angularVelocity = - x * 100;
            rb2d.velocity = - transform.up * y * 100;
        }
    }
}
