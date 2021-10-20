using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField]
    private float m_fMovementSpeed = 0.0f;

    [SerializeField]
    private Rigidbody2D m_rigidbody;

    private Vector2 m_vDirection = new Vector2(-1,0);

    [SerializeField]
    private bool m_bIsActive = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (m_bIsActive)
        {
            m_rigidbody.velocity = m_vDirection * m_fMovementSpeed;
        }
    }
}
