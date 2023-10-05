using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    float time = 0;
    float runTime;
    public void EnterEnemy(Enemy enemy)
    {

        enemy.Stop();
        time = 0;
        runTime = Random.Range(1f, 2f);
    }
    public void OnExcute(Enemy enemy)
    {
        enemy.FindTarget();

        if (enemy.target != null && enemy.hasBullet)
        {
           
            enemy.ChangeState(new AttackState());
        }
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
