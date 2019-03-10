using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image wavePanel;
    bool wavePanelAnimated;
    float disableWavePanelDelay = 2;

    public Image healthBarWrap;
    public Image healthBar;
    public Text healthText;
    float healthBarWidth;


    void Start()
    {
        healthBarWidth = healthBarWrap.rectTransform.sizeDelta.x;
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
        healthBarWrap.rectTransform.sizeDelta = new Vector2(health / maxHealth * healthBarWidth, healthBarWrap.rectTransform.sizeDelta.y);
        healthText.text = health + "/" + maxHealth;
    }
}
