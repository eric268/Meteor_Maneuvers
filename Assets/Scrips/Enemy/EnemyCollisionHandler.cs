using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionHandler : MonoBehaviour
{
    private Collider2D m_collider;
    private WaypointAttributes[] m_waypointArray;

    // Start is called before the first frame update
    void Start()
    {
        m_waypointArray = GameObject.FindObjectsOfType<WaypointAttributes>();
        m_collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GetComponent<EnemyAttributes>().m_bIsAlive)
        {
            CheckCollisionWithWayPoint();
            CheckCollisions();
        }
    }

    void CheckCollisionWithWayPoint()
    {
        foreach(WaypointAttributes wayPoint in m_waypointArray)
        {
            if (m_collider.OverlapPoint(new Vector2(wayPoint.gameObject.transform.position.x, wayPoint.gameObject.transform.position.y)))
            {
                if (GetComponent<EnemyAttributes>().m_vDirection != wayPoint.m_vDirection)
                {

                    GetComponent<EnemyAttributes>().m_fAngle = transform.eulerAngles.z + MathHelper.CalculateAngle(GetComponent<EnemyAttributes>().m_vDirection, wayPoint.m_vDirection);
                    GetComponent<EnemyAttributes>().m_vDirection = wayPoint.m_vDirection;
                }

            }
        }
    }

    void CheckCollisions()
    {
        List<Collider2D> possibleCollisions = new List<Collider2D>();
        m_collider.OverlapCollider(new ContactFilter2D(), possibleCollisions);

        foreach (Collider2D coll in possibleCollisions)
        {
            if (coll.gameObject.GetComponent<BulletAttributes>())
            {
                GetComponent<EnemyAttributes>().m_fCurrentHealth -= coll.gameObject.GetComponent<BulletAttributes>().m_fBulletDamage;
                BulletManager.Instance().ReturnBullet(coll.gameObject, coll.gameObject.GetComponent<BulletAttributes>().m_bulletType);
                if (GetComponent<EnemyAttributes>().m_fCurrentHealth <= 0.0f)
                {
                    GetComponent<EnemyAttributes>().m_bIsAlive = false;
                    GetComponent<Animator>().SetFloat("Health", 0);
                }
            }
            else if (coll.gameObject.GetComponent<EarthAttributes>())
            {
                coll.gameObject.GetComponent<EarthAttributes>().m_iHealthRemaining -= GetComponent<EnemyAttributes>().m_fDamageOnHit;
                GetComponent<EnemyAttributes>().m_bIsAlive = false;
                GetComponent<Animator>().SetFloat("Health", 0);
            }
            else
            {
                //For now this is the green ship
            }
        }
    }
}
