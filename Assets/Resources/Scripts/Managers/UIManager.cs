using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    // Highscore
    public Text highscoreText;

    // Wave
    public Image wavePanel;
    public Text wavePanelText;
    public Text waveText;
    bool wavePanelAnimated;
    float disableWavePanelDelay = 2;

    // Player health bar
    public Image healthBarWrap;
    public Image healthBar;
    public Text healthText;
    Color healthBarStartColor;

    // Charges (Special Attack)
    public Text chargesText;

    // Score
    public Text scoreText;

    // AI
    public Text aiCountText;

    // Slider
    public Slider musicSlider;
    public Slider sfxSlider;
    int sliderSteps = 5;        // So that the slidervalues always rounds to steps of 5

    // Pause
    public GameObject pausePanel;

    // Hitpoint
    public HitPoint hitPointsPrefab;

    // Flash Ahaa
    public GameObject flash; // Red flash when damaged


    void Start()
    {
        instance = this;

        // Cursor
        SetCursor();

        // Highscore
        SetHighscoreText();

        // Sliders
        SetSliders();

        // Player health
        if (healthBar)
            healthBarStartColor = healthBar.color;

        // Pause
        if (pausePanel)
            pausePanel.SetActive(false);

        // Flash
        if (flash)
            flash.SetActive(false);

        // Wave
        SetWaveText();
        Invoke("DisableWavePanel", disableWavePanelDelay);
    }

    /// <summary>
    /// Sets the image of the cursor and hides the cursor when ingame
    /// </summary>
    void SetCursor()
    {
        Cursor.visible = !(SceneManager.GetActiveScene().name == "Game");
    }

    void SetHighscoreText()
    {
        if (SceneManager.GetActiveScene().name == "Start")
        {
            highscoreText.text = (StatsManager.instance.highscore > 0) ?
                "Highscore: " + StatsManager.instance.highscore + " !" :
                "Flocking Hell";
        }
    }

    void SetWaveText()
    {
        if (!wavePanelText)
        {
            if (wavePanel)
                wavePanelText = wavePanel.GetComponentInChildren<Text>();
            else
                return;
        }
        wavePanelText.text = "Wave " + StatsManager.instance.wave;
        waveText.text = "Wave: " + StatsManager.instance.wave;
    }

    void SetSliders()
    {
        if (!musicSlider || !sfxSlider)
            return;

        // Set volume if it ever has been set
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 100) / sliderSteps;
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 100) / sliderSteps;
        
        OnSliderValueChange(musicSlider, "Music");
        OnSliderValueChange(sfxSlider, "SFX");

        musicSlider.onValueChanged.AddListener(delegate { OnSliderValueChange(musicSlider, "Music"); });
        sfxSlider.onValueChanged.AddListener(delegate { OnSliderValueChange(sfxSlider, "SFX"); });
    }

    void OnSliderValueChange(Slider slider, string label)
    {
        float value = slider.value * sliderSteps;     // Because I want steps of 5 (0,5,10,15 etc.)
        slider.GetComponentInChildren<Text>().text = label + ": " + (int)value + "%";

        AudioManager.instance.PlaySound(GetComponentInChildren<AudioSource>(), Sounds.Select);
        PlayerPrefs.SetFloat(label + "Volume", value);
        AudioManager.instance.SetVolumes();
    }

    // Invoke
    public void DisableWavePanel()
    {
        if (wavePanel)
            wavePanel.gameObject.SetActive(false);
    }

    public void ShowWavePanel(string text = "")
    {
        wavePanel.gameObject.SetActive(true);
        wavePanelText.text = text;
        Invoke("DisableWavePanel", disableWavePanelDelay);
    }

    public void StartGame()
    {
        AudioManager.instance.PlaySound(GetComponentInChildren<AudioSource>(), Sounds.Select);
        SceneManager.LoadScene("UpgradeShop");
    }

    public void TogglePausePanel(bool show)
    {
        AudioManager.instance.PlaySound(GetComponentInChildren<AudioSource>(), Sounds.Select);
        if (show)
            AudioManager.instance.musicSource.Pause();
        else
            AudioManager.instance.musicSource.UnPause();

        pausePanel.SetActive(show);
    }

    public void UpdateHealthBar(int health, int maxHealth)
    {
        healthBar.color = Color.Lerp(Color.red, healthBarStartColor, (float)health / (float)maxHealth); // Casten naar float omdat int delen door int voor een float is 0
        healthText.text = health + "/" + maxHealth;
    }

    public void UpdateChargesText(int charges)
    {
        chargesText.text = "Charges: " + charges;
    }

    public void UpdateScoreText(int score)
    {
        scoreText.text = "Score: " + score;
    }

    public void UpdateAIAliveText(int aiAlive)
    {
        aiCountText.text = "Enemies alive: " + aiAlive;
    }

    public void SpawnHitPoint(Vector3 position, Color color, int hitPoints)
    {
        HitPoint hitPoint = Instantiate(hitPointsPrefab);
        hitPoint.Initiate(position, color, hitPoints);
    }

    public void Flash()
    {
        flash.SetActive(true);

        Invoke("DisableFlash", 0.05f);
    }

    void DisableFlash()
    {
        flash.SetActive(false);
    }
}
