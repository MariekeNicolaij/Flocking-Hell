using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public Image wavePanel;
    bool wavePanelAnimated;
    float disableWavePanelDelay = 2;

    public Image healthBarWrap;
    public Image healthBar;
    public Text healthText;
    Color healthBarStartColor;


    void Start()
    {
        instance = this;
        healthBarStartColor = healthBar.color;
        Invoke("DisableWavePanel", disableWavePanelDelay);
    }

    void Update()
    {
        
    }

    void DisableWavePanel()
    {
        wavePanel.gameObject.SetActive(false);
    }

    public void UpdateHealthBar(int health, int maxHealth)
    {
        healthBar.color = Color.Lerp(Color.red, healthBarStartColor, (float)health / (float)maxHealth); // Casten naar float omdat int delen door int voor een float is 0
        healthText.text = health + "/" + maxHealth;
    }
}
