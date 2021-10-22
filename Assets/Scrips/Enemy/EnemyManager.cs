using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyManager
{
    private static EnemyManager instance = null;

    public Vector3 m_vSpawnLocation;

    private EnemyManager()
    {
        Initalize();
    }

    public static EnemyManager Instance()
    {
        if (instance == null)
        {
            instance = new EnemyManager();
        }

        return instance;
    }

    private void AddEnemy(EnemyType type)
    {
        var temp_enemy = EnemyFactory.Instance().createEnemy(type);
        enemyPools[(int)type].Enqueue(temp_enemy);
    }

    public List<Queue<GameObject>> enemyPools;

    private void Initalize()
    {
        enemyPools = new List<Queue<GameObject>>();

        for (int i = 0; i < (int)EnemyType.NUMBER_OF_ENEMY_TYPES; i++)
        {
            enemyPools.Add(new Queue<GameObject>());
        }
    }

    public GameObject SpawnEnemy(Vector2 spawnPosition, EnemyType type)
    {
        GameObject temp_enemy = null;

        if (enemyPools[(int)type].Count < 1)
        {
            AddEnemy(type);
        }
        temp_enemy = enemyPools[(int)type].Dequeue();
        temp_enemy.GetComponent<EnemyAttributes>().m_bIsAlive = true;
        temp_enemy.transform.position = spawnPosition;
        temp_enemy.SetActive(true);
        return temp_enemy;
    }

    public void ReturnEnemy(GameObject returnedEnemy, EnemyType type)
    {
        returnedEnemy.transform.position = m_vSpawnLocation;
        returnedEnemy.GetComponent<EnemyAttributes>().m_vDirection = new Vector3(-1,0,1.0f);
        returnedEnemy.GetComponent<EnemyAttributes>().m_fAngle = 180.0f;
        returnedEnemy.GetComponent<EnemyAttributes>().m_fDistanceTravelled = 0.0f;
        returnedEnemy.GetComponent<Animator>().SetFloat("Health", returnedEnemy.GetComponent<EnemyAttributes>().m_fStartingHealth);
        returnedEnemy.GetComponent<EnemyAttributes>().m_fCurrentHealth = returnedEnemy.GetComponent<EnemyAttributes>().m_fStartingHealth;
        returnedEnemy.SetActive(false);
        enemyPools[(int)type].Enqueue(returnedEnemy);
    }

    public void SetSpawnLocation(Vector3 spawnLoc)
    {
        //m_vSpawnLocation = spawnLoc;
        //Debug.Log("SpawnLocation: " + m_vSpawnLocation);
    }
}
