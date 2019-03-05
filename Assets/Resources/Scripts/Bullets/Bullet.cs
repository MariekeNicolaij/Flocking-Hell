using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody rBody;

    LineRenderer line;
    float lineLength = 1.5f;
    Color lineColor = new Color(255, 255, 0);

    float bulletAliveTime = 1;

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

    void Start()
    {
        line = gameObject.AddComponent<LineRenderer>();
        line.material.color = lineColor;
        line.widthMultiplier = 0.0015f;
    }

    void Update()
    {
        // Want to keep the line right behind the bullet
        line.SetPositions(new Vector3[2] { transform.position, transform.position + (transform.up * lineLength) });
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