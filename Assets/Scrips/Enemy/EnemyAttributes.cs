//--------------------------------------------------------------------------------
//------------------------------EnemyAttributes.cs--------------------------------
//------------------------------Eric Galway---------------------------------------
//------------------------------101252535-----------------------------------------
//------------------------------Last Modified: 21/10/2021-------------------------
//------------------------------Description---------------------------------------
//             This script controls the attributes for all enemies. It also
//             moves and rotates the enemy. If the enemy dies it stops moving
//             while the animation plays.
//------------------------------Revision History----------------------------------
//------------------------------Version 1.2 - Added pause for death animation-----
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class that controls all enemy movement
public class EnemyAttributes : MonoBehaviour
{

    [SerializeField]
    private float m_fMovementSpeed = 0.0f;
    [SerializeField]
    private float m_fRotationSpeed;
    [SerializeField]
    public float m_fDamageOnHit;

    [SerializeField]
    public float m_fDeathAnimTime;

    [SerializeField]
    private float m_fDeathAnimCounter;

    [SerializeField]
    public Vector3 m_vDirection = new Vector3(-1,0,0);

    public Quaternion m_fWantedRotation;

    [SerializeField]
    public float m_fCashWhenDestroyed;

    public float m_fAngle;

    [SerializeField]
    public float m_fStartingHealth;

    [SerializeField]
    public float m_fCurrentHealth;

    [SerializeField]
    public bool m_bIsAlive;

    [SerializeField]
    public EnemyType m_enemyType;

    public float m_fDistanceTravelled;

    // Start is called before the first frame update
    void Start()
    {
        m_fAngle = 180.0f;
        m_fDeathAnimCounter = 0;
        m_fDistanceTravelled = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_bIsAlive)
        {
            //Moves enemy
            transform.position += (m_vDirection * m_fMovementSpeed)*Time.deltaTime;

            //Gets total distance traveled which is used for tower targeting.
            m_fDistanceTravelled += m_fMovementSpeed * Time.deltaTime;
            //Rotation enemy should have based upon direction
            m_fWantedRotation = Quaternion.Euler(new Vector3(0f, 0f, m_fAngle));
            //Rotates enemy
            transform.rotation = Quaternion.Slerp(transform.rotation, m_fWantedRotation, m_fRotationSpeed);

            //This acts as a safety net that if a enemy travels far enough off the screen they are destroyed
            if (m_fDistanceTravelled > 65.0f)
            {
                EnemyManager.Instance().ReturnEnemy(this, m_enemyType);
            }
        }
        else
        {
            m_fDeathAnimCounter += Time.deltaTime;
            //Checks if enemy death animation is over.
            if (m_fDeathAnimCounter >= m_fDeathAnimTime)
            {
                m_fDeathAnimCounter = 0;
                EnemyManager.Instance().ReturnEnemy(gameObject, m_enemyType);
            }
            
        }
    }
}
