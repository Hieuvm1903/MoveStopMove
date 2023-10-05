using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    void EnterEnemy(Enemy enemy);


    void OnExcute(Enemy enemy);

    void OnExit(Enemy enemy);

}
