using UnityEngine;

public class PlayerRangedAttack : MonoBehaviour 
{    
    public GameObject fireball;
	public float delay = 0.5f;								// ranged attack delay
	public int costToThrow = 1;                             // how much time does it take to throw an attack
    private float timepassed = 0f;
	private bool waitToCharging = false;
	private Vector2 aimDirc;
	private PlayerMovement pm;

	//public bool da = gameObject.GetComponent<PlayerMovement>().dashing; 


    void Start() 
    {
		pm = GetComponent<PlayerMovement>();
    }

	void Update() 
    {
		if (Input.GetButton ("Fire" + gameObject.tag)) 
        {
			if (!waitToCharging) 
            {
				// wait for delay
				timepassed = 0f;
				waitToCharging = true;
				//StartCoroutine ("DelayTime");
				pm.charging = true;
			} 
            else 
            {
                // change direction while aiming, player cannot move
                aimDirc = Utils.GetPlayerMovement(tag);
                if (!Utils.IsPlayerMoving(tag)) 
                {
					aimDirc = pm.FacingDirection ();
				} 
			}
		} 
        else if (Input.GetButtonUp ("Fire" + gameObject.tag)) 
        {
			if (timepassed >= delay) 
            {
            	Fire();
			}
            waitToCharging = false;
			timepassed = 0f;
			pm.charging = false;
		}

		//DelayTime
		if (waitToCharging) 
        {
			timepassed += Time.deltaTime;
		}
	}

	void Fire() 
    {
		gameObject.GetComponent<PlayerTime>().DecrementTime(costToThrow);
		GameObject newFireball = Instantiate(fireball, transform.position, Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.down, aimDirc)));
		newFireball.GetComponent<Projectile> ().setAim(aimDirc);
        newFireball.GetComponent<Projectile>().setPlayer(gameObject.tag);
		newFireball.GetComponentInChildren<SpriteRenderer> ().color = GetComponent<PlayerColor> ().color;
    }
}
