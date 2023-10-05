using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public static Vector3 offset = new Vector3(0,18,-15);
    public float val = 100;
    public static float dist = 1f;
    public LayerMask layer;
    Obstacle obstacle;
    void Start()
    {
        target = FindObjectOfType<Player>().tf;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        if (target != null 
            //&& GameManager.IsState(GameState.Gameplay)
            )
        {

            Vector3 pos = target.position + offset * dist;
            transform.position = Vector3.Lerp(transform.position, pos, val * Time.deltaTime);
            CheckObstacle();
        }

    }
    public static void OnInit()
    {

    }
    void CheckObstacle()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position,target.position-transform.position, out hit,Mathf.Infinity, layer))
        {
            obstacle = Cache.GetObstacle(hit.collider);
            obstacle.Faded();
        }
        else
        {
            if(obstacle !=null)
            {
                obstacle.GetColorBack();
                obstacle = null;
            }
            
        }
    }
}
