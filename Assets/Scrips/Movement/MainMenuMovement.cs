using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D m_rigidBody;

    private Vector2 m_vStartingPosition;

    void Start()
    {
        m_vStartingPosition = new Vector2(transform.position.x, transform.position.y);
    }

    void Update()
    {
        if (transform.position.x <= -22)
        {
            m_rigidBody.MovePosition(m_vStartingPosition);
        }
    }
}
