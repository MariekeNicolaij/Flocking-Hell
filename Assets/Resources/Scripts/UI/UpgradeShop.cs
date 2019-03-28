using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UpgradeShop : MonoBehaviour
{
    // --------UI--------
    public Text scoreText;

    // ------Stats-------
    // Score
    public int score;

    // Player
    public int health;
    public int healthGeneration, healthGenerationDelay;
    public int speed;

    // Laser
    public float laserLength;

    // Offense
    public int minDamage, maxDamage;
    public float bulletAliveTime;
    public float bulletSpeed;
    public float shootDelay;

    // Camera
    public float cameraZoom;


    void Start()
    {
        GetStats();

        SetScoreText();
    }

    void SetScoreText()
    {
        scoreText.text = "Score: " + score;
    }

    void GetStats()
    {
        score = StatsManager.instance.score;
    }

    void Update()
    {

    }

    public void NextWave()
    {
        PlayerPrefs.SetString("Scene", "Game");
        SceneManager.LoadScene("Loading");
    }
}
