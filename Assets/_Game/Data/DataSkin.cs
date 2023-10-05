using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SkinData", order = 1)]

public class DataSkin : ScriptableObject
{
    public SkinMaterial[] skinMaterials;
    public SkinMaterial GetSkin(SkinColor color)
    {

        foreach (var skin in skinMaterials)
        {
            if (skin.color == color)
            {
                return skin;
            }
        }
        return null;
    }

}
public enum SkinColor
{
    Red,
    Yellow,
    Blue,
    Gray,
    White,
    Black,
    Default



}
[System.Serializable]
public class SkinMaterial
{

    public Material skin;
    public SkinColor color;
    public Color color1;
    public int price;
    
}
