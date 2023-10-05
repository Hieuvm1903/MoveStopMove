using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : Bullet
{
   
    public override void OnInit(float attackrange)
    {
        base.OnInit(attackrange);
        rb.angularVelocity = tf.up * 30f;
       
    }
    
}
