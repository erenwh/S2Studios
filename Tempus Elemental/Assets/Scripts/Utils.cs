using UnityEngine;

public static class Utils 
{

    public enum ObjectType { Other, Player, Projectile, PowerUp, Distortion, Wall};

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

    public static ObjectType DetermineObjectType(Collider2D col) {
        return DetermineObjectType(col.gameObject);
    }

	public static ObjectType DetermineObjectType(GameObject obj) 
    {
        if (obj.tag.StartsWith("Player", System.StringComparison.CurrentCulture)) 
        {
            return ObjectType.Player;
        }
        if (obj.tag.StartsWith("Wall", System.StringComparison.CurrentCulture)) 
        {
            return ObjectType.Wall;
        }
        if (obj.tag.StartsWith("Projectile", System.StringComparison.CurrentCulture)) 
        {
            return ObjectType.Projectile;
        }
		if (obj.tag.StartsWith("Powerup", System.StringComparison.CurrentCulture))
		{
            return ObjectType.PowerUp;
		}
        return ObjectType.Other;
    }
}
