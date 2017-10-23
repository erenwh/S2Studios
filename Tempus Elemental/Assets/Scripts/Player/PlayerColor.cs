﻿using UnityEngine;

public class PlayerColor : MonoBehaviour 
{
	public Color color;
    	
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
}