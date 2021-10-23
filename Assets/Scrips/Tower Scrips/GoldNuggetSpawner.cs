//--------------------------------------------------------------------------------
//------------------------------GoldNuggetSpawner.cs------------------------------
//------------------------------Eric Galway---------------------------------------
//------------------------------101252535-----------------------------------------
//------------------------------Last Modified: 22/10/2021-------------------------
//------------------------------Description---------------------------------------
//             This script controls the spawning rate and location of 
//             all golden nuggets that are to be generated.
//------------------------------Revision History----------------------------------
//------------------------------Version 1.1 - Updated generated position range-----

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class that manages the creation and destruction of all golden nuggets
//Owned by the drill tower
public class GoldNuggetSpawner : MonoBehaviour
{
    float m_fSpawnTimer;
    float m_fSpawnCounter;

    [SerializeField]
    public float minSpawnTime;
    [SerializeField]
    public float maxSpawnTime;

    [SerializeField]
    GameObject m_goldNuggetPrefab;

    [SerializeField]
    public float m_fMinSpawnRange;
    [SerializeField]
    public float m_fMaxSpawnRange;

    // Start is called before the first frame update
    void Start()
    {
        m_fSpawnTimer = 0.0f;
        m_fSpawnCounter = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemySpawner.m_fStartLevel)
        {
            //Every time a new nugget is to be created generates a random timer for creation
            if (m_fSpawnCounter == 0.0f)
            {
                m_fSpawnTimer = Random.Range(minSpawnTime, maxSpawnTime);
            }
            m_fSpawnCounter += Time.deltaTime;
            //If nugget is to be created, creates it in a random direction and range
            if (m_fSpawnCounter >= m_fSpawnTimer)
            {
                m_fSpawnCounter = 0.0f;
                GameObject temp = Instantiate(m_goldNuggetPrefab);
                temp.transform.SetParent(gameObject.transform);

                float xPos = Random.Range(-1.0f, 1.0f);
                float yPos = Random.Range(-1.0f, 1.0f);

                float range = Random.Range(m_fMinSpawnRange, m_fMaxSpawnRange);

                temp.transform.localPosition = new Vector3(xPos * range, yPos * range, 1);
            }
        }
    }
}
