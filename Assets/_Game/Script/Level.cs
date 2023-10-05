using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Level : MonoBehaviour
{
    public NavMeshData navMeshData;
    public Transform startPoint;
    public int enemyAmount = 50;
    public int enemyInMap = 10;
    // Start is called before the first frame update
    public void OnInit()
    {
        
    }
}
