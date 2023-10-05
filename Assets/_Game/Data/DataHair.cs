using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/HairData", order = 1)]
public class DataHair : ScriptableObject
{
    public HairData[] hairDatas;
    public HairData GetHair(HairType type)
    {
        return hairDatas[(int)type];
    }
}
public enum HairType
{
    
    Arrow,
    Cowboy,
    Crown,
    BunnyEar,
    Flower,
    BandoHair,
    Hat,
    PoliceCap,
    HatYellow,
    Headphone,
    Horn,
    Bread,
    Default



}
[System.Serializable]
public class HairData
{
    public GameObject hairPrefab;
    public HairType hairType;
    public Sprite sprite;
    public int price;
    public State state;
    public string buff;
}
