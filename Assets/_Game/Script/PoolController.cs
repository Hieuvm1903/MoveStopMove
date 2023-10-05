using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolController : MonoBehaviour
{
    [SerializeField] PoolAmount[] prePools;
    [SerializeField] ParticleAmount[] Particle;

    private void Awake()
    { 
        for (int i = 0; i < prePools.Length; i++)
            PoolLearning.PreLoad(prePools[i].gameUnit, prePools[i].parent, prePools[i].amount);

        for (int i = 0; i < Particle.Length; i++)
        {
            ParticlePool.Preload(Particle[i].prefab, Particle[i].amount, Particle[i].root);
            ParticlePool.Shortcut(Particle[i].particleType, Particle[i].prefab);
        }
    }

}
[System.Serializable]
public class PoolAmount
{
    public GameUnit gameUnit;
        
    public Transform parent;
    public int amount;
}
[System.Serializable]
public class ParticleAmount
{
    public Transform root;
    public ParticleType particleType;
    public ParticleSystem prefab;
    public int amount;
}


public enum ParticleType
{
    Hit
}