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
        if (Utils.DetermineObjectType(other) != ObjectType.Player) 
        {
            if (Utils.DetermineObjectType(other) == ObjectType.Wall) {
				Destroy(gameObject);
			}
			return;
		} 
        if (!other.CompareTag(currP))
        {
            //other.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
            //other.GetComponent<PlayerTime>().timeRemaining -= damage;
            if (Game.Instance.gameModeSelected == 1 || Game.Instance.gameModeSelected == 4) //don't harm teammates in TDM or CTF
            {
                if (other.gameObject.CompareTag("Player1") || other.gameObject.CompareTag("Player3"))
                {
                    if (currP.Equals("Player1") || currP.Equals("Player3"))
                    {
                        return;
                    }
                }
                else if (other.gameObject.CompareTag("Player2") || other.gameObject.CompareTag("Player4"))
                {
                    if (currP.Equals("Player2") || currP.Equals("Player4"))
                    {
                        return;
                    }
                }
            }
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
