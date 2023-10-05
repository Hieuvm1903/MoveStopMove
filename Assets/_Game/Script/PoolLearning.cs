using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PoolLearning 
{
    static Dictionary<PoolType, Pool> dictOfPool = new Dictionary<PoolType, Pool>();
    public  static void PreLoad(GameUnit gameUnit, Transform parent, int amount)
    {
        if(!dictOfPool.ContainsKey(gameUnit.type))
        {
            Pool pool = new Pool();
            pool.PreLoad(gameUnit,parent, amount);
            dictOfPool.Add(gameUnit.type, pool);
        }
       
    }
   public static GameUnit Spawn(PoolType pool ,Vector3 pos, Quaternion rot) 
    {
        GameUnit unit = null;
        if (dictOfPool.ContainsKey(pool))
        {
          unit = dictOfPool[pool].Spawn();
          
        }

        else
        {
            //TODO
        }
        unit.tf.SetPositionAndRotation(pos, rot);
        return unit;
    }

    public static void DeSpawn(GameUnit gameUnit)
    {
        dictOfPool[gameUnit.type].DeSpawn(gameUnit);
        dictOfPool[gameUnit.type].InActive(gameUnit);
    }
    public static void CollectAll()
    {
        foreach( var pool in dictOfPool)
        {
            pool.Value.CollectAll();
        }
    }
    public class Pool
    {
        List<GameUnit> gameUnits = new List<GameUnit>();
        List<GameUnit> isActive = new List<GameUnit>();
        GameUnit prefab;
        Transform parent;
        public GameUnit Spawn()
        {
            GameUnit unit  = null;
            if(gameUnits.Count >0)
            {
                unit = gameUnits[0];
                
                gameUnits.RemoveAt(0);

            }
                 else
            {
                unit = GameObject.Instantiate(prefab, parent);

            }
            unit.gameObject.SetActive(true);
            isActive.Add(unit);
            return unit;
        }
        public void DeSpawn(GameUnit gameUnit)
        {
            gameUnit.gameObject.SetActive(false);
            gameUnits.Add(gameUnit);
            
        }
        public void InActive(GameUnit gameUnit)
        {
            isActive.Remove(gameUnit);
        }
        public void CollectAll()
        {
            foreach(var unit in isActive)
            {
                DeSpawn(unit);
            }
            isActive.Clear();
        }
        public void PreLoad(GameUnit gameUnit, Transform parent, int amount)
        {
            prefab = gameUnit;
            this.parent = parent;
            for(int i = 0; i< amount; i++)
            {

                GameUnit game = GameObject.Instantiate(gameUnit, parent);
                gameUnits.Add(game);
                game.gameObject.SetActive(false);
            }
        }



    }
}
