using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionHandler : MonoBehaviour
{
    private Collider2D m_collider;
    private WaypointAttributes[] m_waypointArray;

    private bool m_bShipDestoryed;

    // Start is called before the first frame update
    void Start()
    {
        m_waypointArray = GameObject.FindObjectsOfType<WaypointAttributes>();
        m_collider = GetComponent<Collider2D>();
        m_bShipDestoryed = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckCollisionWithWayPoint();
        CheckCollisions();
    }

    void CheckCollisionWithWayPoint()
    {
        foreach(WaypointAttributes wayPoint in m_waypointArray)
        {
            if (m_collider.OverlapPoint(new Vector2(wayPoint.gameObject.transform.position.x, wayPoint.gameObject.transform.position.y)))
            {
                if (GetComponent<EnemyAttributes>().m_vDirection != wayPoint.m_vDirection)
                {
                    Debug.Log("Enemy Dir: " + GetComponent<EnemyAttributes>().m_vDirection);
                    Debug.Log("Waypoint Dir: " + wayPoint.m_vDirection);
                    Debug.Log("Angle: " + MathHelper.CalculateAngle(new Vector2(GetComponent<EnemyAttributes>().m_vDirection.x, GetComponent<EnemyAttributes>().m_vDirection.y), 
                        new Vector2(wayPoint.m_vDirection.x, wayPoint.m_vDirection.y)));

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

            }
            else if (coll.gameObject.GetComponent<EarthAttributes>())
            {

            }
            else
            {
                //For now this is the green ship
            }
        }
    }
}
