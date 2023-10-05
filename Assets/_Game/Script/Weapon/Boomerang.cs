using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : Bullet
{
    bool isStop = false;
    // Update is called once per frame
    public override void OnInit(float attackrange)
    {
        base.OnInit(attackrange);
        isStop = false;
        rb.angularVelocity = tf.up * 30f;
    }
    public override void Stop()
    {
        base.Stop();
        isStop = true;

    }
    public override void CheckTime()
    {        
            time += GameManager.deltaTime;
            if (time >= 2 * timeDespawn)
            {
                OnDespawn();
            }
            else
            if (!isStop &&time >= timeDespawn)
            {
                //rb.velocity = Vector3.zero;
                target = character.throwPoint.position;
            }
    }
    
}
