//--------------------------------------------------------------------------------
//------------------------------TowerCollision.cs---------------------------------
//------------------------------Eric Galway---------------------------------------
//------------------------------101252535-----------------------------------------
//------------------------------Last Modified: 22/10/2021-------------------------
//------------------------------Description---------------------------------------
//             Manages the detection of enemies, targets the enemy
//             closest to earth and the rotation of towers to face 
//             allows it enemy target. Finally, fires at enemy if firing rate 
//------------------------------Revision History----------------------------------
//------------------------------Version 1.5 - Updated tower rotation using SLERP

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class that manages tower rotation and interactions with enemy targets
public class TowerCollision : MonoBehaviour
{
    GameObject m_parent;
    public float m_fRotationSpeed;
    public bool m_bWithinRange;
    private float m_fFiringCounter;
    Quaternion m_wantedRotation;
    GameObject m_targetEnemy;
    public bool debugTest = false;
    // Start is called before the first frame update
    void Start()
    {
        m_parent = transform.parent.gameObject;
        m_fRotationSpeed = 0.9f;
        m_fFiringCounter = 0.0f;
        m_wantedRotation = Quaternion.Euler(0, 0, 0);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemySpawner.m_fStartLevel)
        {
            m_targetEnemy = FindTarget();
            if (m_targetEnemy != null)
            {
                m_fFiringCounter += Time.deltaTime;
                RotateTower(m_targetEnemy);
                ShootBullet();
            }
        }
    }
    //Detects collision within its radius. If enemies are detected uses the enemies distance traveled to find which is
    //closest to Earth.
    GameObject FindTarget()
    {
        GameObject temp = null;
        //Generates list to hold all collisions
        List<Collider2D> colliderList = new List<Collider2D>();
        GetComponent<Collider2D>().OverlapCollider(new ContactFilter2D(), colliderList);
        //Sets value to check distance traveled to find largest value
        float highestDistanceTravelled = -Mathf.Infinity;

        foreach (Collider2D coll in colliderList)
        {
            //Checks if collision is with an enemy and finds the one closest to Earth
            if (coll.GetComponent<EnemyAttributes>() && highestDistanceTravelled < coll.gameObject.GetComponent<EnemyAttributes>().m_fDistanceTravelled)
            {
                highestDistanceTravelled = coll.gameObject.GetComponent<EnemyAttributes>().m_fDistanceTravelled;
                temp = coll.gameObject;
            }
        }
        return temp;
    }

    //Rotates the tower to face the targeted enemy
    void RotateTower(GameObject targetEnemy)
    {
        
        if (targetEnemy.GetComponent<EnemyAttributes>())
        {
            Vector2 m_vDesiredDirection = new Vector2(targetEnemy.transform.position.x - m_parent.transform.position.x, targetEnemy.transform.position.y - m_parent.transform.position.y);
            m_vDesiredDirection = m_vDesiredDirection.normalized;
            float desiredRotation = MathHelper.CalculateAngle(m_parent.GetComponent<TowerAttributes>().m_vDirection, m_vDesiredDirection) + m_parent.transform.eulerAngles.z;
            m_wantedRotation = Quaternion.Euler(0, 0, desiredRotation);
            m_parent.transform.rotation =Quaternion.Slerp(m_parent.transform.rotation, m_wantedRotation, m_fRotationSpeed);
            m_bWithinRange = true;
        }
        else
            m_bWithinRange = false;
    }
    //If a target is within range uses the firing rate to shoot at enemy
    void ShootBullet()
    {
        if (m_parent.GetComponent<TowerAttributes>().m_bIsActive && m_bWithinRange)
        {
            //Add another check before to see if enemy is within range
            //Then store all enemies within range and sort them to the lowest x value and target this one
            if (m_fFiringCounter >= m_parent.GetComponent<TowerAttributes>().m_fFireRate)
            {
                m_fFiringCounter = 0.0f;
                SoundEffectManager.PlaySoundEffect("FireBullet");

                //Blue tower fires in a 5 bullet spread pattern
                if (m_parent.GetComponent<TowerAttributes>().m_bulletType == BulletType.BLUE_BULLET)
                {
                    float maxSpread = 20.0f;
                    float degreesBetweenBullets = 10;
                    for (int i = 0; i < 5; i++)
                    {
                        Vector2 tempDir = MathHelper.CalculateDirection(transform.rotation.eulerAngles.z - maxSpread + (i * degreesBetweenBullets));
                        BulletManager.Instance().FireBullet(m_parent.GetComponent<TowerAttributes>().m_bulletSpawn.position, tempDir,
                            m_parent.GetComponent<TowerAttributes>().m_bulletType, m_parent.GetComponent<TowerAttributes>().m_bulletRange);
                    }
                }
                else
                {
                    BulletManager.Instance().FireBullet(m_parent.GetComponent<TowerAttributes>().m_bulletSpawn.position, m_parent.GetComponent<TowerAttributes>().m_vDirection,
                        m_parent.GetComponent<TowerAttributes>().m_bulletType, m_parent.GetComponent<TowerAttributes>().m_bulletRange);
                }
            }
        }
    }
}
