using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class LevelManager : Singleton<LevelManager>
{
    public Level[] levelPrefabs;

    public Enemy enemyPrefab;
    public Player player;
    public int CharacterAmount => currentLevel.enemyAmount + 1;

    public List<Enemy> enemies = new List<Enemy>();
    private Level currentLevel;
    private int currentEnemies;
    private int levelIndex;
    public int point;

    public int aliveEnemies  ;

    private void Awake()
    {
        levelIndex = DataManager.Instance.GetLevel();
        LoadLevel(levelIndex);
    }
    void Start()
    {
        

        // UIManager.Instance.OpenUI<MainMenu>();
    }
    public Transform PlayerTF()
    {
        player.OnInit();
        player.ChangeAnim(Const.DANCE);
        return player.tf;
    }
    public void LoadLevel(int level)
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }

        if (level < levelPrefabs.Length)
        {
            currentLevel = Instantiate(levelPrefabs[level]);
            currentLevel.OnInit();
        }
        else
        {
            //TODO: level vuot qua limit
            levelIndex = 0;
            currentLevel = Instantiate(levelPrefabs[0]);
        }
    }
    public void OnInit()
    {
        aliveEnemies = currentLevel.enemyAmount;
        currentEnemies = 0;
        NavMesh.RemoveAllNavMeshData();
        NavMesh.AddNavMeshData(currentLevel.navMeshData);
        player.gameObject.SetActive(true);
        player.tf.position = currentLevel.startPoint.position;
        player.tf.rotation = Quaternion.identity;
        for (int i = 0; i < currentLevel.enemyInMap; i++)
        {
            SpawnNewEnemy();
        }
        
    }
    public void EnemyDeath(Enemy enemy)
    {
        enemies.Remove(enemy);
        aliveEnemies--;
        if (currentEnemies < CharacterAmount-1)
        {
            SpawnNewEnemy().ChangeState(new RunState());
            
        }
        else
        {
            if(!player.isDead && enemies.Count == 0)
            {
                GameManager.ChangeState(GameState.Pause);
                player.ChangeAnim(Const.WIN);
                //Camera.main.transform.DOMove(player.tf.forward*5 + player.tf.up*2, 1f);
                //Camera.main.transform.DOLookAt(player.tf.position, 1f);
                UIManager.Instance.OpenUI<Win>().SetScore(point);
                UIManager.Instance.CloseUI<InGame>();
            }
        }
       
    }


    Enemy SpawnNewEnemy()
    {
        Enemy enemy = PoolLearning.Spawn(PoolType.Character, RandomNavmeshLocation(100f), Quaternion.identity) as Enemy;
        enemy.ChangeRandomSkin();
        enemy.OnInit();
        enemies.Add(enemy);
        currentEnemies++;
        return enemy;

    }
    public Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position + Vector3.up;

        }

        return finalPosition;
    }

    public void OnPause()
    {

        for (int i = 0; i< enemies.Count;i++)
        {
             enemies[i].Stop();
            
        }
    }
    public void OnContinue()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].Continue();
           
        }
    }
    public void OnStartGame()
    {
        GameManager.ChangeState(GameState.Gameplay);
        OnInit();
        point = 0;
        ChangeSkinPlayer();
        player.OnInit();
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].ChangeState(new RunState());

        }

    }


    public void ChangeSkinPlayer()
    { player.ChangeSkin(); }    

    public void OnFinishGame()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].ChangeState(null);
            enemies[i].Stop();
        }
    }

    public void OnReset()
    {
        OnFinishGame();
        enemies.Clear();
        PoolLearning.CollectAll();
        
    }

    internal void OnRetry()
    {
        OnReset();
        LoadLevel(levelIndex);
        OnStartGame();
   }

    internal void OnNextLevel()
    {
        levelIndex++;
        DataManager.Instance.SetLevel(levelIndex);
        OnReset();
        LoadLevel(levelIndex);
        OnInit();
        OnStartGame();
        UIManager.Instance.OpenUI<InGame>();
    }
    

}

