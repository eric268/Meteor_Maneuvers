using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldNuggetManager : MonoBehaviour
{
    [SerializeField]
    public float m_fLifeSpan;
    [SerializeField]
    public float m_vValueUponCollection;

    private float m_fLifeSpanCounter;

    private void Start()
    {
        m_fLifeSpanCounter = 0.0f;
    }

    private void Update()
    {
        CheckBounds();

        m_fLifeSpanCounter += Time.deltaTime;
        if (m_fLifeSpanCounter >= m_fLifeSpan)
        {
            Destroy(gameObject);
        }
        else if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchedPosition = Camera.main.ScreenToWorldPoint(touch.position);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (GetComponent<Collider2D>().OverlapPoint(touchedPosition))
                    {
                        Level1UI.m_fTotalScore += m_vValueUponCollection;
                        Level1UI.m_fTotalCash += m_vValueUponCollection;
                        Destroy(gameObject);
                    }
                    break;
                case TouchPhase.Moved:
                    if (GetComponent<Collider2D>().OverlapPoint(touchedPosition))
                    {
                        Level1UI.m_fTotalScore += m_vValueUponCollection;
                        Level1UI.m_fTotalCash += m_vValueUponCollection;
                        Destroy(gameObject);
                    }
                    break;
            }
        }
    }

    void CheckBounds()
    {
        if (transform.position.x > TowerAttributes.xBounds)
        {
            transform.position = new Vector3(TowerAttributes.xBounds, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -TowerAttributes.xBounds)
        {
            transform.position = new Vector3(-TowerAttributes.xBounds, transform.position.y, transform.position.z);
        }
        else if (transform.position.y > TowerAttributes.yBoundsMax)
        {
            transform.position = new Vector3(transform.position.x, TowerAttributes.yBoundsMax, transform.position.z);
        }
        if (transform.position.y < TowerAttributes.yBoundsMin)
        {
            transform.position = new Vector3(transform.position.x, TowerAttributes.yBoundsMin, transform.position.z);
        }
    }
}
