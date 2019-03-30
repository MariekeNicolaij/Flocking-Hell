using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UpgradeShop : MonoBehaviour
{
    // --------UI--------
    // Score
    public Text scoreText;

    // Player
    public UpgradePanel healthPanel;
    public UpgradePanel healthGenerationPanel, healthGenerationDelayPanel;
    public UpgradePanel speedPanel;

    // Laser
    public UpgradePanel laserLengthPanel;

    // Offense
    public UpgradePanel damageLevelPanel;
    public UpgradePanel bulletAlivePanel;
    public UpgradePanel bulletSpeedPanel;
    public UpgradePanel shootDelayPanel;

    // Camera
    public UpgradePanel cameraZoomPanel;

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
    public float damageLevel;
    public float bulletAliveTime;
    public float bulletSpeed;
    public float shootDelay;

    // Camera
    public float cameraZoom;


    void Start()
    {
        GetStats();

        SetTexts();
    }

    void GetStats()
    {
        // Score
        score = StatsManager.instance.score;

        // Player
        health = StatsManager.instance.health;
        healthGeneration = StatsManager.instance.healthGeneration;
        healthGenerationDelay = StatsManager.instance.healthGenerationDelay;
        speed = StatsManager.instance.speed;

        // Laser
        laserLength = StatsManager.instance.laserLength;

        // Offense
        damageLevel = StatsManager.instance.damageLevel;
        bulletAliveTime = StatsManager.instance.bulletAliveTime;
        bulletSpeed = StatsManager.instance.bulletSpeed;
        shootDelay = StatsManager.instance.shootDelay;

        // Camera
        cameraZoom = StatsManager.instance.cameraZoom;
    }

    /// <summary>
    /// Sets the texts of the score and all upgrade panels
    /// </summary>
    void SetTexts()
    {
        // Score
        scoreText.text = "Score: " + score;

        // Player
        healthPanel.panelText.text = "Health: " + health;
        healthGenerationPanel.panelText.text = "Health Generation: " + healthGeneration;
        healthGenerationDelayPanel.panelText.text = "Health Generation Delay: " + healthGenerationDelay;
        speedPanel.panelText.text = "Speed: " + speed;

        // Laser
        laserLengthPanel.panelText.text = "Laser Length: " + laserLength;

        // Offense
        damageLevelPanel.panelText.text = "Damage Level: " + damageLevel;
        bulletAlivePanel.panelText.text = "Bullet Alive Time: " + bulletAliveTime;
        bulletSpeedPanel.panelText.text = "Bullet Speed: " + bulletSpeed;
        shootDelayPanel.panelText.text = "Shoot Delay: " + shootDelay;

        // Camera
        cameraZoomPanel.panelText.text = "Camera Zoom: " + cameraZoom;
    }

    public void NextWave()
    {
        PlayerPrefs.SetString("Scene", "Game");
        SceneManager.LoadScene("Loading");
    }
}

[System.Serializable]
public class UpgradePanel
{
    public Button upgradeButton;
    public Text upgradeButtonText;
    public Text panelText;
}

