using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    float time;
    float runTime;
    public void EnterEnemy(Enemy enemy)
    {
        enemy.Attack();
        time = 0;
        runTime = 1.2f;
    }
    public void OnExcute(Enemy enemy)
    {
        enemy.CheckAttack();
        time += GameManager.deltaTime;
        if (time > runTime)
        {
            enemy.ChangeState(new RunState());
        }

    }
    public void OnExit(Enemy enemy)
    {

    }
}
