using UnityEngine;

public static class Utils 
{

    public static Vector2 GetPlayerMovement(string playerTag) 
    {
		float h = Input.GetAxis("Horizontal" + playerTag);
		float v = Input.GetAxis("Vertical" + playerTag);
        return new Vector2(h, v).normalized;
    }

    public static bool IsPlayerMoving(string playerTag) 
    {
		float h = Input.GetAxis("Horizontal" + playerTag);
		float v = Input.GetAxis("Vertical" + playerTag);
		if (Mathf.Abs(h) > Mathf.Epsilon || Mathf.Abs(v) > Mathf.Epsilon)
		{
            return true;
		}
        return false;
    }
}
