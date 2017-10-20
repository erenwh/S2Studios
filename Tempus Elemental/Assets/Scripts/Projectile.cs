using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour 
{
	public int damage = 8;
	public Vector2 aim;
	public float speed = 5f;
    public string currP;

	private Rigidbody2D rb2d;

	void Start()
	{
		rb2d = GetComponent<Rigidbody2D> ();
	}

	public void setAim(Vector2 a)
	{
		
		aim = a;
	}

    public void setPlayer(string tag)
    {
        currP = tag;
    }

    void OnTriggerEnter2D (Collider2D other) 
    {
        if (Utils.DetermineObjectType(other) != Utils.ObjectType.Player) 
        {
            if (Utils.DetermineObjectType(other) == Utils.ObjectType.Wall) {
				Destroy(gameObject);
			}
			return;
		} 
        if (!other.CompareTag(currP))
        {
            //other.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
           	//other.GetComponent<PlayerTime>().timeRemaining -= damage;
			PlayerTime.TransferTime(damage, other.gameObject, GameObject.FindGameObjectWithTag(currP));
            Destroy(gameObject);
        }

        /*if (other.gameObject != gameObject)
        {
            other.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
            other.GetComponent<PlayerTime>().timeRemaining -= damage;
            Destroy(gameObject);
        }*/
	}
	void FixedUpdate() 
	{
		rb2d.velocity = aim * speed;
//		Debug.Log (rb2d.velocity);
	}
}
