using UnityEngine;

public class Powerup : MonoBehaviour
{
    public PowerUpType powerUpType;
    public float powerupLength;
    public float speedMultiplier;
    public int amountTimeAdd;
    public int addedDamage;
    public float duration = 7f;            // the duration of powerup on map


    // Update is called once per frame
    void Update()
    {
        duration -= Time.deltaTime;
        if (duration <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (Utils.DetermineObjectType(coll) == ObjectType.Player)
        {
            activatePowerUpForPlayer(coll.gameObject);
			SFXHandler.CollectSFX ();
        }

        Destroy(gameObject);
    }

    private void activatePowerUpForPlayer(GameObject player)
    {
        DistortionCreator dc = player.GetComponent<DistortionCreator>();
        switch (powerUpType)
        {
            case PowerUpType.SlowDown:
                dc.DistortionType = DistortionType.SlowDown;
                break;
            case PowerUpType.SpeedUp:
                dc.DistortionType = DistortionType.SpeedUp;
                break;
            case PowerUpType.Freeze:
                dc.DistortionType = DistortionType.Freeze;
                break;
            case PowerUpType.Reverse:
                dc.DistortionType = DistortionType.Reverse;
                break;
            case PowerUpType.AddTime:
                player.GetComponent<PlayerTime>().AddTime(amountTimeAdd);
                break;
            case PowerUpType.AddDamage:
                player.GetComponent<PlayerMeleeAttack>().
                addDamageForTime(addedDamage, powerupLength);
                break;
        }
    }
}
