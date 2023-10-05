using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    public DataHair dataHair;
    public DataPant dataPant;
    public DataWeapon dataWeapon;
    public DataBullet dataBullet;
    public DataSkin dataSkin;
    static System.Random _R = new System.Random();
    static T RandomEnumValue<T>()
    {
        var v = Enum.GetValues(typeof(T));
        return (T)v.GetValue(_R.Next(v.Length));
    }
    // Start is called before the first frame update
    private void Start()
    {
        SetWeaponState(GetLastWeapon(), State.Equipped);
    }
    public HairData[] Hairs()
    {
        return dataHair.hairDatas;
    }
    public Pant[] Pants()
    {
        return dataPant.pants;
    }
    public SkinMaterial[] Skins()
    {
        return dataSkin.skinMaterials;
    }
    public DataWeapon Weapons()
    {
        return dataWeapon;
    }



    public HairData Hair(HairType type)
    {
        return dataHair.GetHair(type);
    }
    public Pant Pant(PantType type)
    {
        return dataPant.GetPant(type);
    }
    public SkinMaterial Skin(SkinColor color)
    {
        return dataSkin.GetSkin(color);
    }
    public Weapon Weapon(WeaponType type)
    {
        return dataWeapon.GetWeapon(type);
    }
    public Bullet BulletData(PoolType type)
    {
        return dataBullet.GetBullet(type);
    }

    public (HairType,PantType,SkinColor,WeaponType) RandomSkin()
    {
        
        return (RandomEnumValue<HairType>(),  RandomEnumValue<PantType>(), RandomEnumValue<SkinColor>(), RandomEnumValue<WeaponType>());
    }
    public (HairType, PantType, SkinColor) GetLastOb()
    {
        HairType hair = (HairType)PlayerPrefs.GetInt("hair", 12);
        PantType pant = (PantType)PlayerPrefs.GetInt("pant", 8);
        SkinColor skin = (SkinColor)PlayerPrefs.GetInt("skin", 6);
        
        return (hair, pant, skin);
    }
    public WeaponType GetLastWeapon()
    {
        return (WeaponType)PlayerPrefs.GetInt("weapon", 0);
    }

    //-----------------------------------------------------------------------------
    public void SetLastHair(HairType type)
    {
        PlayerPrefs.SetInt("hair", (int)(type));
    }
    public void SetLastPant(PantType type)
    {
        PlayerPrefs.SetInt("pant", (int)(type));
    }
    public void SetLastColor(SkinColor type)
    {
        PlayerPrefs.SetInt("skin", (int)(type));
    }
    //-------------------------------------------------------------------------------
    public State GetHairState(HairType type)
    {
        return (State)PlayerPrefs.GetInt("hair"+type.ToString());
    }
    public State GetPantState(PantType type)
    {
        return (State)PlayerPrefs.GetInt("pant"+type.ToString());
    }
    public State GetSkinState(SkinColor type)
    {
        return (State)PlayerPrefs.GetInt("skin"+type.ToString());
    }
    //----------------------------------------------------------------------------------
    public void SetHairState(HairType type, State state)
    {
        PlayerPrefs.SetInt("hair"+type.ToString(), (int)state);
    }
    public void SetPantState(PantType type, State state)
    {
        PlayerPrefs.SetInt("pant"+type.ToString(), (int)state);
    }
    public void SetSkinState(SkinColor type, State state)
    {
        PlayerPrefs.SetInt("skin"+type.ToString(), (int)state);
    }
    public void SetLastWeapon(WeaponType type)
    {
        PlayerPrefs.SetInt("weapon", (int)(type));
    }
    public State GetWeaponState(WeaponType type)
    {
        return (State)PlayerPrefs.GetInt("weapon" + type.ToString(),0);
    }
    public void SetWeaponState(WeaponType type, State state)
    {
        PlayerPrefs.SetInt("weapon" + type.ToString(), (int)state);
    }
    public int GetCoin()
    {
        return PlayerPrefs.GetInt("coin", 0);
    }
    public void SetCoin(int coin)
    {
        PlayerPrefs.SetInt("coin", coin);
    }
    public void AddCoin(int num)
    {
        SetCoin(GetCoin() + num);
    }
    public int GetLevel()
    {
        return PlayerPrefs.GetInt("level", 0);
    }
    public void SetLevel(int level)
    {
        PlayerPrefs.SetInt("level", level);
    }
}
public enum State
{
    UnBought,
    Bought,
    Equipped
}
