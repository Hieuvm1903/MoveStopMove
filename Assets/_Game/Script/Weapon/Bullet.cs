using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bullet : GameUnit
{
    public Rigidbody rb;
    public float timeDespawn = 1f;
    public float time;
    public Character character;
    public Vector3 direct;
    public Vector3 target;
    public float speed = 3;
    [SerializeField] ParticleSystem ps;
    private void Update()
    {
        CheckTime();
        tf.position = Vector3.MoveTowards(tf.position, target, GameManager.deltaTime * character.attackRange / timeDespawn);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Const.CHARACTER))
        {
            Character character = Cache.CharacterCollider(other);
            
            if(character != this.character && GameManager.IsState(GameState.Gameplay))
            {
                character.OnDeath();
                this.character.LevelUp();
                OnDespawn();
                //Instantiate(ps,transform.position,transform.rotation);
                ParticlePool.Play(ParticleType.Hit, transform.position, transform.rotation);


                if(this.character is Player)
                {
                    LevelManager.Instance.point++;
                }
                

            }


        }
        if(other.CompareTag(Const.OBSTACLE))
        {
            Stop();
        }
    }
    public virtual void Stop()
    {
        target = transform.position;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
    public virtual void OnInit(float attackrange)
    {
        rb.useGravity = false;
        //rb.position = tf.position;
        target = tf.position + character.attackRange * direct;
       // direct = tf.forward;
        //rb.velocity =  direct* attackrange / timeDespawn;
        //rb.DOMove(tf.position + attackrange * direct, timeDespawn).SetLoops(1) ;
        
        
    }
    public virtual void CheckTime()
    {
        if (time >= timeDespawn)
        {
            OnDespawn();
            
        }
        time += GameManager.deltaTime;
    }
    public void OnDespawn()
    {
        //rb.DOKill();
        //Destroy(gameObject);
        time = 0;
        PoolLearning.DeSpawn(this);
    }

}
