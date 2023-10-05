using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopWeapon : UICanvas
{
    Weapon weapon;
    public int currentID;
    public Button next;
    public Button previous;
    public Button buyBtn;
    public Button equipBtn;
    public Transform display;
    [SerializeField] TextMeshProUGUI coinTxt;
    [SerializeField] TextMeshProUGUI priceTxt;
    [SerializeField] TextMeshProUGUI weaponName;
    [SerializeField] Animator anim;
    int price;
    DataWeapon weapons => DataManager.Instance.Weapons();
    private void Start()
    {
        OnInital();
    }
    private void Update()
    {
        display.Rotate(Vector3.forward * 2);
        display.Rotate(Vector3.up * 2);
        display.Rotate(Vector3.left * 2);
    }
    public override void Close(float delayTime)
    {
        anim.SetTrigger("exit");
        base.Close(delayTime);
    }
    public void OnInital()
    {
        SetCoinText();
        WeaponType weaponType = DataManager.Instance.GetLastWeapon();
        currentID = (int)(weaponType);
        ChangeWeapon();
        ChangeClickButton();
        if (currentID == 0)
        {
            previous.gameObject.SetActive(false);
        }
        if (currentID == weapons.weapons.Length - 1)
        {
            next.gameObject.SetActive(false);
        }
    }
    public void Next()
    {
        currentID++;
        ChangeWeapon();
        ChangeClickButton();
        if(currentID == weapons.weapons.Length-1)
        {
            next.gameObject.SetActive(false);
        }
        previous.gameObject.SetActive(true);
    }
    public void Prev()
    {
        currentID--;
        ChangeWeapon();
        ChangeClickButton();
        if (currentID == 0)
        {
            previous.gameObject.SetActive(false);
        }
        next.gameObject.SetActive(true);
    }
    public void ChangeWeapon()
    {
        if(weapon!= null)
        {
            Destroy(weapon.gameObject);
            Destroy(weapon);
        }
        WeaponType weaponType = (WeaponType)(currentID);
        weapon = Instantiate(weapons.GetWeapon(weaponType),display.position,Quaternion.Euler(0,0,45),  display);
        price = weapon.price;
        priceTxt.text = price.ToString();
        weaponName.text = weapon.nameWP;

    }
    public void Buy()
    {
        if(price<= DataManager.Instance.GetCoin())
        {
            DataManager.Instance.SetWeaponState((WeaponType)(currentID), State.Bought);
            ChangeClickButton();
            DataManager.Instance.AddCoin(-price);
            SetCoinText();
        }
    

    }
    public void Equip()
    {
        WeaponType weaponType = DataManager.Instance.GetLastWeapon();
        DataManager.Instance.SetWeaponState(weaponType, State.Bought);
        DataManager.Instance.SetWeaponState((WeaponType)(currentID), State.Equipped);
        DataManager.Instance.SetLastWeapon((WeaponType)(currentID));
        ChangeClickButton();

    }
    void ChangeClickButton()
    {
        State state = DataManager.Instance.GetWeaponState((WeaponType)currentID);
        switch (state)
        {
            case State.Bought:
                {
                    priceTxt.text = "Bought";

                    buyBtn.gameObject.SetActive(false);
                    equipBtn.gameObject.SetActive(true);
                    break;
                }
            case State.UnBought:
                {
                    buyBtn.gameObject.SetActive(true);
                    equipBtn.gameObject.SetActive(false);
                    break;
                }
            case State.Equipped:
                {
                    priceTxt.text = "Equipped";
                    buyBtn.gameObject.SetActive(false);
                    equipBtn.gameObject.SetActive(false);
                    break;
                }
        }
    }

    void SetCoinText()
    {
        coinTxt.text = DataManager.Instance.GetCoin().ToString();
    }

    public void BackBtn()
    {
        LevelManager.Instance.ChangeSkinPlayer();
        UIManager.Instance.OpenUI<MainMenu>();
        Close(1f);
    }


}
