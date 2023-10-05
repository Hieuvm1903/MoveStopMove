using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lose : UICanvas
{
    public Text score;

    public void MainMenuButton()
    {
        LevelManager.Instance.OnReset();

        GameManager.ChangeState(GameState.MainMenu);
        UIManager.Instance.OpenUI<MainMenu>();
        Close(0);
    }
    public void RetryButton()
    {
        LevelManager.Instance.OnRetry();
        UIManager.Instance.OpenUI<InGame>();
        
        Close(0);
    }
    public void SetScore(int point)
    {
        score.text = point.ToString();
        DataManager.Instance.AddCoin(point*3);
    }
}
