using UnityEngine;

public class PlayerColor : MonoBehaviour 
{
	public Color color;
	[HideInInspector] public int playerNum;			//a zero based indication of which player this is

    private SpriteRenderer spriteRenderer;
	private bool updatelock = false;

	void Start () 
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (Game.Instance.gameModeSelected == 1) //do color assignment for team death match
        {
            if (CompareTag("Player1"))
            {
                color = new Color(144f / 256f, 208f / 256f, 1, 1);
				playerNum = 0;
            }
            else if (CompareTag("Player2"))
            {
                color = Color.red;
				playerNum = 1;
            }
            else if (CompareTag("Player3"))
            {
                color = new Color(144f / 256f, 208f / 256f, 1, 1);
				playerNum = 2;
            }
            else if (CompareTag("Player4"))
            {
                color = Color.red;
				playerNum = 3;
            }
        }
        else //do normal color assignment
        {
            if (CompareTag("Player1"))
            {
                color = Game.Instance.p1Color;
				playerNum = 0;
            }
            else if (CompareTag("Player2"))
            {
                color = Game.Instance.p2Color;
				playerNum = 1;
            }
            else if (CompareTag("Player3"))
            {
                color = Game.Instance.p3Color;
				playerNum = 2;
            }
            else if (CompareTag("Player4"))
            {
                color = Game.Instance.p4Color;
				playerNum = 3;
            }
        }

		spriteRenderer.color = color;

	}
	void Update() 
    {
		// player's color transparency based on player remaining health
		if (!updatelock) 
        {
			float time = (float)GetComponent<PlayerTime> ().TimeRemaining / 90.0f + 0.4f;
			color.a = time;
			spriteRenderer.color = color;
		}
	}
	public void pickUpPowerUpNotification() 
    {
		// flash when pickup a powerup
		updatelock = true;

		float time = Time.deltaTime;
		while (time < .5F) {
			color = Color.white; 
			spriteRenderer.color = color;
		}

		//finish
		time = 0;
		updatelock = false;
		spriteRenderer.color = color;
	}
}
