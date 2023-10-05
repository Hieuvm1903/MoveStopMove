using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PantData", order = 1)]

public class DataPant : ScriptableObject
{
    public Pant[] pants;
    public Pant GetPant(PantType type)
    {
        foreach (var pant in pants)
        {
            if (pant.type == type)
            {
                return pant;
            }
        }
        return null;
    }


}
public enum PantType
{
    Batman,
    Chambi,
    Dabao,
    Onion,
    Pokemon,
    RainBow,
    Skull,
    Vantim,
    Default
}
[System.Serializable]
public class Pant
{
    public Material material;
    public Sprite sprite;
    public int price;
    public State state;
    public string buff;
    public PantType type;
}
