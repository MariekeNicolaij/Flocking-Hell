using UnityEngine;
using System.Collections;

public class BulletManager : MonoBehaviour
{
    public static BulletManager instance;

    public Bullet bulletPrefab;

    void Awake()
    {
        instance = this;
    }

    public Bullet SpawnBullet()
    {
        Bullet bullet = Instantiate(bulletPrefab);

        bullet.SetBullet();

        return bullet;
    }

    public void DestroyBullet(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }
}
