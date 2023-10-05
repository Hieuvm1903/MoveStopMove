using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGame : UICanvas
{
    [SerializeField] TextMeshProUGUI aliveTxt;
    [SerializeField] FloatingJoystick joystick;
    [SerializeField] Animator anim;
    private void Awake()
    {
        LevelManager.Instance.player.joystick = joystick;

    }
    public void SettingButton()
   {
        UIManager.Instance.OpenUI<Setting>().Open();
   }
    private void Update()
    {
        aliveTxt.text = "Alive: "+ LevelManager.Instance.aliveEnemies.ToString();
    }
    public override void Open()
    {
        base.Open();
    }
    public override void Close(float delayTime)
    {

        base.Close(delayTime);
    }
}
