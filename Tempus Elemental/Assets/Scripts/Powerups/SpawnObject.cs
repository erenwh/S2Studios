using UnityEngine;

public class SpawnObject : MonoBehaviour
{

    public GameObject[] powerups;       // powerup array
    public Vector3 spawnValues;
    public float spawnMostWait;     	// the upper bound for time wait
    public float spawnLeastWait;    	// the lower bound for time wait
    public float startWait;           	// initial
    public int testMode = 0;

    private float spawnWait;            // some amount to wait


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
    }

    void Start()
    {
        spawnWait = Random.Range(spawnLeastWait, spawnMostWait);
    }

    void Update()
    {
        // wait for the initial delay
        if (startWait > 0)
        {
            startWait -= Time.deltaTime;
            return;
        }

        if (spawnWait > 0)
        {
            spawnWait -= Time.deltaTime;
            return;
        }

        Spawner();
        spawnWait = Random.Range(spawnLeastWait, spawnMostWait);
    }

    private void Spawner()
    {
        if (testMode == 0)
        {
            int randomPowerup = Random.Range(0, powerups.Length);
            // randomPowerup = 0; // grab the time powerup for now but later change to picking a random powerup
            Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), Random.Range(-spawnValues.y, spawnValues.y), 1);   // grab the spawn position with random vals
            Instantiate(powerups[randomPowerup], spawnPosition + transform.TransformPoint(0, 0, 0), gameObject.transform.rotation);                         // spawn the object
        }
        else if (testMode == 1)
        {            
            Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), Random.Range(-spawnValues.y, spawnValues.y), 1);   // grab the spawn position with random vals
            Instantiate(Resources.Load("Tests/powerup1"), spawnPosition + transform.TransformPoint(0, 0, 0), gameObject.transform.rotation);                         // spawn the object

        }
        else if (testMode == 3)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), Random.Range(-spawnValues.y, spawnValues.y), 1);
            Instantiate(Resources.Load("Tests/powerup3"), spawnPosition + transform.TransformPoint(0, 0, 0), gameObject.transform.rotation);
        }
        else if (testMode == 4)
        {            
            Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), Random.Range(-spawnValues.y, spawnValues.y), 1);
            Instantiate(Resources.Load("Tests/powerup4"), spawnPosition + transform.TransformPoint(5, 5, 5), gameObject.transform.rotation);
        }
        else if (testMode == 5)
        {            
            Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), Random.Range(-spawnValues.y, spawnValues.y), 1);
            Instantiate(Resources.Load("Tests/powerup5"), spawnPosition + transform.TransformPoint(1, 1, 1), gameObject.transform.rotation);
        }
        else if (testMode == 6)
        {            
            Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), Random.Range(-spawnValues.y, spawnValues.y), 1);
            Instantiate(Resources.Load("Tests/powerup6"), spawnPosition + transform.TransformPoint(7, 7, 7), gameObject.transform.rotation);

        }
    }
}
