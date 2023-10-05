using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/DataWeapon", order = 1)]

public class DataWeapon : ScriptableObject
{
    public Weapon[] weapons;
    public Weapon GetWeapon(WeaponType type)
    {
        foreach(var weapon in weapons)
        {
            if(weapon.type == type)
            {
                return weapon;
            }
        }
        return null;
    }
}


public enum WeaponType
{
   // Candy = PoolType.Candy,
    Boomerang = PoolType.Boomerang,
  //  Hammer = PoolType.Hammer,
    Arrow = PoolType.Arrow,
    Axe = PoolType.Axe



}
