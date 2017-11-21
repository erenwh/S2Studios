using UnityEngine;

public class Powerup : MonoBehaviour 
{
    public int ptype;
    public float powerupLength;
    public float speedMultiplier;
    public int amountTimeAdd;
    public float duration = 7f;            // the duration of powerup on map

    private PowerupController controller;

    void Start () 
    {
        controller = FindObjectOfType<PowerupController>();

    }
	
	// Update is called once per frame
	void Update () 
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
            controller.AssignPlayer(coll.gameObject);
            controller.ActivatePowerup(ptype, powerupLength, speedMultiplier, amountTimeAdd);
        }

        Destroy(gameObject);
    }
}
