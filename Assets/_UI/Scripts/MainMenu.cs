using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class MainMenu : UICanvas
{
    [SerializeField] TextMeshProUGUI textCoin;
    [SerializeField] GameObject panel;
    [SerializeField] Animator anim;
    private void Start()
    {
        OnInitial();
    }
    public override void Open()
    {
        base.Open();
        OnInitial();
        SetCoin();
    }
   


    private void OnEnable()
    {
        transform.localScale = Vector3.one;
        panel.SetActive(true);
    }
    public void OnInitial()
    {
        Transform tf = LevelManager.Instance.PlayerTF();
        Vector3 pos = tf.forward * -7 + tf.up * 2;
        //Camera.main.transform.position = pos + tf.position;
        //Camera.main.transform.LookAt(tf);
        CameraFollow.dist = 0.4f;
    }
    public void ShopButton()
    {
        UIManager.Instance.OpenUI<Shop>();
        Close(0.5f);

    }
    public void ShopWeapon()
    {
        UIManager.Instance.OpenUI<ShopWeapon>();
        Close(0.5f);
    }
    public void PlayButton()
    {

        //Camera.main.transform.DOMove(LevelManager.Instance.PlayerTF().position + CameraFollow.offset, 0.9f).OnComplete(() =>
        //{
            UIManager.Instance.OpenUI<InGame>();
            LevelManager.Instance.OnStartGame();
        //Camera.main.transform.LookAt(LevelManager.Instance.PlayerTF().position + Vector3.down * 3) ;
        Camera.main.DOFieldOfView(60, 0.5f).OnComplete(() => DOTween.Clear());
        DOTween.To(x => CameraFollow.dist = x,0.4f,1,0.5f);
        //}
        //);
        //Camera.main.transform.DOLookAt(LevelManager.Instance.PlayerTF().position + Vector3.down * 4, 0.9f);
        //Camera.main.fieldOfView = 60;
        //CameraFollow.dist = 1;
        Close(0.5f);

    }
    public void SetCoin()
    {
        textCoin.text = DataManager.Instance.GetCoin().ToString();
    }
    public override void Close(float delayTime)
    {
        anim.SetTrigger("exit");
        base.Close(delayTime);
    }
    
}
