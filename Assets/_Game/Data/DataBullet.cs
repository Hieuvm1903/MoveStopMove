using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/DataBullet", order = 1)]

public class DataBullet : ScriptableObject

{
    public Bullet[] bullets;
    public Bullet GetBullet(PoolType type)
    {
        foreach(var bullet in bullets)
        {
            if(bullet.type == type)
            {
                return bullet;
            }
        }
        return null;
    }
}


