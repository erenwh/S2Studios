using UnityEngine;

public class PlayerMeleeAttack : MonoBehaviour 
{

    public int damage = 1; 						//damage default to remove 1 seconds of time from player
    public float timeBtwnAttacks = 0.33f;		//even with button spam, don't allow buttonSpammers to attack too quickly
    private Collider2D playerAttacked;
    private bool hasAttacked = false;
    private float delay;

	void Start () 
    {
        hasAttacked = false;
	}

    void Update() 
    {
        if (playerAttacked == null)
        {
            return;
        }

		delay += Time.deltaTime;

        if (Input.GetButton("Fire" + gameObject.tag) && !hasAttacked)
        {
            hasAttacked = true;
            Attack();
        }

		if (Input.GetButtonUp("Fire" + gameObject.tag) && delay >= timeBtwnAttacks)
        {
            hasAttacked = false;
        }
    }

    void Attack()
    {
		delay = 0.0f;
        if (Utils.DetermineObjectType(playerAttacked) == Utils.ObjectType.Player)
        {
			PlayerTime.TransferTime (damage, playerAttacked.gameObject, gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        // controller.AssignPlayer(coll.gameObject);
        // controller.ActivatePowerup(ptype, powerupLength, speedMultiplier, amountTimeAdd);
        playerAttacked = coll;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerAttacked = null;
    }
}
