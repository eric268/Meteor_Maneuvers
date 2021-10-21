using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFactory : MonoBehaviour
{
    private static BulletFactory m_instance = null;

    public GameObject m_greenBullet;
    public GameObject m_purpleBullet;
    public GameObject m_orangeBullet;
    public GameObject m_blueBullet;

    private GameObject m_bulletController;

    private BulletFactory()
    {
        Initialize();
    }

    private void Initialize()
    {
        m_greenBullet = Resources.Load("Prefabs/Bullets/GreenBullet") as GameObject; 
        m_purpleBullet = Resources.Load("Prefabs/Bullets/PurpleBullet") as GameObject;
        m_orangeBullet = Resources.Load("Prefabs/Bullets/OrangeBullet") as GameObject;
        m_blueBullet = Resources.Load("Prefabs/Bullets/BlueBullet") as GameObject;
        m_bulletController = GameObject.Find("BulletController");
    }


    public static BulletFactory Instance()
    {
        if (m_instance == null)
        {
            m_instance = new BulletFactory();
        }

        return m_instance;
    }

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
