using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeReverse : MonoBehaviour 
{
    private List<GameObject> caughtPlayers = new List<GameObject>();

    public float transparency;                                          //how transparent is the clock
    private GameObject callingPlayer;                                   //keep track of the player who called it
    public float radius;                                                //how big is this time freeze?

    // make sure the proper animation is playing
    void Start()
    {
        GetComponent<Animator>().SetInteger("Type", 2);
    }

    // The Player calls this function whenever he instantiates a time "slow down" distortion. This will make sure that only the calling player isn't by the slow down, and follows them around the map.
    public void AssignPlayer(GameObject player, float radius)
    {
        callingPlayer = player;
        callingPlayer.GetComponent<PlayerMovement>().frozen = true;
        transform.localScale = new Vector3(radius, radius, 1);
        SpriteRenderer[] srs = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sr in srs)
        {
            sr.color = new Color(player.GetComponent<PlayerColor>().color.r, player.GetComponent<PlayerColor>().color.g, player.GetComponent<PlayerColor>().color.b, transparency);
        }
    }

    // In this instance, used to make sure the time distortion stays with the calling player.
    void FixedUpdate()
    {        
        transform.position = callingPlayer.transform.position;

        foreach (GameObject g in caughtPlayers)
        {
            g.GetComponent<PlayerRecord>().Rewind();
        }
    }

    //// Upon entering the distortion, stop other objects
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (Utils.DetermineObjectType(coll) == ObjectType.Player && 
            coll.gameObject.tag != gameObject.tag)
        {
            caughtPlayers.Add(coll.gameObject);
            coll.gameObject.GetComponent<PlayerRecord>().Pause();
            coll.gameObject.GetComponent<PlayerMovement>().frozen = true;
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (Utils.DetermineObjectType(coll) == ObjectType.Player)
        {
            caughtPlayers.Remove(coll.gameObject);
            coll.gameObject.GetComponent<PlayerRecord>().Resume();
            coll.gameObject.GetComponent<PlayerMovement>().frozen = false;
        }
    }


    void OnDestroy()
    {
        foreach (GameObject g in caughtPlayers)
        {
            g.GetComponent<PlayerRecord>().Resume();
            g.GetComponent<PlayerMovement>().frozen = false;
            callingPlayer.GetComponent<PlayerMovement>().frozen = false;
        }
    }
}
