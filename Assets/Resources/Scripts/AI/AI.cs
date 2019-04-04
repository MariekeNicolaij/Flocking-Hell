using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AI : MonoBehaviour
{
    StateManager stateManager;

    [HideInInspector]
    public Rigidbody rBody;

    public bool isFlock, isBoss;
    public int groupIndex;          // Used if AI is flocking
    [HideInInspector]
    public float minVelocity;      // Min speed
    [HideInInspector]
    public float maxVelocity;      // Max speed

    // Health
    public GameObject healthBar;
    public TextMesh healthText;
    Material healthBarMaterial;
    public int health, maxHealth;
    public float minDamage, maxDamage;

    [HideInInspector]
    public bool isBeingDestroyed;   // So it does not get 'destroyed twice' (because sometimes when you shoot with 2 bullets this would happen)


    public void SetAI(bool isFlock = false, bool isBoss = false)
    {
        rBody = GetComponent<Rigidbody>();
        this.isFlock = isFlock;
        this.isBoss = isBoss;

        GetStats();

        healthBarMaterial = healthBar.GetComponent<Renderer>().material;

        stateManager = new StateManager(this, (isFlock) ? (State)new FlockState() : (State)new NormalState());
    }

    void GetStats()
    {
        health = Mathf.RoundToInt(StatsManager.instance.aiHealth * StatsManager.instance.difficultyMultiplier);
        minDamage = StatsManager.instance.aiMinDamage * StatsManager.instance.difficultyMultiplier;
        maxDamage = StatsManager.instance.aiMaxDamage * StatsManager.instance.difficultyMultiplier;
        minVelocity = AIManager.instance.minVelocity;
        maxVelocity = AIManager.instance.maxVelocity * StatsManager.instance.difficultyMultiplier;

        if (isBoss) // Boss stats
        {
            health *= 10;
            minDamage *= 10;
            maxDamage *= 10;
            maxVelocity = AIManager.instance.maxVelocity;
        }

        maxHealth = health;
        healthText.text = health.ToString();
    }

    void Update()
    {
        stateManager.Update();
        transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
    }

    /// <summary>
    /// Does random calculated damage based on min and max value
    /// </summary>
    /// <returns></returns>
    public int Damage()
    {
        return Mathf.CeilToInt(Random.Range(minDamage, maxDamage));
    }

    public void ReceiveDamage(int damage)
    {
        health -= damage;

        if (health < 0)
        {
            health = 0;
            PlayDeathAnimation();
        }

        // Health bar
        Color blue = Color.blue;
        Color red = Color.red;
        blue.a = 0.5f;
        red.a = 0.5f;

        if (healthBarMaterial)
            healthBarMaterial.color = Color.Lerp(red, blue, (float)health / (float)maxHealth); // Casten naar float omdat het een normalized value nodig heeft
        // Health bar text
        healthText.text = health.ToString();
    }

    void PlayDeathAnimation()
    {
        // Play();
        GetComponent<Collider>().enabled = false;
        ParticleSystem ps = GetComponentInChildren<ParticleSystem>();
        ps.Play();
        Invoke("DestroyAI", ps.main.duration);
    }

    void DestroyAI()
    {
        AIManager.instance.DestroyAI(gameObject, isFlock);
    }
}
