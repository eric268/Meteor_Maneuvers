using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
[System.Serializable]
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

    public static bool m_fStartLevel;

    public static float m_fNumActiveEnemeis;

    GameObject m_earth;

    // Start is called before the first frame update
    void Start()
    {
        m_fGreenEnemySpawnCounter = 0.0f;
        m_fOrangeEnemySpawnCounter = 0.0f;
        m_fPurpleEnemySpawnCounter = 0.0f;
        m_fStartLevel = false;
        m_fNumActiveEnemeis = 0.0f;
        m_earth = FindObjectOfType<EarthAttributes>().gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (m_fStartLevel)
        {
            CheckIfShouldSpawnEnemy(Time.deltaTime, ref m_fNumGreenEnemiesToSpawn, m_fGreenEnemySpawnTimer, ref m_fGreenEnemySpawnCounter, EnemyType.GREEN_ENEMY);
            CheckIfShouldSpawnEnemy(Time.deltaTime, ref m_fNumOrangeEnemiesToSpawn, m_fOrangeEnemySpawnTimer,ref m_fOrangeEnemySpawnCounter, EnemyType.ORANGE_ENEMY);
            CheckIfShouldSpawnEnemy(Time.deltaTime, ref m_fNumPurpleEnemiesToSpawn, m_fPurpleEnemySpawnTimer, ref m_fPurpleEnemySpawnCounter, EnemyType.PURPLE_ENEMY);

            if (m_fNumGreenEnemiesToSpawn == 0 && m_fNumOrangeEnemiesToSpawn == 0 && m_fNumPurpleEnemiesToSpawn == 0)
            {
                if (m_fNumActiveEnemeis ==0)
                {
                    m_fStartLevel = false;
                    PlayerPrefs.SetFloat("HealthRemaining", m_earth.GetComponent<EarthAttributes>().m_iHealthRemaining);
                    PlayerPrefs.SetFloat("ScoreRemaining", Level1UI.m_fTotalScore);
                    PlayerPrefs.SetInt("Saved", 1);
                    PlayerPrefs.Save();
                    SceneManager.LoadScene("GameOver");
                }
            }
        }
    }

    void CheckIfShouldSpawnEnemy(float deltaTime, ref float numEnemiesLeftToSpawn, float spawnTimer, ref float spawnCounter, EnemyType type)
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
