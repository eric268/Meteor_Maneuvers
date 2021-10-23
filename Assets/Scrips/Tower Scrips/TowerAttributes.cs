//--------------------------------------------------------------------------------
//------------------------------TowerAttributes.cs--------------------------------
//------------------------------Eric Galway---------------------------------------
//------------------------------101252535-----------------------------------------
//------------------------------Last Modified: 21/10/2021-------------------------
//------------------------------Description---------------------------------------
//             This script manages the attributes bounds checking, and 
//             and direction updating of all enemy ship
//------------------------------Revision History----------------------------------
//------------------------------Version 1.4 - Updated scene bounds

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class that encapsulates all tower attribute variables
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
    public TowerType m_towerType;

    [SerializeField]
    public float m_fFireRate;

    public float m_fFiringCounter;

    [SerializeField]
    public float m_fTowerCost;
    
    public bool m_bIsActive;

    [SerializeField]
    public static float xBounds;
    [SerializeField]
    public static float yBoundsMax;

    public static float yBoundsMin;

    // Start is called before the first frame update
    void Start()
    {
        xBounds = 8.75f;
        yBoundsMax = 4.5f;
        yBoundsMin = -2.75f;
        m_bIsActive = false;
        m_vDirection = new Vector2(1.0f, 0.0f);
        m_fFiringCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Updates direction based upon tower rotation
        m_vDirection = MathHelper.CalculateDirection(transform.eulerAngles.z);
        CheckBounds();
    }
    //Ensures that towers are placed correctly in the screen and can not leave the scene
    void CheckBounds()
    {
        if (transform.position.x > xBounds)
        {
            transform.position = new Vector3(xBounds, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -xBounds)
        {
            transform.position = new Vector3(-xBounds, transform.position.y, transform.position.z);
        }
        else if (transform.position.y > yBoundsMax)
        {
            transform.position = new Vector3(transform.position.x, yBoundsMax, transform.position.z);
        }
        if (transform.position.y < yBoundsMin)
        {
            transform.position = new Vector3(transform.position.x, yBoundsMin, transform.position.z);
        }
    }
}