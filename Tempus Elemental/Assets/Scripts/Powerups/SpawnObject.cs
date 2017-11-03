using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnObject : MonoBehaviour {

    public enum ObjectType
    {
        PowerUp1,
        PowerUp2,
        PowerUp3,
        PowerUp4,
        PowerUp5,
        Player,
    }

    public GameObject[] powerups;       // powerup array
    public Vector3 spawnValues;
    public float spawnWait;         	// some amount to wait
    public float spawnMostWait;     	// the upper bound for time wait
    public float spawnLeastWait;    	// the lower bound for time wait
    public int startWait;           	// initial
    private int randomPowerup;           // random number to decide which powerup 
	private int testMode = 0;

    private GameObject map1, map2;

	public void Construct(Vector3 _spawnValues, float _spawnWait, float _spawnMostWait, float _spawnLeastWait, int _startWait, int _randomPowerup, int _testMode)
    {
		testMode = _testMode;
        for (int i = 0; i < 3; i++)
        {
            spawnValues[i] = _spawnValues[i];
        }
        spawnWait = _spawnWait;
        spawnMostWait = _spawnMostWait;
        spawnLeastWait = _spawnLeastWait;
        startWait = _startWait;
        randomPowerup = _randomPowerup;
    }

	// Use this for initialization
	void Start () {
        //performing actions to spawn power-ups
        StartCoroutine(Spawner());

        //performing actions to switch the maps
        map1 = GameObject.Find("DummyMap");
        map2 = GameObject.Find("DummyMap2");
        Utils.SwitchMap(map1, map2);

        //performing actions to turn victory screen initially off
        GameObject.Find("Victory Background").GetComponent<Renderer>().enabled = false;
        GameObject.Find("Victory Message Txt").GetComponent<Text>().enabled = false;
    }
	
	// Update is called once per frame 
	void Update () {
        spawnWait = Random.Range(spawnLeastWait, spawnMostWait); 
	}
    
    public IEnumerator Spawner ()
    {
        yield return new WaitForSeconds(startWait); // wait time
		if (testMode == 0) {
			while (true) {
				randomPowerup = Random.Range (0, powerups.Length);
				// randomPowerup = 0; // grab the time powerup for now but later change to picking a random powerup
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), Random.Range (-spawnValues.y, spawnValues.y), 1);   // grab the spawn position with random vals
				Instantiate (powerups [randomPowerup], spawnPosition + transform.TransformPoint (0, 0, 0), gameObject.transform.rotation);                         // spawn the object

				yield return new WaitForSeconds (spawnWait);
			}
		} else if (testMode == 1) {
			while (true) {
				//randomPowerup = Random.Range (0, powerups.Length);
				// randomPowerup = 0; // grab the time powerup for now but later change to picking a random powerup
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), Random.Range (-spawnValues.y, spawnValues.y), 1);   // grab the spawn position with random vals
				Instantiate (Resources.Load("Tests/powerup1"), spawnValues + transform.TransformPoint (0, 0, 0), gameObject.transform.rotation);                         // spawn the object

				yield return new WaitForSeconds (spawnWait);
			}
		}
    }

    public GameObject Spawn(ObjectType type, Vector2 pos)
    {
        var prefab = powerups[(int)type];
        var objSize = prefab.GetComponent<Renderer>().bounds.size;
        var mapSize = GameObject.FindWithTag("Map").GetComponent<Renderer>().bounds.size;
        var colliders = GameObject.FindWithTag("Walls").GetComponents<BoxCollider2D>();
        bool shouldRepeat = false;
        // find a safe place
        while (true) {
			var randx = Random.Range(0, mapSize.x);
			var randy = Random.Range(0, mapSize.y);
			randx -= mapSize.x / 2;
			randy -= mapSize.y / 2;

            var randBounds = new Bounds(new Vector3(randx, randy), objSize);

			foreach (var coll in colliders)
			{
				if (coll.bounds.Intersects(randBounds))
				{
                    shouldRepeat = true;
                    break;
				}
			}

            if (shouldRepeat) {
                shouldRepeat = false;
                continue;
            }

            return Instantiate(prefab, new Vector3(randx, randy), gameObject.transform.rotation);
		}
    }
}
