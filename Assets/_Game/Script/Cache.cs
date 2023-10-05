using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cache : MonoBehaviour
{
    static Dictionary<Collider, Character> ColliderCharacter = new Dictionary<Collider, Character>();
   
    public static Character CharacterCollider (Collider collider)
    {
        if (!ColliderCharacter.ContainsKey(collider))
        {  
            ColliderCharacter.Add(collider, collider.GetComponent<Character>());          
        }

        return ColliderCharacter[collider];
    }
    static Dictionary<Collider, Obstacle> ObstacleCollider = new Dictionary<Collider, Obstacle>();
    public static Obstacle GetObstacle(Collider collider)
    {
        if (!ObstacleCollider.ContainsKey(collider))
        {
            ObstacleCollider.Add(collider, collider.GetComponent<Obstacle>());
        }

        return ObstacleCollider[collider];
    }
}
