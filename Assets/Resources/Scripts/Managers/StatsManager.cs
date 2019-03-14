using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public static StatsManager instance;

    // Wave
    [HideInInspector]
    public int wave;

    // Health
    [HideInInspector]
    public int health;
    [HideInInspector]
    public int healthGeneration, healthGenerationDelay;

    // Laser
    [HideInInspector]
    public float laserLength;

    // Score
    [HideInInspector]
    public int score;
    [HideInInspector]
    public int highscore;

    // Offense
    [HideInInspector]
    public int minDamage, maxDamage;
    [HideInInspector]
    public float bulletAliveTime;
    [HideInInspector]
    public float bulletSpeed;
    [HideInInspector]
    public float shootDelay;

    // Camera
    [HideInInspector]
    public float cameraZoom;

    // AI
    [HideInInspector]
    public int aiHealth;
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
        wave = PlayerPrefs.GetInt("Wave", 1);

        health = PlayerPrefs.GetInt("Health", 100);
        healthGeneration = PlayerPrefs.GetInt("HealthGeneration", 1);
        healthGenerationDelay = PlayerPrefs.GetInt("HealthGenerationDelay", 5);

        laserLength = PlayerPrefs.GetFloat("LaserLength", 3);

        score = PlayerPrefs.GetInt("Score", 0);
        highscore = PlayerPrefs.GetInt("Highscore", 0);

        minDamage = PlayerPrefs.GetInt("MinDamage", 5);
        maxDamage = PlayerPrefs.GetInt("MaxDamage", 10);
        bulletAliveTime = PlayerPrefs.GetFloat("BulletAliveTime", 1);
        bulletSpeed = PlayerPrefs.GetFloat("BulletSpeed", 200);
        shootDelay = PlayerPrefs.GetFloat("ShootDelay", 0.25f);

        cameraZoom = PlayerPrefs.GetFloat("CameraZoom", 4);
}

    void GetAIStats()
    {
        aiHealth = PlayerPrefs.GetInt("AIHealth", 100);
        aiMinDamage = PlayerPrefs.GetInt("AIMinDamage", 1);
        aiMaxDamage = PlayerPrefs.GetInt("AIMaxDamage", 3);
        aiMinVelocity = PlayerPrefs.GetFloat("AIMinVelocity", 1);
        aiMaxVelocity = PlayerPrefs.GetFloat("AIMaxVelocity", 2);
        aiFlockSize = PlayerPrefs.GetInt("AIFlockSize", 10);
        aiGroupSize = PlayerPrefs.GetInt("AIGroupSize", 3);
    }

    /// <summary>
    /// Saves score
    /// </summary>
    public void SaveScore()
    {
        int score = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().score;
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.SetInt("TotalScore", PlayerPrefs.GetInt("TotalScore") + score);
    }

    // Reset everything except highscore
    void OnApplicationQuit()
    {
        int totalScore = PlayerPrefs.GetInt("TotalScore");

        // Delete all the keys!
        PlayerPrefs.DeleteAll();

        // If totalscore is higher than highscore
        PlayerPrefs.SetInt("Highscore", (totalScore > highscore) ? totalScore : highscore);
    }
}
