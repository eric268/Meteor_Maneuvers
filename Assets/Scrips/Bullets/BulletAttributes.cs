using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletAttributes : MonoBehaviour
{
    public float m_fBulletSpeed;
    public float m_totalDistanceTravelled;
    public float m_range;
    public float m_fBulletDamage;
    public Vector3 m_fDirection;
    public BulletType m_bulletType;

    public float m_fBulletLifeTimeCounter;
    // Start is called before the first frame update
    void Start()
    {
        m_fBulletLifeTimeCounter = 0;
        m_totalDistanceTravelled = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_totalDistanceTravelled >= m_range/100.0f)
        {
            BulletManager.Instance().ReturnBullet(this.gameObject, m_bulletType);
        }
        transform.position += (m_fDirection * m_fBulletSpeed) * Time.deltaTime;
        m_totalDistanceTravelled += (m_fDirection.magnitude * m_fBulletSpeed) * Time.deltaTime;
    }
}
