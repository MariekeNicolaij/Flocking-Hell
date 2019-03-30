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

    float minDamage, maxDamage;
    float bulletAliveTime;

    public void InitiateBullet(Vector3 startPosition, float eulerY)
    {
        this.startPosition = startPosition;
        transform.position = startPosition;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, eulerY, transform.eulerAngles.z);

        rBody = GetComponent<Rigidbody>();

        GetStats();

        particles = GetComponentInChildren<ParticleSystem>();

        line = gameObject.AddComponent<LineRenderer>();
        line.material.color = lineColor;
        line.widthMultiplier = 0.005f;

        Invoke("DestroyBullet", bulletAliveTime);
    }


    void GetStats()
    {
        bulletAliveTime = StatsManager.instance.bulletAliveTime;
        minDamage = StatsManager.instance.minDamage;
        maxDamage = StatsManager.instance.maxDamage;
    }

    void Update()
    {
        AnimateLine();
    }

    void AnimateLine()
    {
        // Want to keep the line right behind the bullet
        line.SetPositions(new Vector3[2] { startPosition, transform.position });
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == Layer.AI)
        {
            AI ai = other.gameObject.GetComponent<AI>();

            if (other.collider.tag == "AI Flocking")
            {
                DamageAI(ai);
            }
            if (other.collider.tag == "AI Normal")
            {
                DamageAI(ai, false);    // AI is not part of a flocking group
            }
        }

        // Rumble babyy
        Camera.main.gameObject.GetComponent<Follow>().rumbleTime += 0.1f;
        PlayDestroyAnimation();
    }

    /// <summary>
    /// Does random calculated damage based on min and max value
    /// </summary>
    /// <returns></returns>
    public void DamageAI(AI ai, bool isFlocking = true)
    {
        // Apply score
        AIManager.instance.player.ApplyScore((isFlocking) ? 1 : 2);     // 2 = special multiplier because the 'normal AI's' are harder to beat, otherwise just regular score (1x)

        // Calculate damage
        int damage = Mathf.RoundToInt(Random.Range(minDamage, maxDamage) * StatsManager.instance.damageLevel);

        // Apply damage to AI
        ai.ReceiveDamage(damage);
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