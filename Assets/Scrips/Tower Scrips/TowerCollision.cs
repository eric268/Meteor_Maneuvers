using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    GameObject FindTarget()
    {
        GameObject temp = null;
        List<Collider2D> colliderList = new List<Collider2D>();
        GetComponent<Collider2D>().OverlapCollider(new ContactFilter2D(), colliderList);
        Debug.Log(colliderList.Count);
        float highestDistanceTravelled = -Mathf.Infinity;

        foreach (Collider2D coll in colliderList)
        {
            if (coll.GetComponent<EnemyAttributes>() && highestDistanceTravelled < coll.gameObject.GetComponent<EnemyAttributes>().m_fDistanceTravelled)
            {
                highestDistanceTravelled = coll.gameObject.GetComponent<EnemyAttributes>().m_fDistanceTravelled;
                temp = coll.gameObject;
            }
        }
        return temp;
    }


    void RotateTower(GameObject targetEnemy)
    {
        
        if (targetEnemy.GetComponent<EnemyAttributes>())
        {
            //m_parent.GetComponent<TowerAttributes>().m_vDirection = MathHelper.CalculateDirection(m_parent.transform.eulerAngles.z);
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
