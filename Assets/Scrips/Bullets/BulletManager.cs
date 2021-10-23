//--------------------------------------------------------------------------------
//------------------------------BulletManager.cs--------------------------------
//------------------------------Eric Galway---------------------------------------
//------------------------------101252535-----------------------------------------
//------------------------------Last Modified: 21/10/2021-------------------------
//------------------------------Description---------------------------------------
//             This script controls all bullet pools. It identifies if
//             a bullet pool is empty and calls the MakeBullet function
//             if necessary. Also contains function to activate/deactivate
//             bullets.
//------------------------------Revision History----------------------------------
//------------------------------Version 1.1 Updated FireBullet Function-----------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class that manages access to bullet pools
[System.Serializable]
public class BulletManager
{
    //Singleton
    private static BulletManager instance = null;

    private BulletManager()
    {
        Initalize();
    }
    //Access to singleton
    public static BulletManager Instance()
    {
        if (instance == null)
        {
            instance = new BulletManager();
        }

        return instance;
    }
    //Creates a bullet
    private void AddBullet(BulletType type)
    {
        var temp_bullet = BulletFactory.Instance().createBullet(type);
        bulletPools[(int)type].Enqueue(temp_bullet);
    }

    public List<Queue<GameObject>> bulletPools;

    //Initializes queues which contain different bullets.
    private void Initalize()
    {
        bulletPools = new List<Queue<GameObject>>();

        for (int i = 0; i < (int)BulletType.NUMBER_OF_BULLET_TYPES; i++)
        {
            bulletPools.Add(new Queue<GameObject>());
        }
    }
    //Activates a bullet and places it into scene with appropriate variable values
    public GameObject FireBullet(Vector2 spawnPosition, Vector2 direction, BulletType type, float bulletRange)
    {
        GameObject temp_bullet = null;

        if (bulletPools[(int)type].Count < 1)
        {
            AddBullet(type);
        }
        temp_bullet = bulletPools[(int)type].Dequeue();
        temp_bullet.transform.position = spawnPosition;
        temp_bullet.GetComponent<BulletAttributes>().m_fDirection = new Vector3(direction.x, direction.y, 1.0f);
        temp_bullet.GetComponent<BulletAttributes>().m_range = bulletRange;
        temp_bullet.GetComponent<BulletAttributes>().m_totalDistanceTravelled = 0.0f;
        temp_bullet.SetActive(true);
        return temp_bullet;
    }
    //Deactivates bullet and returns it with appropriate values
    public void ReturnBullet(GameObject returnedBullet, BulletType type)
    {
        returnedBullet.SetActive(false);
        bulletPools[(int)type].Enqueue(returnedBullet);
    }
}
