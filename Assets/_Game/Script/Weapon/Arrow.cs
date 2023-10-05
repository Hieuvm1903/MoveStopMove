using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Bullet
{
   
    public override void OnInit(float attackrange)
    {
        base.OnInit(attackrange);
        rb.angularVelocity = tf.forward*10;    
      
    }
  



}
