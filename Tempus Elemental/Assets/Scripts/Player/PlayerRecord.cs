using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRecord : MonoBehaviour 
{
    public float maxSecondsToRecord = 10;
    private List<Vector3> records = new List<Vector3>();
    private bool shouldRecord = true;

    private PlayerMovement playerMovement;
    private Rigidbody2D rb;

	void Start () 
    {        
        playerMovement = GetComponent<PlayerMovement>();
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
        records.Insert(0, transform.position);
    }

    public void Resume()
    {
        shouldRecord = true;
        rb.isKinematic = false;
        playerMovement.frozen = false;
    }

    public void Pause()
    {
        shouldRecord = false;
        rb.isKinematic = true;
        playerMovement.frozen = true;
    }

    public bool Rewind()
    {
        if (records.Count == 0) {
            return false;
        }

        //Debug.Log(records[0]);
        // Rewind Logic


        gameObject.transform.position = records[0];

        records.RemoveAt(0);
        return true;
    }
}
