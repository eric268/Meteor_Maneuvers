//--------------------------------------------------------------------------------
//------------------------------EnemyManager.cs-----------------------------------
//------------------------------Eric Galway---------------------------------------
//------------------------------101252535-----------------------------------------
//------------------------------Last Modified: 21/10/2021-------------------------
//------------------------------Description---------------------------------------
//             This script controls activation and deactivation of enemies
//             moving them to the correct location with correct attributes.
//------------------------------Revision History----------------------------------
//------------------------------Version 1.2 - Updates spawn/return enemy functions

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class that manages all enemy activation and deactivation via a singleton
[System.Serializable]
public class EnemyManager
{
    private static EnemyManager instance = null;

    public Vector3 m_vSpawnLocation;

    //Constructor
    private EnemyManager()
    {
        Initalize();
    }
    //Instantiates or returns singleton
    public static EnemyManager Instance()
    {
        if (instance == null)
        {
            instance = new EnemyManager();
        }

        return instance;
    }
    //Call enemy factor to create a new enemy
    private void AddEnemy(EnemyType type)
    {
        var temp_enemy = EnemyFactory.Instance().createEnemy(type);
        enemyPools[(int)type].Enqueue(temp_enemy);
    }
    //Container for all enemy object pools
    public List<Queue<GameObject>> enemyPools;

    //Generate enemy object pools via a Queue
    private void Initalize()
    {
        enemyPools = new List<Queue<GameObject>>();

        for (int i = 0; i < (int)EnemyType.NUMBER_OF_ENEMY_TYPES; i++)
        {
            enemyPools.Add(new Queue<GameObject>());
        }
    }
    //Generates or dequeues and enemy with correct attributes and position
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
        EnemySpawner.m_fNumActiveEnemeis++;
        return temp_enemy;
    }
    //Deactivates enemy and returns it with correct position and attributes
    public void ReturnEnemy(GameObject returnedEnemy, EnemyType type)
    {
        returnedEnemy.transform.position = m_vSpawnLocation;
        returnedEnemy.GetComponent<EnemyAttributes>().m_vDirection = new Vector3(-1,0,1.0f);
        returnedEnemy.GetComponent<EnemyAttributes>().m_fAngle = 180.0f;
        returnedEnemy.GetComponent<EnemyAttributes>().m_fDistanceTravelled = 0.0f;
        returnedEnemy.GetComponent<Animator>().SetFloat("Health", returnedEnemy.GetComponent<EnemyAttributes>().m_fStartingHealth);
        returnedEnemy.GetComponent<EnemyAttributes>().m_fCurrentHealth = returnedEnemy.GetComponent<EnemyAttributes>().m_fStartingHealth;
        returnedEnemy.SetActive(false);
        EnemySpawner.m_fNumActiveEnemeis--;
        enemyPools[(int)type].Enqueue(returnedEnemy);
    }
}
