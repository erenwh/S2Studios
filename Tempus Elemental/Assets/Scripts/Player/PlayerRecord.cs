using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRecord : MonoBehaviour 
{
    public static float maxSecondsToRecord = 3;
    private List<Vector3> records = new List<Vector3>();
    private bool shouldRecord = true;

    private PlayerMovement playerMovement;
    private PlayerTime playerTime;
    private Rigidbody2D rb;

	void Start () 
    {        
        playerMovement = GetComponent<PlayerMovement>();
        playerTime = GetComponent<PlayerTime>();
        rb = GetComponent<Rigidbody2D>();
	}
	
	
	void FixedUpdate () 
    {
        if (shouldRecord)
        {
            Record();
        }
	}

    void Record()
    {
        if (records.Count > Mathf.Round(maxSecondsToRecord * 1f / Time.fixedDeltaTime))
        {
            records.RemoveAt(records.Count - 1);
        }
        records.Insert(0, gameObject.transform.position);
    }

    public void Resume()
    {
        shouldRecord = false;
    }

    public void Pause()
    {
        shouldRecord = true;
    }

    public bool Rewind()
    {
        if (records.Count == 0) {
            return false;
        }

        // Rewind Logic
        rb.isKinematic = true;

        gameObject.transform.position = records[0];

        rb.isKinematic = false;

        records.RemoveAt(0);
        return true;
    }
}
