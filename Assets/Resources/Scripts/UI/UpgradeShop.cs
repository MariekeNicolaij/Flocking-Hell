using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpgradeShop : MonoBehaviour
{
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
        PlayerPrefs.SetInt("Wave", PlayerPrefs.GetInt("Wave", 0) + 1);
        PlayerPrefs.SetString("Scene", "Game");
        SceneManager.LoadScene("Loading");
    }
}
