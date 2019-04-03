using UnityEngine;
using System.Collections;

public class BulletManager : MonoBehaviour
{
    public static BulletManager instance;

    public Bullet bulletPrefab, specialBulletPrefab;


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

    public Bullet SpawnSpecialBullet(Vector3 startPosition, float eulerY)
    {
        Bullet bullet = Instantiate(specialBulletPrefab);

        bullet.InitiateBullet(startPosition, eulerY, true);

        return bullet;
    }

    public void DestroyBullet(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }
}
