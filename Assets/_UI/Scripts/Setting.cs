using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : UICanvas
{
    public override void Open()
    {
        GameManager.ChangeState(GameState.Pause);
        LevelManager.Instance.OnPause();
        base.Open();
    }
    public void ContinueButton()
    {
        GameManager.ChangeState(GameState.Gameplay);
        LevelManager.Instance.OnContinue();
        Close(0);
    }

    public void RetryButton()
    {
        LevelManager.Instance.OnRetry();
        Close(0);
    }
    public void MainMenu()
    {
        LevelManager.Instance.OnReset();
        GameManager.ChangeState(GameState.MainMenu);
        UIManager.Instance.OpenUI<MainMenu>();
        UIManager.Instance.CloseUI<InGame>();
        Close(0);
    }
}
