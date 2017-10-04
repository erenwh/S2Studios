using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour {

    public float speed;					//how quickly the player moves
	public float rotationSpeed = 30.0f;	//how quickly the player faces the direction they are moving

    private Rigidbody2D rb2d;

    void Start()
    {
        GameObject p2 = GameObject.FindGameObjectWithTag("Player2");
        rb2d = p2.GetComponent<Rigidbody2D>();
    }
		
    private void FixedUpdate()
    {
        //x
        float moveHorizontal = Input.GetAxis("Horizontal2");
        //y
        float moveVertical = Input.GetAxis("Vertical2");

		Vector2 movement = new Vector2(moveHorizontal, moveVertical).normalized;

        rb2d.velocity = movement * speed;

		//(Parr's rotation suggestion) <- delete this comment if you like it :)
		//Angular Movement
//		if (Mathf.Abs (moveHorizontal) > 0.1 || Mathf.Abs (moveVertical) > 0.1) {
//			float angle = Mathf.Atan2 (moveVertical, moveHorizontal) * Mathf.Rad2Deg - 90;
//			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.AngleAxis (angle, Vector3.forward), Time.deltaTime * rotationSpeed);
//		} 
    }
}
