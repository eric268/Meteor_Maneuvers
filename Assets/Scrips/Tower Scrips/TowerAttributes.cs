using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttributes : MonoBehaviour
{
    public Vector2 m_vDirection;

    [SerializeField]
    public float m_bulletRange;

    [SerializeField]
    public Transform m_bulletSpawn;

    [SerializeField]
    public BulletType m_bulletType;

    [SerializeField]
    public float m_fFireRate;

    public float m_fFiringCounter;

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
       
    }
    
}