using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRecord : MonoBehaviour 
{
    public static float maxSecondsToRecord = 3;
    private List<PlayerRewindInfo> records;
    private bool shouldRecord;

    private PlayerMovement playerMovement;
    private PlayerTime playerTime;
    private Rigidbody2D rb;

	void Start () 
    {
        shouldRecord = true;
        records = new List<PlayerRewindInfo>();
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
        records.Insert(0, new PlayerRewindInfo(gameObject.transform.position,
                                               playerMovement.FacingDirection(),
                                               playerTime.TimeRemaining));
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

        PlayerRewindInfo info = records[0];

        // Rewind Logic
        rb.isKinematic = true;

        gameObject.transform.position = info.position;
        // need to deal with facing direction
        playerTime.TimeRemaining = info.remainingTime;

        rb.isKinematic = false;

        records.RemoveAt(0);
        return true;
    }
}
