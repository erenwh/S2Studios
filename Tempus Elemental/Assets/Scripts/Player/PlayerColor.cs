using UnityEngine;

public class PlayerColor : MonoBehaviour 
{
	public Color color;
	public float t;
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
		float time = (float)GetComponent<PlayerTime> ().TimeRemaining / 45.0f;
		t = time;
		color.a = time;
		GetComponent<SpriteRenderer> ().color = color;

	}

}
