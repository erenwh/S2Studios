using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour {
	public int damage;

	void OnTriggerEnter2D (Collider2D other) {
		if (!other.CompareTag ("Player1")) {
			other.SendMessage ("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
			Destroy (gameObject);
		}
	}

}
