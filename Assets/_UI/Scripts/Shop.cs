using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : UICanvas
{
    [SerializeField] Transform tab1;
    [SerializeField] Transform tab2;
    [SerializeField] Transform tab3;
    [SerializeField] ShopButton shopButton;
    [SerializeField] GameObject priceHolder;
    [SerializeField] Button buyBtn;
    [SerializeField] Button equipBtn;
    [SerializeField] GameObject coin;
    [SerializeField] TextMeshProUGUI priceTxt;
    [SerializeField] TextMeshProUGUI coinTxt;
    [SerializeField] Animator anim;
    List<ShopButton> shopButtons = new List<ShopButton>();
    int price;
    int isSelect;
    TypeSelect typeSelect;
    private void Start()
    {
        priceHolder.SetActive(false);
        buyBtn.gameObject.SetActive(false);
        equipBtn.gameObject.SetActive(false);
        //OnInital();
    }
    public override void Open()
    {
        base.Open();
        OnInital();
    }
    public override void Close(float delayTime)
    {
        anim.SetTrigger("exit");
        base.Close(delayTime);
    }
    public void OnInital()
    {
        SetCoin();
        foreach(var btn in shopButtons)
        {
            Destroy(btn.gameObject);
            Destroy(btn);
        }
        shopButtons.Clear();
        HairData[] hairs = DataManager.Instance.Hairs();
        for(int i = 0; i < hairs.Length-1;i++)
        {
            int j = i;
            ShopButton sbtn = Instantiate(shopButton, tab1);
            sbtn.AddListen(() => OnSelect((HairType)j));
            State state = DataManager.Instance.GetHairState(((HairType)j));
            sbtn.ChangeImage( DataManager.Instance.Hair((HairType)j).sprite);
            sbtn.ChangeState(state);
            shopButtons.Add(sbtn);
            
        }
        Pant[] pants = DataManager.Instance.Pants();
        for (int i = 0; i < pants.Length-1; i++)
        {
            int j = i;
            ShopButton sbtn = Instantiate(shopButton, tab2);
            sbtn.AddListen(() => OnSelect((PantType)j));
            sbtn.ChangeImage( DataManager.Instance.Pant((PantType)j).sprite);
            State state = DataManager.Instance.GetPantState(((PantType)j));
            sbtn.ChangeState(state);
            shopButtons.Add(sbtn);



        }
        SkinMaterial[] skins = DataManager.Instance.Skins();
        for (int i = 0; i < skins.Length-1; i++)
        {
            int j = i;
            ShopButton sbtn = Instantiate(shopButton, tab3);
            sbtn.AddListen(() => OnSelect((SkinColor)j));
            SkinMaterial skin = DataManager.Instance.Skin((SkinColor)j);
            sbtn.ChangeColor( skin.color1);
            State state = DataManager.Instance.GetSkinState((SkinColor)j);
            sbtn.ChangeState(state);
            shopButtons.Add(sbtn);


        }
    }
    public void Buy()
    {
        if(price <= DataManager.Instance.GetCoin())
        {
            ChangeState(State.Bought);
            ChangeClickButton(State.Bought);
            DataManager.Instance.AddCoin(-price);
            SetCoin();
            OnInital();
        }
  
    }
    public void Equip()
    {
        ChangeState(State.Equipped);
        ChangeClickButton(State.Equipped);
        (HairType hair, PantType pant, SkinColor skin) = DataManager.Instance.GetLastOb();
        switch (typeSelect)
        {
            case TypeSelect.hair:
                {
                    DataManager.Instance.SetHairState(hair, State.Bought);
                    DataManager.Instance.SetLastHair((HairType)isSelect);
                    break;
                }
            case TypeSelect.pant:
                {
                    DataManager.Instance.SetPantState(pant, State.Bought);
                    DataManager.Instance.SetLastPant((PantType)isSelect);
                    break;
                }
            case TypeSelect.skin:
                {
                    DataManager.Instance.SetSkinState(skin, State.Bought);
                    DataManager.Instance.SetLastColor((SkinColor)isSelect);
                    break;
                }


        }
        OnInital();


    }
    public void BackBtn()
    {
        UIManager.Instance.OpenUI<MainMenu>();
        LevelManager.Instance.ChangeSkinPlayer();
        Close(1f);
    }
    void ChangeState(State state)
    {
        switch (typeSelect)
        {
            case TypeSelect.hair:
                {
                    DataManager.Instance.SetHairState((HairType)isSelect, state);
                    break;
                }
            case TypeSelect.pant:
                {
                    DataManager.Instance.SetPantState((PantType)isSelect, state);
                    break;
                }
            case TypeSelect.skin:
                {
                    DataManager.Instance.SetSkinState((SkinColor)isSelect, state);
                    break;
                }


        }
    }
    void OnSelect(HairType type)
    {
        State state = DataManager.Instance.GetHairState(type);
        ChangeClickButton(state);
        HairData hair = DataManager.Instance.Hair(type);
        price = hair.price;
        typeSelect = TypeSelect.hair;
        isSelect = (int)type;
        LevelManager.Instance.player.ChangeHair(type);
        OnInital();


    }
    void OnSelect(PantType type)
    {
        State state = DataManager.Instance.GetPantState(type);
        ChangeClickButton(state);
        Pant pant = DataManager.Instance.Pant(type);
        price = pant.price;
        
        typeSelect = TypeSelect.pant;
        isSelect = (int)type;
        LevelManager.Instance.player.ChangePant(type);
        OnInital();

    }
    void OnSelect(SkinColor type)
    {
        State state = DataManager.Instance.GetSkinState(type);
        SkinMaterial skin = DataManager.Instance.Skin(type);
        price = skin.price;     
        ChangeClickButton(state);
        typeSelect = TypeSelect.skin;
        isSelect = (int)type;
        LevelManager.Instance.player.ChangeSkin(type);
        OnInital();
    }
    void ChangeClickButton(State state)
    {
        buyBtn.gameObject.SetActive(false);
        equipBtn.gameObject.SetActive(false);
        priceHolder.SetActive(true);
        switch (state)
        {
            case State.Bought:
                {
                    equipBtn.gameObject.SetActive(true);
                    coin.SetActive(false);

                    priceTxt.text = "Bought";
                    break;
                }
            case State.UnBought:
                {
                    buyBtn.gameObject.SetActive(true);
                    coin.SetActive(true);
                    priceTxt.text = price.ToString();
                    break;
                }
            case State.Equipped:
                {
                    
                    priceTxt.text = "Equipped";
                    coin.SetActive(false);
                    break;
                }
        }
    }
    void SetCoin()
    {
        coinTxt.text = DataManager.Instance.GetCoin().ToString();
    }
}
public enum TypeSelect
{
    pant,
    hair,
    skin
}
