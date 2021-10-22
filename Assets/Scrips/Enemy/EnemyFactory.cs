using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyFactory
{
    private static EnemyFactory m_instance = null;

    GameObject m_greenEnemy;
    GameObject m_orangeEnemy;
    GameObject m_purpleEnemy;
    int val = 0;

    private GameObject m_enemyController;

    private EnemyFactory()
    {
        Initialize();
    }

    private void Initialize()
    {
        m_greenEnemy = Resources.Load("Prefabs/Enemy/Green Enemy") as GameObject;
        m_purpleEnemy = Resources.Load("Prefabs/Enemy/Purple Enemy") as GameObject;
        m_orangeEnemy = Resources.Load("Prefabs/Enemy/Orange Enemy") as GameObject;
        m_enemyController = GameObject.Find("EnemyController");
    }


    public static EnemyFactory Instance()
    {
        if (m_instance == null)
        {
            m_instance = new EnemyFactory();
        }

        return m_instance;
    }

    public GameObject createEnemy(EnemyType type)
    {
        GameObject temp_enemy = null;
        switch (type)
        {
            case EnemyType.GREEN_ENEMY:
                temp_enemy = MonoBehaviour.Instantiate(m_greenEnemy);
                temp_enemy.name = "Green" + val;
                val++;
                break;
            case EnemyType.PURPLE_ENEMY:
                temp_enemy = MonoBehaviour.Instantiate(m_purpleEnemy);
                break;
            case EnemyType.ORANGE_ENEMY:
                temp_enemy = MonoBehaviour.Instantiate(m_orangeEnemy);
                break;
        }
        temp_enemy.transform.parent = m_enemyController.gameObject.transform;
        temp_enemy.SetActive(false);

        return temp_enemy;
    }
}
