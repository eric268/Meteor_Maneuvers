using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Number of Enemies")]
    public float m_fNumGreenEnemiesToSpawn;
    public float m_fNumOrangeEnemiesToSpawn;
    public float m_fNumPurpleEnemiesToSpawn;

    [Header("Enemy Spawn Rate")]
    public float m_fGreenEnemySpawnTimer;
    public float m_fOrangeEnemySpawnTimer;
    public float m_fPurpleEnemySpawnTimer;

    [Header("Timer Counters")]
    public float m_fGreenEnemySpawnCounter;
    public float m_fOrangeEnemySpawnCounter;
    public float m_fPurpleEnemySpawnCounter;

    public bool m_fStartLevel;

    // Start is called before the first frame update
    void Start()
    {
        m_fGreenEnemySpawnCounter = 0.0f;
        m_fOrangeEnemySpawnCounter = 0.0f;
        m_fPurpleEnemySpawnCounter = 0.0f;
        m_fStartLevel = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (m_fStartLevel)
        {
            CheckIfShouldSpawnEnemy(Time.deltaTime, m_fNumGreenEnemiesToSpawn, m_fGreenEnemySpawnTimer, ref m_fGreenEnemySpawnCounter, EnemyType.GREEN_ENEMY);
            CheckIfShouldSpawnEnemy(Time.deltaTime, m_fNumOrangeEnemiesToSpawn, m_fOrangeEnemySpawnTimer,ref m_fOrangeEnemySpawnCounter, EnemyType.ORANGE_ENEMY);
            CheckIfShouldSpawnEnemy(Time.deltaTime, m_fNumPurpleEnemiesToSpawn, m_fPurpleEnemySpawnTimer, ref m_fPurpleEnemySpawnCounter, EnemyType.PURPLE_ENEMY);
        }
    }

    void CheckIfShouldSpawnEnemy(float deltaTime,float numEnemiesLeftToSpawn, float spawnTimer, ref float spawnCounter, EnemyType type)
    {
        if (numEnemiesLeftToSpawn > 0)
        {
            spawnCounter += deltaTime;
            if (spawnCounter >= spawnTimer)
            {
                spawnCounter = 0.0f;
                EnemyManager.Instance().SpawnEnemy(transform.position,type);
                numEnemiesLeftToSpawn--;
            }
        }
    }
}
