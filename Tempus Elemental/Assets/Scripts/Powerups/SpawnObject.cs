using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour {

    public GameObject[] pups;       // powerup array
    public Vector3 spawnValues;
    public float spawnWait;         // some amount to wait
    public float spawnMostWait;     // the upper bound for time wait
    public float spawnLeastWait;    // the lower bound for time wait
    public int startWait;           // initial

    int randpup;                    // random number to decide which powerup 

	// Use this for initialization
	void Start () {
        StartCoroutine(Spawner()); 
	}
	
	// Update is called once per frame 
	void Update () {
        spawnWait = Random.Range(spawnLeastWait, spawnMostWait); 
	}
    
    IEnumerator Spawner ()
    {
        yield return new WaitForSeconds(startWait); // wait time
        while (true)
        {
            randpup = 0; // grab the time powerup for now but later change to picking a random powerup
            Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), Random.Range(-spawnValues.y, spawnValues.y) , 1);   // grab the spawn position with random vals
            Instantiate(pups[randpup], spawnPosition + transform.TransformPoint(0,0,0), gameObject.transform.rotation);                         // spawn the object

            yield return new WaitForSeconds(spawnWait);
        }
    }
}
