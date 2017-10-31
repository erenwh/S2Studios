using UnityEngine;

public class PlayerColor : MonoBehaviour 
{
	public Color color;

	public float t;
	private bool updatelock = false;

	void Start () 
    {
		//assign color
		if (CompareTag ("Player1")) 
        {
			color = Color.blue;
		} else if (CompareTag ("Player2")) 
        {
			color = Color.red;
		}else if (CompareTag ("Player3")) 
        {
			color = Color.green;
		}else if (CompareTag ("Player4")) 
        {
			color = Color.yellow;
		}

		GetComponent<SpriteRenderer> ().color = color;
	}
	void Update() {
		// player's color transparency based on player remaining health
		if (!updatelock) {
			float time = (float)GetComponent<PlayerTime> ().TimeRemaining / 45.0f;
			t = time;
			color.a = time;
			GetComponent<SpriteRenderer> ().color = color;
		} else {
		}

	}
	public void pickUpPowerUpNotification() {
		// flash when pickup a powerup

		updatelock = true;

		float time = Time.deltaTime;
		while (time < .5F) {
			color = Color.white; 
			GetComponent<SpriteRenderer> ().color = color;
		}

		//finish
		time = 0;
		updatelock = false;

		GetComponent<SpriteRenderer> ().color = color;
	}
}
