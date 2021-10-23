//--------------------------------------------------------------------------------
//------------------------------EnemyCollisionHandler.cs--------------------------------
//------------------------------Eric Galway---------------------------------------
//------------------------------101252535-----------------------------------------
//------------------------------Last Modified: 22/10/2021-------------------------
//------------------------------Description---------------------------------------
//             This script controls all enemy collisions. Importantly
//             it controls way point collisions which give enemies their
//             steering behavior. Also checks for collision with earth and
//             bullets and reacts accordingly.
//------------------------------Revision History----------------------------------
//------------------------------Version 1.5 - Added Sound Effects------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class that handles all enemy collisions with game objects
public class EnemyCollisionHandler : MonoBehaviour
{
    private Collider2D m_collider;
    //Array which holds all way point game objects
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
    //Checks for collision against all way points. If collision occurs updates direction
    //based upon way point direction
    void CheckCollisionWithWayPoint()
    {
        foreach(WaypointAttributes wayPoint in m_waypointArray)
        {
            if (m_collider.OverlapPoint(new Vector2(wayPoint.gameObject.transform.position.x, wayPoint.gameObject.transform.position.y)))
            {
                if (GetComponent<EnemyAttributes>().m_vDirection != wayPoint.m_vDirection)
                {
                    //Sets the angel given the new direction
                    GetComponent<EnemyAttributes>().m_fAngle = transform.eulerAngles.z + MathHelper.CalculateAngle(GetComponent<EnemyAttributes>().m_vDirection, wayPoint.m_vDirection);
                    
                    //Updates direction
                    GetComponent<EnemyAttributes>().m_vDirection = wayPoint.m_vDirection;
                }

            }
        }
    }
    //Checks for collision against most game objects
    void CheckCollisions()
    {
        //List of collisions
        List<Collider2D> possibleCollisions = new List<Collider2D>();
        //Populates list with collisions
        m_collider.OverlapCollider(new ContactFilter2D(), possibleCollisions);

        foreach (Collider2D coll in possibleCollisions)
        {
            //Checks if the collision is with a bullet
            if (coll.gameObject.GetComponent<BulletAttributes>())
            {
                //Returns bullet to queue if correct and decrements own health
                GetComponent<EnemyAttributes>().m_fCurrentHealth -= coll.gameObject.GetComponent<BulletAttributes>().m_fBulletDamage;
                BulletManager.Instance().ReturnBullet(coll.gameObject, coll.gameObject.GetComponent<BulletAttributes>().m_bulletType);
               
                //Updates UI if enemy has been killed and plays explosion effect
                if (GetComponent<EnemyAttributes>().m_fCurrentHealth <= 0.0f)
                {
                    GetComponent<EnemyAttributes>().m_bIsAlive = false;
                    GetComponent<Animator>().SetFloat("Health", 0);
                    Level1UI.m_fTotalCash += GetComponent<EnemyAttributes>().m_fCashWhenDestroyed;
                    Level1UI.m_fTotalScore += GetComponent<EnemyAttributes>().m_fCashWhenDestroyed;
                    SoundEffectManager.PlaySoundEffect("Explosion");
                }
                //Plays bullet sound effect 
                else
                {
                    SoundEffectManager.PlaySoundEffect("BulletHit");
                }
            }
            //Checks for collision against earth and updates UI/sounds effects
            else if (coll.gameObject.GetComponent<EarthAttributes>())
            {
                coll.gameObject.GetComponent<EarthAttributes>().m_iHealthRemaining -= GetComponent<EnemyAttributes>().m_fDamageOnHit;
                GetComponent<EnemyAttributes>().m_bIsAlive = false;
                GetComponent<Animator>().SetFloat("Health", 0);
                SoundEffectManager.PlaySoundEffect("Explosion");
            }
        }
    }
}
