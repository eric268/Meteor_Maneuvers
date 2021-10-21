using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttributes : MonoBehaviour
{
    private Vector2 m_vDirection;

    [SerializeField]
    public float m_bulletRange;

    [SerializeField]
    public Transform m_bulletSpawn;

    [SerializeField]
    private BulletType m_bulletType;

    [SerializeField]
    public float m_fFireRate;

    private float m_fFiringCounter;

    [SerializeField]
    public float m_fTowerCost;
    
    public bool m_bIsActive;

    // Start is called before the first frame update
    void Start()
    {
        m_bIsActive = false;
        m_vDirection = new Vector2(1.0f, 0.0f);
        m_fFiringCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_bIsActive)
        {
            //Add another check before to see if enemy is within range
            //Then store all enemies within range and sort them to the lowest x value and target this one
            m_fFiringCounter += Time.deltaTime;
            if (m_fFiringCounter >= m_fFireRate)
            {
                m_fFiringCounter = 0.0f;
                m_vDirection = CalculateTowerDirection();
                BulletManager.Instance().FireBullet(m_bulletSpawn.position, m_vDirection, m_bulletType, m_bulletRange);
            }
        }
    }

    public Vector2 CalculateTowerDirection()
    {
        float zRotation = transform.rotation.eulerAngles.z;
        return new Vector2(Mathf.Cos(zRotation * Mathf.Deg2Rad), Mathf.Sin(zRotation * Mathf.Deg2Rad));
    }
    
}
/*
void Tower::AimAtTarget(Vector2 targetPos)
{
    float x1, y1;
    GetSprite()->GetPosition(x1, y1);
    float x2 = targetPos.GetX();
    float y2 = targetPos.GetY();
    Vector2 vec = MathFunctions::VectorFromTwoPoints(x1, y1, x2, y2);
    m_angleToTarget = MathFunctions::CalculateAngle(GetDirection(), vec);
    GetSprite()->SetAngle(m_angleToTarget);
    m_shootingDir = Vector2(cosf(m_angleToTarget), sinf(m_angleToTarget));
}
*/