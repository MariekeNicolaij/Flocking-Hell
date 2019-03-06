using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody rBody;

    ParticleSystem particles;

    LineRenderer line;
    Color lineColor = new Color(0, 100, 255);
    Vector3 startPosition;
    float lineLength = 1.5f;

    float minDamage, maxDamage, actualDamage;
    float bulletAliveTime = 1;


    public void InitiateBullet()
    {
        rBody = GetComponent<Rigidbody>();

        Invoke("DestroyBullet", bulletAliveTime);
    }

    void Start()
    {
        particles = GetComponentInChildren<ParticleSystem>();
        line = gameObject.AddComponent<LineRenderer>();
        line.material.color = lineColor;
        line.widthMultiplier = 0.0015f;
        startPosition = transform.position;
    }

    void Update()
    {
        // Want to keep the line right behind the bullet
        line.SetPositions(new Vector3[2] { startPosition, transform.position });
    }

    void OnCollisionEnter(Collision other)
    {
            Debug.Log(other.collider.name);
        if (other.collider.tag == "AI")
        {
            Debug.Log("AI");
        }

        PlayDestroyAnimation();
    }

    void PlayDestroyAnimation()
    {
        line.enabled = false;
        rBody.velocity = Vector3.zero;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;

        particles.Play();

        Invoke("DestroyBullet", particles.main.duration);
    }

    void DestroyBullet()
    {
        BulletManager.instance.DestroyBullet(this);
    }
}