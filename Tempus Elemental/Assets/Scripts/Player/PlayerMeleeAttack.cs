using UnityEngine;

public class PlayerMeleeAttack : MonoBehaviour 
{

    public int damage = 5; 						//damage default to remove 5 seconds of time from player
    public float timeBtwnAttacks = 0.33f;		//even with button spam, don't allow buttonSpammers to attack too quickly
    private Collider2D playerAttacked;
    private bool hasAttacked = false;
    private bool isDamageChanged = false;
    private float damageChangeTime = 0f;
    private int oldDamage; 
    private float delay;

    private void Start()
    {
        oldDamage = damage;
    }

    void Update()
    {
        if (isDamageChanged) 
        {
            damageChangeTime -= Time.deltaTime;
            if (damageChangeTime <= 0) 
            {
                damage = oldDamage;
                isDamageChanged = false;
            }
        }

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
			// delay gets set to 0 here to indicate that an attack has ended and can start again.
			delay = 0.0f;
            hasAttacked = false;
        }
    }

    void Attack()
    {
        if (Utils.DetermineObjectType(playerAttacked) == ObjectType.Player)
        {
			PlayerTime.TransferTime (damage, playerAttacked.gameObject, gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {        
        playerAttacked = coll;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerAttacked = null;
    }

    public void addDamageForTime(int addedDamage, float time) 
    {
        damage += addedDamage;
        isDamageChanged = true;
        damageChangeTime = time;
    }
}
