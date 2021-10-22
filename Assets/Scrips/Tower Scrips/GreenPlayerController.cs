using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenPlayerController : MonoBehaviour
{
    public bool m_bSelected;
    private float m_bSpeed;
    private float m_fRotationSpeed;
    public Vector3 m_vTargetPosition;
    GameObject m_parent;
    // Start is called before the first frame update
    void Start()
    {
        m_bSelected = false;
        m_fRotationSpeed = 0.7f;
        m_bSpeed = 3.0f;
        transform.localPosition = new Vector3(0, 0, 1);
        GetComponent<SpriteRenderer>().enabled = false;
        m_vTargetPosition = transform.parent.gameObject.transform.position;
        m_parent = transform.parent.gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchedPosition = Camera.main.ScreenToWorldPoint(touch.position);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (transform.parent.gameObject.GetComponent<Collider2D>().OverlapPoint(touchedPosition))
                    {
                        if (m_bSelected)
                        {
                            m_bSelected = false;
                            GetComponent<SpriteRenderer>().enabled = false;
                        }
                        else
                        {
                            GetComponent<SpriteRenderer>().enabled = true;
                            m_bSelected = true;
                        }
                    }
                    else
                    {
                        if (m_bSelected)
                        {
                            m_vTargetPosition = touchedPosition;
                            GetComponent<SpriteRenderer>().enabled = false;
                            m_bSelected = false;
                        }
                    }
                    break;
            }
                
        }

        if (Vector3.Distance(m_vTargetPosition, m_parent.transform.position) > 0.1f)
        {
            RotateShip(m_vTargetPosition);
            MoveShip(m_vTargetPosition);
        }
        CheckCollisions();


    }

    void RotateShip(Vector3 touchedPos)
    {
            //m_parent.GetComponent<TowerAttributes>().m_vDirection = MathHelper.CalculateDirection(m_parent.transform.eulerAngles.z);
            Vector2 m_vDesiredDirection = new Vector2(touchedPos.x - m_parent.transform.position.x, touchedPos.y - m_parent.transform.position.y);
            m_vDesiredDirection = m_vDesiredDirection.normalized;
            Debug.Log(m_parent.name);
            float desiredRotation = MathHelper.CalculateAngle(m_parent.GetComponent<TowerAttributes>().m_vDirection, m_vDesiredDirection) + m_parent.transform.eulerAngles.z;
            m_parent.transform.rotation = Quaternion.Slerp(m_parent.transform.rotation, Quaternion.Euler(0, 0, desiredRotation), m_fRotationSpeed);
    }

    void MoveShip(Vector3 touchedPos)
    {
        m_parent.transform.position = new Vector2(Mathf.Lerp(m_parent.transform.position.x, touchedPos.x, m_bSpeed * Time.deltaTime),
            Mathf.Lerp(m_parent.transform.position.y, touchedPos.y, m_bSpeed * Time.deltaTime));
    }

    void CheckCollisions()
    {
        List<Collider2D> collList = new List<Collider2D>();
        m_parent.transform.gameObject.GetComponent<Collider2D>().OverlapCollider(new ContactFilter2D(), collList);

        foreach(Collider2D coll in collList)
        {
            if (coll.gameObject.GetComponent<EnemyAttributes>())
            {
                PlayerTouchHandler.m_bGreenPlayerPlaced = false;
                if (coll.gameObject.GetComponent<EnemyAttributes>().m_enemyType != EnemyType.PURPLE_ENEMY)
                {
                    Destroy(transform.parent.gameObject);
                    Level1UI.totalCash += coll.gameObject.GetComponent<EnemyAttributes>().m_fCashWhenDestroyed;
                }
            }
        }
    }
}
