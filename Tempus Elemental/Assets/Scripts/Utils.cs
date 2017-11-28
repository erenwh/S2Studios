using UnityEngine;

public enum ObjectType { Other, Player, Projectile, PowerUp, Distortion, Wall, };
public enum DistortionType { SlowDown, SpeedUp, Freeze, Reverse, };
public enum PowerUpType { SlowDown, SpeedUp, Freeze, AddDamage, AddTime, Reverse,  };

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
        if (obj.tag.StartsWith("Fire", System.StringComparison.CurrentCulture)) 
        {
            return ObjectType.Projectile;
        }
		if (obj.tag.StartsWith("Powerup", System.StringComparison.CurrentCulture))
		{
            return ObjectType.PowerUp;
		}
        return ObjectType.Other;
    }

    public static void SelectMap(GameObject map1, GameObject map2, GameObject map3)
    {


        //Something went wrong when finding the maps' gameObjects
        if (map1 == null || map2 == null || map3 == null) return;

        if (Game.Instance.mapSelected == 1)
        {
            map1.SetActive(true);
            map2.SetActive(false);
            map3.SetActive(false);
        }
        else if (Game.Instance.mapSelected == 2)
        {
            map1.SetActive(false);
            map2.SetActive(true);
            map3.SetActive(false);
        }
        else if (Game.Instance.mapSelected == 3)
        {
            map1.SetActive(false);
            map2.SetActive(false);
            map3.SetActive(true);
        }
        else //default to one in error case
        {
            map1.SetActive(true);
            map2.SetActive(false);
            map3.SetActive(false);
        }
    }
}
