using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KOTHController : GameController 
{
	//variables
    public float stealTreshold = 1.0f;			//how often time is stolen from players not in the zone
	public int timeStolen = 1;					//how much time is taken from players not in the zone
	public Vector3[] zoneLocations;				//possible locations that a zone can be spawned in
	public float timeBtwnZoneSwitch = 5.0f;		//how much time until the zone goes to a new location
	private float timeSinceLastZoneSwitch = 0.0f;
	private GameObject currZone;				//the zone currently in play

	//references
	public GameObject zone;
    private List<GameObject> playersInTheZone = new List<GameObject>();

	protected override bool VictoryCondition () 
    {
		if (players.Count <= 1)
		{
			return true;
		}

		// this means no end-game state has been breached and the game continues
		return false; 
	}

	//called when the map is loaded
	public override void OnStart () {
		base.OnStart ();
		currZone = Instantiate (zone);
		currZone.transform.position = zoneLocations [Random.Range (0, zoneLocations.Length)];
	}

    protected override void GameLogic() 
    {
		//switch zone location
		timeSinceLastZoneSwitch += Time.deltaTime;
		if (timeSinceLastZoneSwitch >= timeBtwnZoneSwitch) {
			timeSinceLastZoneSwitch = 0.0f;
			currZone.transform.position = zoneLocations [Random.Range (0, zoneLocations.Length)];
		}
    }

	//called every "stealThreshold" amount of time to take time from players not in the zone and give it to players in the zone
	void CommenceSteal () {
		if (playersInTheZone.Count < players.Count) {
			
		}
	}

    public void PlayerEnteredZone(GameObject player) 
    {
        playersInTheZone.Remove(player);
    }

    public void PlayerLeftZone(GameObject player) 
    {
        playersInTheZone.Add(player);
    }

	protected override string VictoryText () 
    {
		return "I like grapes";
	}
}
