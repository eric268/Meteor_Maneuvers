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
    private float m_fDamageOnHit;

    [SerializeField]
    public Vector3 m_vDirection = new Vector3(-1,0,0);

    public Quaternion m_fWantedRotation;

    public float m_fAngle;
    [SerializeField]
    public float m_fHealth;

    [SerializeField]
    private bool m_bIsActive = false;

   



    // Start is called before the first frame update
    void Start()
    {
        m_fAngle = 180.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_bIsActive)
        {
            transform.position += (m_vDirection * m_fMovementSpeed)*Time.deltaTime;

            //if (m_fAngle != transform.eulerAngles.z)
            {
                m_fWantedRotation = Quaternion.Euler(new Vector3(0f, 0f, m_fAngle));
                transform.rotation = Quaternion.Slerp(transform.rotation, m_fWantedRotation, m_fRotationSpeed);
            } 
        }
    }

    public float CalculateRotationBasedOnDirection()
    {
        float x = m_vDirection.x;
        float y = m_vDirection.y;
        return Mathf.Atan((y / x));
    }
}
