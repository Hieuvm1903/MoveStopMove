using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Win : UICanvas
{
    [SerializeField] Effect effect;
    public Text score;
    private void OnDisable()
    {
        Camera.main.DOKill();
    }
    public override void Open()
    {
        base.Open();
        Transform tf = LevelManager.Instance.PlayerTF();
       
        Vector3 pos = tf.position+Vector3.forward * 5 + Vector3.up * 2;
        Camera.main.DOFieldOfView(15, 1f).OnComplete(()=> Instantiate(effect, tf.position + Vector3.up * 3, Quaternion.identity).Play());
        //Camera.main.fieldOfView = 15;
        //Camera.main.transform.DORotate(new Vector3(48.46f, 0f, 0), 1f);
        //Camera.main.transform.DOMove(pos, 0.9f);
        //Camera.main.transform.DOLookAt(tf.position,0.9f);     
    }
    public void RetryButton()
    {
        LevelManager.Instance.OnRetry();
        UIManager.Instance.OpenUI<InGame>();

        Close(0.5f);
    }

    public void NextButton()
    {
        LevelManager.Instance.OnNextLevel();
        Close(0.5f);
    }
    public void SetScore(int point)
    {
        score.text = point.ToString();
        DataManager.Instance.AddCoin(point * 3);
    }
    public void MainMenu()
    {
        LevelManager.Instance.OnReset();
        UIManager.Instance.OpenUI<MainMenu>();
        Close(0.5f);
    }
    public override void Close(float delayTime)
    {
        Camera.main.DOFieldOfView(60, 0.5f).OnComplete(()=>DOTween.Clear());
        //Camera.main.fieldOfView = 60;
        base.Close(delayTime);
    }
}
