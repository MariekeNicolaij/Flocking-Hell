using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpgradeShop : MonoBehaviour
{
    // Health
    public int health;
    public int healthGeneration, healthGenerationDelay;

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
        
    }

    void GetStats()
    {

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
