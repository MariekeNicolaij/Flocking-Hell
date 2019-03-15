using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    // Cursor
    public Texture2D cursor;

    // Highscore
    public Text highscoreText;

    // Wave
    public Image wavePanel;
    public Text waveText;
    bool wavePanelAnimated;
    float disableWavePanelDelay = 2;

    // Player health bar
    public Image healthBarWrap;
    public Image healthBar;
    public Text healthText;
    Color healthBarStartColor;

    // Score
    public Text scoreText;

    // AI
    public Text aiCountText;


    void Start()
    {
        instance = this;

        // Cursor
        SetCursor();

        // Highscore
        SetHighscoreText();

        // Player health
        if (healthBar)
            healthBarStartColor = healthBar.color;

        // Wave
        SetWaveText();
        Invoke("DisableWavePanel", disableWavePanelDelay);
    }

    /// <summary>
    /// Sets the image of the cursor and hides the cursor when ingame
    /// </summary>
    void SetCursor()
    {
        if (cursor)
            Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
        Cursor.visible = !(SceneManager.GetActiveScene().name == "Game");
    }

    void SetHighscoreText()
    {
        if (SceneManager.GetActiveScene().name == "Start")
            highscoreText.text = "Highscore: " + StatsManager.instance.highscore + " !";
    }

    void SetWaveText()
    {
        if (!waveText)
        {
            if (wavePanel)
                waveText = wavePanel.GetComponentInChildren<Text>();
            else
                return;
        }
        waveText.text = "Wave " + StatsManager.instance.wave;
    }

    // Invoke
    public void DisableWavePanel()
    {
        wavePanel.gameObject.SetActive(false);
    }

    public void ShowWavePanel(string wavePanelText = "")
    {
        wavePanel.gameObject.SetActive(true);
        waveText.text = wavePanelText;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("UpgradeShop");
    }

    public void UpdateHealthBar(int health, int maxHealth)
    {
        healthBar.color = Color.Lerp(Color.red, healthBarStartColor, (float)health / (float)maxHealth); // Casten naar float omdat int delen door int voor een float is 0
        healthText.text = health + "/" + maxHealth;
    }

    public void UpdateScoreText(int score)
    {
        scoreText.text = "Score: " + score;
    }

    public void UpdateAIAliveText(int aiAlive)
    {
        aiCountText.text = "Enemies alive: " + aiAlive;
    }

    // Show upgradable stats on screen for testing purposes
    void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.font = (Font)Resources.Load("Fonts/Font");
        style.fontSize = 20;
        style.normal.textColor = Color.white;

        GUI.TextArea(
            new Rect(Screen.width * 0.05f, Screen.height * 0.3f, Screen.width * 0.2f, Screen.height * 0.2f),
            "Max health: \t" + StatsManager.instance.health + "\n" +
            "Health generation: \t" + StatsManager.instance.healthGeneration + "\n" +
            "Generation delay: \t" + StatsManager.instance.healthGenerationDelay + "\n" +
            "Laser length: \t" + StatsManager.instance.laserLength + "\n" +
            "Min damage: \t" + StatsManager.instance.minDamage + "\n" +
            "Max damage: \t" + StatsManager.instance.maxDamage + "\n" +
            "Bullet alive time: \t" + StatsManager.instance.bulletAliveTime + "\n" +
            "Bullet speed: \t" + StatsManager.instance.bulletSpeed + "\n" +
            "Shoot delay: \t" + StatsManager.instance.shootDelay + "\n" +
            "Camera zoom: \t" + StatsManager.instance.cameraZoom + "\n",
            style);
    }
}
