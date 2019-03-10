using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public static StatsManager instance;

    public float health;
    public float healthGeneration;

    public float laserLength;
    public float laserSightLength;

    public float damage;
    public float bulletAliveTime;
    public float bulletSpeed;
    public float shootDelay;

    public float cameraZoom;
    public float knockback;

    // AI
    public float aiHealth;
    public float aiDamage;
    public float aiMinVelocity;
    public float aiMaxVelocity;
    public float aiFlockSize;


    void Awake()
    {
        instance = this;

        GetStats();
        GetAIStats();
    }

    void GetStats()
    {
        health = PlayerPrefs.GetFloat("Health", 100);
        healthGeneration = PlayerPrefs.GetFloat("healthGeneration", 0.25f);

    public float laserLength;
    public float laserSightLength;

    public float damage;
    public float bulletAliveTime;
    public float bulletSpeed;
    public float shootDelay;

    public float cameraZoom;
    public float knockback;
}

    void GetAIStats()
    {
       public float aiHealth;
public float aiDamage;
public float aiMinVelocity;
public float aiMaxVelocity;
public float aiFlockSize;
    }

    void Update()
    {
        
    }
}
