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
    public UpgradePanel cameraZoomLevelPanel;

    // ------Stats-------
    // Score
    public int score;

    // Player
    public int health;
    public int healthGeneration;
    public int healthGenerationDelay;
    public int speed;

    // Laser
    public int laserLength;

    // Offense
    public float damageLevel;
    public int bulletAliveTime;
    public int bulletSpeed;
    public float shootDelay;

    // Camera
    public int cameraZoomLevel;

    // ------Costs-------
    float costMultiplier = 1.5f;
    // Player
    int healthCost;
    int healthGenerationCost;
    int healthGenerationDelayCost;
    int speedCost;

    // Laser
    int laserLengthCost;

    // Offense
    int damageLevelCost;
    int bulletAliveTimeCost;
    int bulletSpeedCost;
    int shootDelayCost;

    // Camera
    int cameraZoomLevelCost;

    // ------Next level upgrades-------
    // Player
    int healthUpgrade = 50;
    int healthGenerationUpgrade = 1;
    int healthGenerationDelayUpgrade = 1; // minus
    int speedUpgrade = 1;

    // Laser
    int laserLengthUpgrade = 1;

    // Offense
    float damageLevelUpgrade = 0.5f;
    int bulletAliveTimeUpgrade = 1;
    int bulletSpeedUpgrade = 100;
    float shootDelayUpgrade = 0.1f; // minus

    // Camera
    int cameraZoomLevelUpgrade = 1;


    void Start()
    {
        GetStats();
        GetCosts();
        SetTexts();
        ToggleButtons();
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
        cameraZoomLevel = StatsManager.instance.cameraZoomLevel;
    }

    void GetCosts()
    {
        // Player
        healthCost = PlayerPrefs.GetInt("HealthCost", 1000);
        healthGenerationCost = PlayerPrefs.GetInt("HealthGenerationCost", 10000);
        healthGenerationDelayCost = PlayerPrefs.GetInt("HealthGenerationDelayCost", 20000);
        speedCost = PlayerPrefs.GetInt("SpeedCost", 5000);

        // Laser
        laserLengthCost = PlayerPrefs.GetInt("LaserLengthCost", 1000);

        // Offense
        damageLevelCost = PlayerPrefs.GetInt("DamageLevelCost", 10000);
        bulletAliveTimeCost = PlayerPrefs.GetInt("BulletAliveTimeCost", 3000);
        bulletSpeedCost = PlayerPrefs.GetInt("BulletSpeedCost", 5000);
        shootDelayCost = PlayerPrefs.GetInt("ShootDelayCost", 25000);

        // Camera
        cameraZoomLevelCost = PlayerPrefs.GetInt("CameraZoomLevelCost", 20000);
    }

    /// <summary>
    /// Sets the texts of the score and all upgrade panels and buttons
    /// </summary>
    void SetTexts()
    {
        // Score
        scoreText.text = "$ " + score;

        // --- Panel texts ---
        // Player
        healthPanel.panelText.text = "Health: \n" + health + " > " + (health + healthUpgrade);
        healthGenerationPanel.panelText.text = "Health Generation: \n" + healthGeneration + " > " + (healthGeneration + healthGenerationUpgrade);
        healthGenerationDelayPanel.panelText.text = "HG Delay: \n" + healthGenerationDelay + " > " + (healthGenerationDelay - healthGenerationDelayUpgrade);
        speedPanel.panelText.text = "Movespeed: \n" + speed + " > " + (speed + speedUpgrade);

        // Laser
        laserLengthPanel.panelText.text = "Laser Length: \n" + laserLength + " > " + (laserLength + laserLengthUpgrade);

        // Offense
        damageLevelPanel.panelText.text = "Damage Level: \n" + damageLevel + " > " + (damageLevel + damageLevelUpgrade);
        bulletAlivePanel.panelText.text = "Bullet Alive Time: \n" + bulletAliveTime + " > " + (bulletAliveTime + bulletAliveTimeUpgrade);
        bulletSpeedPanel.panelText.text = "Bullet Speed: \n" + bulletSpeed + " > " + (bulletSpeed + bulletSpeedUpgrade);
        shootDelayPanel.panelText.text = "Shoot Delay: \n" + shootDelay + " > " + (shootDelay - shootDelayUpgrade);

        // Camera
        cameraZoomLevelPanel.panelText.text = "Camera Zoom Level: \n" + cameraZoomLevel + " > " + (cameraZoomLevel + cameraZoomLevelUpgrade);

        // --- Button texts (Cost) ---
        // Player
        healthPanel.upgradeButtonText.text = "$ " + healthCost;
        healthGenerationPanel.upgradeButtonText.text = "$ " + healthGenerationCost;
        healthGenerationDelayPanel.upgradeButtonText.text = "$ " + healthGenerationDelayCost;
        speedPanel.upgradeButtonText.text = "$ " + speedCost;

        // Laser
        laserLengthPanel.upgradeButtonText.text = "$ " + laserLengthCost;

        // Offense
        damageLevelPanel.upgradeButtonText.text = "$ " + damageLevelCost;
        bulletAlivePanel.upgradeButtonText.text = "$ " + bulletAliveTimeCost;
        bulletSpeedPanel.upgradeButtonText.text = "$ " + bulletSpeedCost;
        shootDelayPanel.upgradeButtonText.text = "$ " + shootDelayCost;

        // Camera
        cameraZoomLevelPanel.upgradeButtonText.text = "$ " + cameraZoomLevelCost;
    }

    /// <summary>
    /// Checks if the player can afford upgrades, if not then disable then, if so enable them
    /// </summary>
    void ToggleButtons()
    {
        healthPanel.upgradeButton.interactable = (healthCost <= score);
        healthGenerationPanel.upgradeButton.interactable = (healthGenerationCost <= score);
        healthGenerationDelayPanel.upgradeButton.interactable = (healthGenerationDelayCost <= score);
        speedPanel.upgradeButton.interactable = (speedCost <= score);
        laserLengthPanel.upgradeButton.interactable = (laserLengthCost <= score);
        damageLevelPanel.upgradeButton.interactable = (damageLevelCost <= score);
        bulletAlivePanel.upgradeButton.interactable = (bulletAliveTimeCost <= score);
        bulletSpeedPanel.upgradeButton.interactable = (bulletSpeedCost <= score);
        shootDelayPanel.upgradeButton.interactable = (shootDelayCost <= score);
        cameraZoomLevelPanel.upgradeButton.interactable = (cameraZoomLevelCost <= score);
    }

    void SaveStatsAndCosts()
    {
        // -------Stats--------
        PlayerPrefs.SetInt("Score", score);

        PlayerPrefs.SetInt("Health", health);
        PlayerPrefs.SetInt("HealthGeneration", healthGeneration);
        PlayerPrefs.SetInt("HealthGenerationDelay", healthGenerationDelay);
        PlayerPrefs.SetInt("Speed", speed);

        PlayerPrefs.SetInt("LaserLength", laserLength);

        PlayerPrefs.SetFloat("DamageLevel", damageLevel);
        PlayerPrefs.SetInt("BulletAliveTime", bulletAliveTime);
        PlayerPrefs.SetInt("BulletSpeed", bulletSpeed);
        PlayerPrefs.SetFloat("ShootDelay", shootDelay);

        PlayerPrefs.SetInt("CameraZoom", cameraZoomLevel);

        // -------Costs--------
        // Player
        PlayerPrefs.SetInt("HealthCost", healthCost);
        PlayerPrefs.SetInt("HealthGenerationCost", healthGenerationCost);
        PlayerPrefs.SetInt("HealthGenerationDelayCost", healthGenerationDelayCost);
        PlayerPrefs.SetInt("SpeedCost", speedCost);

        // Laser
        PlayerPrefs.SetInt("LaserLengthCost", laserLengthCost);

        // Offense
        PlayerPrefs.SetInt("DamageLevelCost", damageLevelCost);
        PlayerPrefs.SetInt("BulletAliveTimeCost", bulletAliveTimeCost);
        PlayerPrefs.SetInt("BulletSpeedCost", bulletSpeedCost);
        PlayerPrefs.SetInt("ShootDelayCost", shootDelayCost);

        // Camera
        PlayerPrefs.SetInt("CameraZoomLevelCost", cameraZoomLevelCost);
    }

    public void Upgrade(int upgradeEnumIndex) // Why is passing enums in UI still not supported?
    {
        Upgrades upgrade = (Upgrades)upgradeEnumIndex;

        switch (upgrade)
        {
            case Upgrades.Health:
                score -= healthCost;
                health += healthUpgrade;
                healthCost = Mathf.RoundToInt(healthCost * costMultiplier);
                break;
            case Upgrades.HealthGeneration:
                score -= healthGenerationCost;
                healthGeneration += healthGenerationUpgrade;
                healthGenerationCost = Mathf.RoundToInt(healthGenerationCost * costMultiplier);
                break;
            case Upgrades.HealthGenerationDelay:
                score -= healthGenerationDelayCost;
                healthGenerationDelay -= healthGenerationDelayUpgrade;
                healthGenerationDelayCost = Mathf.RoundToInt(healthGenerationDelayCost * costMultiplier);
                break;
            case Upgrades.Speed:
                score -= speedCost;
                speed += speedUpgrade;
                speedCost = Mathf.RoundToInt(speedCost * costMultiplier);
                break;
            case Upgrades.LaserLength:
                score -= laserLengthCost;
                laserLength += laserLengthUpgrade;
                laserLengthCost = Mathf.RoundToInt(laserLengthCost * costMultiplier);
                break;
            case Upgrades.DamageLevel:
                score -= damageLevelCost;
                damageLevel += damageLevelUpgrade;
                damageLevelCost = Mathf.RoundToInt(damageLevelCost * costMultiplier);
                break;
            case Upgrades.BulletAliveTime:
                score -= bulletAliveTimeCost;
                bulletAliveTime += bulletAliveTimeUpgrade;
                bulletAliveTimeCost = Mathf.RoundToInt(bulletAliveTimeCost * costMultiplier);
                break;
            case Upgrades.BulletSpeed:
                score -= bulletSpeedCost;
                bulletSpeed += bulletSpeedUpgrade;
                bulletSpeedCost = Mathf.RoundToInt(bulletSpeedCost * costMultiplier);
                break;
            case Upgrades.ShootDelay:
                score -= shootDelayCost;
                shootDelay -= shootDelayUpgrade;
                shootDelayCost = Mathf.RoundToInt(shootDelayCost * costMultiplier);
                break;
            case Upgrades.CameraZoomLevel:
                score -= cameraZoomLevelCost;
                cameraZoomLevel += cameraZoomLevelUpgrade;
                cameraZoomLevelCost = Mathf.RoundToInt(cameraZoomLevelCost * costMultiplier);
                break;
        }

        SaveStatsAndCosts();
        SetTexts();
        ToggleButtons();
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

public enum Upgrades
{
    Health = 0,
    HealthGeneration = 1,
    HealthGenerationDelay = 2,
    Speed = 3,
    DamageLevel = 4,
    LaserLength = 5,
    BulletAliveTime = 6,
    BulletSpeed = 7,
    ShootDelay = 8,
    CameraZoomLevel = 9
}