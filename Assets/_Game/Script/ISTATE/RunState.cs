using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : IState
{
    float time;
    float runTime;
    public void EnterEnemy(Enemy enemy)
    {
        time = 0;
        runTime = Random.Range(2f, 5f);
        enemy.Move();
        

    }
    public void OnExcute(Enemy enemy)
    {
        time += GameManager.deltaTime;
        if (time > runTime || enemy.isDestination)
        {
            
            enemy.ChangeState(new IdleState());
        }
    }
    public void OnExit(Enemy enemy)
    {

    }
}
