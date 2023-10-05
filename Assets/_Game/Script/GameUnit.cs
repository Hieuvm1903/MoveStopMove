using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUnit : MonoBehaviour
{
    public Transform tf;
    public PoolType type;
}
public enum PoolType
{
    Boomerang = 0,
    Arrow = 1,
    Axe = 2,
    None = 3,
    Character = 4
    
    
}