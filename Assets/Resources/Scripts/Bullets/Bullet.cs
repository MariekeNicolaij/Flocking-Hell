using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody rBody;

    float bulletAliveTime = 5;

    //bool rainbow;
    //Color[] randomColor = { Color.blue, Color.cyan, Color.green, Color.magenta, Color.red, Color.white, Color.yellow };

    public void SetBullet()
    {
        rBody = GetComponent<Rigidbody>();

        Invoke("DestroyBullet", bulletAliveTime);
        //rainbow = System.Convert.ToBoolean(PlayerPrefs.GetInt("RainbowBulletsEnabled"));
        //if (rainbow)
        //    GetComponent<Renderer>().material.color = randomColor[Random.Range(0, randomColor.Length)];


    }

    public void Shoot()
    {

    }

    void Start()
    {
        //LineRenderer l = gameObject.AddComponent<LineRenderer>();
        //Material newMat = Resources.Load("Materials/Gold", typeof(Material)) as Material;
        //l.material = newMat;

        //l.startWidth = 0.01f;
        //l.endWidth = 0.01f;

        //Color t = l.endColor;
        //t.a = 0;
        //l.endColor = t;

        //l.SetPositions(new Vector3[2] { transform.position, transform.up * 10 });

    }

    void OnTriggerEnter(Collider other)
    {
        DestroyBullet();
    }

    void DestroyBullet()
    {
        BulletManager.instance.DestroyBullet(this);
    }
}