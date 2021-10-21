using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BulletManager
{
    private static BulletManager instance = null;

    private BulletManager()
    {
        Initalize();
    }

    public static BulletManager Instance()
    {
        if (instance == null)
        {
            instance = new BulletManager();
        }

        return instance;
    }

    private void AddBullet(BulletType type)
    {
        var temp_bullet = BulletFactory.Instance().createBullet(type);
        bulletPools[(int)type].Enqueue(temp_bullet);
    }

    public List<Queue<GameObject>> bulletPools;

    private void Initalize()
    {
        bulletPools = new List<Queue<GameObject>>();

        for (int i = 0; i < (int)BulletType.NUMBER_OF_BULLET_TYPES; i++)
        {
            bulletPools.Add(new Queue<GameObject>());
        }
    }
    
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

    public void ReturnBullet(GameObject returnedBullet, BulletType type)
    {
        returnedBullet.SetActive(false);
        bulletPools[(int)type].Enqueue(returnedBullet);
    }
}
