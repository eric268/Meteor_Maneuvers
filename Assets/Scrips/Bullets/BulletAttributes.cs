//--------------------------------------------------------------------------------
//------------------------------BulletAttributes.cs--------------------------------
//------------------------------Eric Galway---------------------------------------
//------------------------------101252535-----------------------------------------
//------------------------------Last Modified: 21/10/2021-------------------------
//------------------------------Description---------------------------------------
//             This script controls the attributes that all bullets contain.  
//------------------------------Revision History----------------------------------
//------------------------------Version 1.2 - Added total bullet distance---------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class that encapsulates bullet attributes
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
        //The range is set based upon which tower fires the bullet
        if (m_totalDistanceTravelled >= m_range/100.0f)
        {
            //If the bullet is outside of the towers range return the bullet appropriate Queue.
            BulletManager.Instance().ReturnBullet(this.gameObject, m_bulletType);
        }
        //Moves bullet
        transform.position += (m_fDirection * m_fBulletSpeed) * Time.deltaTime;
        m_totalDistanceTravelled += (m_fDirection.magnitude * m_fBulletSpeed) * Time.deltaTime;
    }
}
