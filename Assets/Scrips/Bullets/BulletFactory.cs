//--------------------------------------------------------------------------------
//------------------------------BulletFactory.cs----------------------------------
//------------------------------Eric Galway---------------------------------------
//------------------------------101252535-----------------------------------------
//------------------------------Last Modified: 21/10/2021-------------------------
//------------------------------Description---------------------------------------
//             This script controls the creation of bullets from the
//             appropriate prefab object.
//------------------------------Revision History----------------------------------
//------------------------------Version 1.0 - Add bullet create bullet functions--
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class which contains functions and singleton for creating bullets
[System.Serializable]
public class BulletFactory
{
    private static BulletFactory m_instance = null;

    public GameObject m_greenBullet;
    public GameObject m_purpleBullet;
    public GameObject m_orangeBullet;
    public GameObject m_blueBullet;

    private GameObject m_bulletController;

    //Constructor
    private BulletFactory()
    {
        Initialize();
    }
    //Gets bullet prefabs from resource folder
    private void Initialize()
    {
        m_greenBullet = Resources.Load("Prefabs/Bullets/GreenBullet") as GameObject; 
        m_purpleBullet = Resources.Load("Prefabs/Bullets/PurpleBullet") as GameObject;
        m_orangeBullet = Resources.Load("Prefabs/Bullets/OrangeBullet") as GameObject;
        m_blueBullet = Resources.Load("Prefabs/Bullets/BlueBullet") as GameObject;
        m_bulletController = GameObject.Find("BulletController");
    }

    //Creates or returns singleton
    public static BulletFactory Instance()
    {
        if (m_instance == null)
        {
            m_instance = new BulletFactory();
        }

        return m_instance;
    }
    //Creates a bullet from given type
    public GameObject createBullet(BulletType type)
    {
        GameObject temp_bullet = null;
        switch(type)
        {
            case BulletType.GREEN_BULLET:
                temp_bullet = MonoBehaviour.Instantiate(m_greenBullet);
                break;
            case BulletType.PURPLE_BULLET:
                temp_bullet = MonoBehaviour.Instantiate(m_purpleBullet);
                break;
            case BulletType.ORANGE_BULLET:
                temp_bullet = MonoBehaviour.Instantiate(m_orangeBullet);
                break;
            case BulletType.BLUE_BULLET:
                temp_bullet = MonoBehaviour.Instantiate(m_blueBullet);
                break;
        }
        temp_bullet.transform.parent = m_bulletController.gameObject.transform;
        temp_bullet.SetActive(false);

        return temp_bullet;
    }
}
