using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public static StatsManager instance;

    // Wave
    [HideInInspector]
    public int wave;

    // Player
    [HideInInspector]
    public int health;
    [HideInInspector]
    public int healthGeneration, healthGenerationDelay;
    [HideInInspector]
    public int speed;

    // Laser
    [HideInInspector]
    public int laserLength;

    // Score
    [HideInInspector]
    public int score;
    [HideInInspector]
    public int highscore;

    // Offense
    [HideInInspector]
    public float damageLevel;
    [HideInInspector]
    public int minDamage, maxDamage;
    [HideInInspector]
    public int bulletSpeed;
    [HideInInspector]
    public float shootDelay;

    // Camera
    [HideInInspector]
    public int cameraZoomLevel;

    // Special attack
    [HideInInspector]
    public int specialAttackCharges;

    // Difficulty
    [HideInInspector]
    public float difficultyMultiplier;

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
        wave = 5;
        score = 1000000;
    }

    void GetStats()
    {
        wave = PlayerPrefs.GetInt("Wave", 1);

        health = PlayerPrefs.GetInt("Health", 100);
        healthGeneration = PlayerPrefs.GetInt("HealthGeneration", 1);
        healthGenerationDelay = PlayerPrefs.GetInt("HealthGenerationDelay", 5);
        speed = PlayerPrefs.GetInt("Speed", 1);

        laserLength = PlayerPrefs.GetInt("LaserLength", 3);

        score = PlayerPrefs.GetInt("Score", 10000);
        highscore = PlayerPrefs.GetInt("Highscore", 0);

        damageLevel = PlayerPrefs.GetFloat("DamageLevel", 1);
        minDamage = PlayerPrefs.GetInt("MinDamage", 15);
        maxDamage = PlayerPrefs.GetInt("MaxDamage", 20);
        bulletSpeed = PlayerPrefs.GetInt("BulletSpeed", 100);
        shootDelay = PlayerPrefs.GetFloat("ShootDelay", 0.5f);

        cameraZoomLevel = PlayerPrefs.GetInt("CameraZoom", 4);

        specialAttackCharges = PlayerPrefs.GetInt("SpecialAttackCharges", 0);

        difficultyMultiplier = PlayerPrefs.GetFloat("DifficultyMultiplier", 1);
    }

    void GetAIStats()
    {
        aiHealth = PlayerPrefs.GetInt("AIHealth", 100);
        aiMinDamage = PlayerPrefs.GetInt("AIMinDamage", 1);
        aiMaxDamage = PlayerPrefs.GetInt("AIMaxDamage", 3);
        aiMinVelocity = PlayerPrefs.GetFloat("AIMinVelocity", 1);
        aiMaxVelocity = PlayerPrefs.GetFloat("AIMaxVelocity", 2);
        aiFlockSize = PlayerPrefs.GetInt("AIFlockSize", 10);
        aiGroupSize = PlayerPrefs.GetInt("AIGroupSize", 2);
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

    // Reset everything except highscore and volume settings
    void OnApplicationQuit()
    {
        int totalScore = PlayerPrefs.GetInt("TotalScore");
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 100);   // If it hasnt been set I do not want it to save 0
        float sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 100);       // *

        // Delete all the keys!
        PlayerPrefs.DeleteAll();

        // If totalscore is higher than highscore
        PlayerPrefs.SetInt("Highscore", (totalScore > highscore) ? totalScore : highscore);

        // Re-set volume (lol)
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
    }
}
