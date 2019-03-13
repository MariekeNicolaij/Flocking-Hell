using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public static StatsManager instance;

    [HideInInspector]
    public int health;
    [HideInInspector]
    public int healthGeneration, healthGenerationDelay;

    [HideInInspector]
    public float laserLength;

    [HideInInspector]
    public int score;

    [HideInInspector]
    public int minDamage, maxDamage;
    [HideInInspector]
    public float bulletAliveTime;
    [HideInInspector]
    public float bulletSpeed;
    [HideInInspector]
    public float shootDelay;

    [HideInInspector]
    public float cameraZoom;
    [HideInInspector]
    public int knockbackForce;

    // AI
    [HideInInspector]
    public float aiHealth;
    [HideInInspector]
    public int aiMinDamage, aiMaxDamage;
    [HideInInspector]
    public float aiMinVelocity, aiMaxVelocity;
    [HideInInspector]
    public int aiFlockSize;
    [HideInInspector]
    public int aiGroupSize;


    void Awake()
    {
        instance = this;

        GetStats();
        GetAIStats();
    }

    void GetStats()
    {
        health = PlayerPrefs.GetInt("Health", 100);
        healthGeneration = PlayerPrefs.GetInt("HealthGeneration", 1);
        healthGenerationDelay = PlayerPrefs.GetInt("HealthGenerationDelay", 5);

        laserLength = PlayerPrefs.GetFloat("LaserLength", 3);

        score = PlayerPrefs.GetInt("Score", 0);

        minDamage = PlayerPrefs.GetInt("MinDamage", 5);
        maxDamage = PlayerPrefs.GetInt("MaxDamage", 10);
        bulletAliveTime = PlayerPrefs.GetFloat("BulletAliveTime", 1);
        bulletSpeed = PlayerPrefs.GetFloat("BulletSpeed", 200);
        shootDelay = PlayerPrefs.GetFloat("ShootDelay", 0.25f);

        cameraZoom = PlayerPrefs.GetFloat("CameraZoom", 4);
        knockbackForce = PlayerPrefs.GetInt("Knockback", 1);
}

    void GetAIStats()
    {
        aiHealth = PlayerPrefs.GetFloat("AIHealth", 100);
        aiMinDamage = PlayerPrefs.GetInt("AIMinDamage", 1);
        aiMaxDamage = PlayerPrefs.GetInt("AIMaxDamage", 3);
        aiMinVelocity = PlayerPrefs.GetFloat("AIMinVelocity", 1);
        aiMaxVelocity = PlayerPrefs.GetFloat("AIMaxVelocity", 2);
        aiFlockSize = PlayerPrefs.GetInt("AIFlockSize", 10);
        aiGroupSize = PlayerPrefs.GetInt("AIGroupSize", 3);
    }
}
