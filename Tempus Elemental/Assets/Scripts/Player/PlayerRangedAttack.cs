using UnityEngine;

public class PlayerRangedAttack : MonoBehaviour 
{    
	//constants
	private const int DOWN = 0;
	private const int LEFT = 1;
	private const int UP = 2;
	private const int RIGHT = 3;

    public GameObject fireball;
	public float delay = 0.5f;								// ranged attack delay
	public int costToThrow = 1;                             // how much time does it take to throw an attack
    private float timepassed = 0f;
	private bool waitToCharging = false;
	private Vector2 aimDirc;
	private PlayerMovement pm;
	private Animator an;

	//public bool da = gameObject.GetComponent<PlayerMovement>().dashing; 


    void Start() 
    {
		pm = GetComponent<PlayerMovement>();
		an = GetComponent<Animator> ();
    }

	void Update() 
    {
		
		transform.rotation = Quaternion.identity;

		//always keep the most recent aim direction up to date
		if (Utils.IsPlayerMoving(tag)) 
		{
			aimDirc = Utils.GetPlayerMovement(tag);
		} 

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
				AimDir ();
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

	//allow the player to animate in the direction that they are aiming
	void AimDir () {
		//get the z rotation from the aimDirc
		float angle = Mathf.Atan2(aimDirc.y, aimDirc.x) * Mathf.Rad2Deg;
		if (angle >= -45f && angle <= 45f) {	//RIGHT
			an.SetInteger ("lastFacing", RIGHT);
			transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
		} else if (angle > -135f && angle < -45f) {	//DOWN
			an.SetInteger ("lastFacing", DOWN);
			transform.rotation = Quaternion.AngleAxis (angle + 90, Vector3.forward);
		} else if (angle > 45f && angle < 135f) {		//UP
			an.SetInteger ("lastFacing", UP);
			transform.rotation = Quaternion.AngleAxis (angle - 90, Vector3.forward);
		} else {	//LEFT
			an.SetInteger ("lastFacing", LEFT);
			transform.rotation = Quaternion.AngleAxis (angle + 180, Vector3.forward);
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
