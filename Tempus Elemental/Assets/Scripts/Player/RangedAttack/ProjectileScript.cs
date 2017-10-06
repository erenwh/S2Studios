using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour {
	public int damage;
	public Vector2 aim;
	public float speed = 5f;

	private Rigidbody2D rb2d;

	void Start()
	{
		rb2d = GetComponent<Rigidbody2D> ();
	}

	public void setAim(Vector2 a)
	{
		
		aim = a;
	}

	void OnTriggerEnter2D (Collider2D other) {
        if (other.gameObject != gameObject)
        {
            other.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
            other.GetComponent<PlayerTime>().timeRemaining -= damage;
            Destroy(gameObject);
        }
	}
	void FixedUpdate() 
	{
		rb2d.velocity = aim * speed;
//		Debug.Log (rb2d.velocity);
	}
}
