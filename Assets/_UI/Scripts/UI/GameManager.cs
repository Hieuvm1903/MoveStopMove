using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Events;
public enum GameState { MainMenu, Gameplay, Pause }
public class GameManager : Singleton<GameManager>
{

    public static GameState gameState = GameState.Gameplay;
    public static float deltaTime;

    protected void Awake()
    {
        //base.Awake();
        Input.multiTouchEnabled = false;
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        int maxScreenHeight = 1280;
        float ratio = (float)Screen.currentResolution.width / (float)Screen.currentResolution.height;
        if (Screen.currentResolution.height > maxScreenHeight)
        {
            Screen.SetResolution(Mathf.RoundToInt(ratio * (float)maxScreenHeight), maxScreenHeight, true);
        }
        UIManager.Instance.OpenUI<MainMenu>();
        ChangeState(GameState.Pause);
        CameraFollow.dist = 0.4f;
              
    }
    private void Update()
    {
        if (IsState(GameState.Gameplay))
        {
            deltaTime = Time.deltaTime;
        }
        else if (IsState(GameState.Pause))
        {
            deltaTime = 0;
        }
    }
    public static void ChangeState(GameState state)
    {
        gameState = state;
    }

    public static bool IsState(GameState state)
    {
        return gameState == state;
    }

}
