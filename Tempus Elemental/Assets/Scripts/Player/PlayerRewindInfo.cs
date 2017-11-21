using UnityEngine;

public class PlayerRewindInfo 
{

    public Vector2 position;
    public Vector2 direction;
    public int remainingTime;

    public PlayerRewindInfo(Vector2 position, Vector2 direction, 
                            int remainingTime)
    {
        this.position = position;
        this.direction = direction;
        this.remainingTime = remainingTime;
    }
}
