using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private float m_fDeathAnimCounter;

    [SerializeField]
    public Vector3 m_vDirection = new Vector3(-1,0,0);

    public Quaternion m_fWantedRotation;

    public float m_fAngle;

    [SerializeField]
    public float m_fStartingHealth;

    [SerializeField]
    public float m_fCurrentHealth;

    [SerializeField]
    public bool m_bIsAlive;

    public EnemyType m_enemyType;

    // Start is called before the first frame update
    void Start()
    {
        m_fAngle = 180.0f;
        m_fDeathAnimCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_bIsAlive)
        {
            transform.position += (m_vDirection * m_fMovementSpeed)*Time.deltaTime;
            m_fWantedRotation = Quaternion.Euler(new Vector3(0f, 0f, m_fAngle));
            transform.rotation = Quaternion.Slerp(transform.rotation, m_fWantedRotation, m_fRotationSpeed);
        }
        else
        {
            
            m_fDeathAnimCounter += Time.deltaTime;
            if (m_fDeathAnimCounter >= m_fDeathAnimTime)
            {
                m_fDeathAnimCounter = 0;
                EnemyManager.Instance().ReturnEnemy(gameObject, m_enemyType);
            }
            
        }
    }
}
