using UnityEngine;
using System.Collections;

public class BulletManager : MonoBehaviour
{
    public static BulletManager instance;

    public Bullet bulletPrefab;

    void Start()
    {
        instance = this;
    }

    public Bullet SpawnBullet(Vector3 startPosition, float eulerY)
    {
        Bullet bullet = Instantiate(bulletPrefab);

        bullet.InitiateBullet(startPosition, eulerY);

        return bullet;
    }

    public void DestroyBullet(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }
}
