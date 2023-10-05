using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy : Character
{
    public NavMeshAgent navMeshAgent;
    public Vector3 destination;
    IState currentState;
    public Indicator indicator;
    public bool isDestination => Vector3.Distance(tf.position, destination + (tf.position.y - destination.y) * Vector3.up) < 0.1f;
    void Start()
    {
        indicator = Instantiate(indicator, ScreenIndicator.tf);
        indicator.SetImageColor(skin.material.color);
        
        
    }
    void Update()
    {
        if (GameManager.IsState(GameState.Gameplay))
        {
            if (isDead) return;
            currentState?.OnExcute(this);
        }
    }
    public override void OnInit()
    {
        base.OnInit();
        
        
        indicator.SetImageColor(skin.material.color);
        indicator.SetLevel(level);

    }
    public override void LevelUp()
    {
        base.LevelUp();
        indicator.SetLevel(level);
    }
    public void Move()
    {
        hasBullet = true;
        navMeshAgent.enabled = true;
        ChangeAnim(Const.RUN);
        SetDestination(LevelManager.Instance.RandomNavmeshLocation(15f));
    }
    public void Stop()
    {
        navMeshAgent.enabled = false;
        if (!isDead)
        {            
            ChangeAnim(Const.IDLE); 
        }

    }
    void SetDestination(Vector3 destination)
    {
        navMeshAgent.SetDestination(destination);
        this.destination = destination;
        this.destination.y = 0;
    }
    public override void OnDespawn()
    {
        base.OnDespawn();
        
    }
    public override void OnDeath()
    {
        base.OnDeath();
        Stop();
        indicator.gameObject.SetActive(false);
        LevelManager.Instance.EnemyDeath(this);
    }
    public void ChangeState(IState newstate)
    {

        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = newstate;
        if (currentState != null)
        {
            currentState.EnterEnemy(this);
        }

    }
    public void Continue()
    {
        ChangeState(currentState);
        navMeshAgent.enabled = true;
        SetDestination(destination);
    }
    
}
