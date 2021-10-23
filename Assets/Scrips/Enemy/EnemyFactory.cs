//--------------------------------------------------------------------------------
//------------------------------EnemyFactory.cs-----------------------------------
//------------------------------Eric Galway---------------------------------------
//------------------------------101252535-----------------------------------------
//------------------------------Last Modified: 21/10/2021-------------------------
//------------------------------Description---------------------------------------
//             This script controls the creation of all enemies.  
//------------------------------Revision History----------------------------------
//------------------------------Version 1.0 - Added createEnemy function----------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class that handles enemy creation and singleton
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
    //Sets enemies to appropriate enemy prefab
    private void Initialize()
    {
        m_greenEnemy = Resources.Load("Prefabs/Enemy/Green Enemy") as GameObject;
        m_purpleEnemy = Resources.Load("Prefabs/Enemy/Purple Enemy") as GameObject;
        m_orangeEnemy = Resources.Load("Prefabs/Enemy/Orange Enemy") as GameObject;
        m_enemyController = GameObject.Find("EnemyController");
    }

    //Gets or initializes singleton 
    public static EnemyFactory Instance()
    {
        if (m_instance == null)
        {
            m_instance = new EnemyFactory();
        }

        return m_instance;
    }
    //Creates enemy given type
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
