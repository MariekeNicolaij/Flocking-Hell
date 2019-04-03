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
    public UpgradePanel bulletSpeedPanel;
    public UpgradePanel shootDelayPanel;

    // Camera
    public UpgradePanel cameraZoomLevelPanel;

    // Special attack
    public UpgradePanel specialAttackPanel;

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
    public int bulletSpeed;
    public float shootDelay;

    // Camera
    public int cameraZoomLevel;

    // Special Attack
    public int specialAttackCharges;

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
    int bulletSpeedCost;
    int shootDelayCost;

    // Camera
    int cameraZoomLevelCost;

    // Special attack
    int specialAttackChargesCost;

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
    int bulletSpeedUpgrade = 100;
    float shootDelayUpgrade = 0.1f; // minus

    // Camera
    int cameraZoomLevelUpgrade = 1;

    // Special Attack
    int specialAttackUpgrade = 1;

    // ------Max/Min values for some stats-------
    // Max
    int healthMax = 1000;
    int healthGenerationMax = 20;
    int speedMax = 10;
    int laserLengthMax = 8;
    int bulletSpeedMax = 1000;
    int cameraZoomLevelMax = 10;

    //Min
    int healthGenerationDelayMin = 1;
    float shootDelayMin = 0.1f;


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
        bulletSpeed = StatsManager.instance.bulletSpeed;
        shootDelay = StatsManager.instance.shootDelay;

        // Camera
        cameraZoomLevel = StatsManager.instance.cameraZoomLevel;

        // Special Attack
        specialAttackCharges = StatsManager.instance.specialAttackCharges;
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
        bulletSpeedCost = PlayerPrefs.GetInt("BulletSpeedCost", 5000);
        shootDelayCost = PlayerPrefs.GetInt("ShootDelayCost", 25000);

        // Camera
        cameraZoomLevelCost = PlayerPrefs.GetInt("CameraZoomLevelCost", 20000);

        // Special attack
        specialAttackChargesCost = PlayerPrefs.GetInt("SpecialAttackCost", 100000);
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
        healthPanel.panelText.text = "Health (" + health + ") : \n" +
            ((IsMaxedOut(health, healthMax)) ?
            "Maxed out!" :
            health + " > " + (health + healthUpgrade));

        healthGenerationPanel.panelText.text = "Health Generation (" + healthGeneration + ") : \n" +                         // Base text
            ((IsMaxedOut(healthGeneration, healthGenerationMax)) ?                                   // If it is maxed out
            "Maxed out!" :                                                                           // Add this text to it otherwise
            healthGeneration + " > " + (healthGeneration + healthGenerationUpgrade));                // Add this

        healthGenerationDelayPanel.panelText.text = "Health Generation Delay (" + healthGenerationDelay + ") : \n" +// Base text
            ((IsMaxedOut(healthGenerationDelay, healthGenerationDelayMin, false)) ?                  // If it is 'minned' out
            "Maxed out!" :                                                                           // Add this text to it otherwise
            healthGenerationDelay + " > " + (healthGenerationDelay - healthGenerationDelayUpgrade)); // Add this

        speedPanel.panelText.text = "Movespeed (" + speed + ") : \n" +
            ((IsMaxedOut(speed, speedMax)) ?
            "Maxed out!" :
            speed + " > " + (speed + speedUpgrade));

        // Laser
        laserLengthPanel.panelText.text = "Laser Length (" + laserLength + ") : \n" +
            ((IsMaxedOut(laserLength, laserLengthMax)) ?
            "Maxed out!" :
            laserLength + " > " + (laserLength + laserLengthUpgrade));

        // Offense
        damageLevelPanel.panelText.text = "Damage Level (" + damageLevel + ") : \n" + damageLevel + " > " + (damageLevel + damageLevelUpgrade);

        bulletSpeedPanel.panelText.text = "Bullet Speed (" + bulletSpeed + ") : \n" +
            ((IsMaxedOut(bulletSpeed, bulletSpeedMax)) ?
            "Maxed out!" :
            bulletSpeed + " > " + (bulletSpeed + bulletSpeedUpgrade));

        shootDelayPanel.panelText.text = "Shoot Delay (" + shootDelay + ") : \n" +
            ((IsMaxedOut(shootDelay, shootDelayMin, false)) ?
            "Maxed out!" :
            shootDelay + " > " + (shootDelay - shootDelayUpgrade));

        // Camera
        cameraZoomLevelPanel.panelText.text = "Camera Zoom Level (" + cameraZoomLevel + ") : \n" +
            ((IsMaxedOut(cameraZoomLevel, cameraZoomLevelMax)) ?
            "Maxed out!" :
            cameraZoomLevel + " > " + (cameraZoomLevel + cameraZoomLevelUpgrade));

        // Special attack
        specialAttackPanel.panelText.text = "Special Attack Charges: \n" +
            specialAttackCharges + " > " + (specialAttackCharges + specialAttackUpgrade);

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
        bulletSpeedPanel.upgradeButtonText.text = "$ " + bulletSpeedCost;
        shootDelayPanel.upgradeButtonText.text = "$ " + shootDelayCost;

        // Camera
        cameraZoomLevelPanel.upgradeButtonText.text = "$ " + cameraZoomLevelCost;

        // Special Attack
        specialAttackPanel.upgradeButtonText.text = "$ " + specialAttackChargesCost;
    }

    // Checks if (int)currentstat is maxed out
    bool IsMaxedOut(int currentValue, int maxedOutValue, bool checkForMaxedOut = true) // Check if it maxed out, otherwise check if it mins out (get it?)
    {
        return (checkForMaxedOut) ? (currentValue >= maxedOutValue) : (currentValue <= maxedOutValue);
    }

    // Checks if (float)currentstat is maxed out
    bool IsMaxedOut(float currentValue, float maxedOutValue, bool checkForMaxedOut = true) // Check if it maxed out, otherwise check if it mins out (get it?)
    {
        return (checkForMaxedOut) ? (currentValue >= maxedOutValue) : (currentValue <= maxedOutValue);
    }

    /// <summary>
    /// Checks if the player can afford upgrades, if not then disable then, if so enable them
    /// </summary>
    void ToggleButtons()
    {
        healthPanel.upgradeButton.interactable = (healthCost <= score && !IsMaxedOut(health, healthMax));
        healthGenerationPanel.upgradeButton.interactable = (healthGenerationCost <= score && !IsMaxedOut(healthGeneration, healthGenerationMax));
        healthGenerationDelayPanel.upgradeButton.interactable = (healthGenerationDelayCost <= score && !IsMaxedOut(healthGenerationDelay, healthGenerationDelayMin, false));
        speedPanel.upgradeButton.interactable = (speedCost <= score && !IsMaxedOut(speed, speedMax));
        laserLengthPanel.upgradeButton.interactable = (laserLengthCost <= score && !IsMaxedOut(laserLength, laserLengthMax));
        damageLevelPanel.upgradeButton.interactable = (damageLevelCost <= score);
        bulletSpeedPanel.upgradeButton.interactable = (bulletSpeedCost <= score && !IsMaxedOut(bulletSpeed, bulletSpeedMax));
        shootDelayPanel.upgradeButton.interactable = (shootDelayCost <= score && !IsMaxedOut(shootDelay, shootDelayMin, false));
        cameraZoomLevelPanel.upgradeButton.interactable = (cameraZoomLevelCost <= score && !IsMaxedOut(cameraZoomLevel, cameraZoomLevelMax));
        specialAttackPanel.upgradeButton.interactable = (specialAttackChargesCost <= score);
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
        PlayerPrefs.SetInt("BulletSpeed", bulletSpeed);
        PlayerPrefs.SetFloat("ShootDelay", shootDelay);

        PlayerPrefs.SetInt("CameraZoom", cameraZoomLevel);

        PlayerPrefs.SetInt("SpecialAttack", 1);

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
            case Upgrades.SpecialAttack:
                score -= specialAttackChargesCost;
                specialAttackCharges += specialAttackUpgrade;
                specialAttackChargesCost = Mathf.RoundToInt(specialAttackChargesCost * costMultiplier);
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
    BulletSpeed = 6,
    ShootDelay = 7,
    CameraZoomLevel = 8,
    SpecialAttack = 9
}