using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttributes : MonoBehaviour
{
    private Vector2 m_vDirection;

    [SerializeField]
    public float m_fRange;

    [SerializeField]
    public float m_fHealth;

    [SerializeField]
    public float m_fTowerCost;
    
    public bool m_bIsActive;



    // Start is called before the first frame update
    void Start()
    {
        m_bIsActive = false;
        m_vDirection = new Vector2(1.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_bIsActive)
        {
            //Do Stuff
        }
    }
}
