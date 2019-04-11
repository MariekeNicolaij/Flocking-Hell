using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerInputController))]
[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [Header("Animator")]
    [HideInInspector]
    public Animator animator;

    public GameObject leftPistol;
    public GameObject rightPistol;

    // Player
    Rigidbody rBody;
    public float moveSpeed;

    // Damaged
    float gettingHitDelay = 0.5f;
    bool isDamaged;
    public bool isDead;

    // Health
    bool isGeneratingHealth;
    public int health, maxHealth;
    int healthGeneration, healthGenerationDelay;

    // Score
    public int score;
    int maxScore = 999999999;

    // Shooting
    bool canShootLeft = true, canShootRight = true;
    float bulletSpeed;
    float shootDelayInSeconds;

    int specialAttackCharges;

    // Pause
    bool pause;


    void Start()
    {
        // Stats
        GetStats();

        // Animator
        animator = GetComponent<Animator>();
        animator.SetBool("Aiming", true);

        // Player
        rBody = GetComponent<Rigidbody>();

        // Enable regeneration
        InvokeRepeating("RegenerateHealth", healthGenerationDelay, healthGenerationDelay);

        // Because UIManager does not really 'exist' at this point
        Invoke("UpdateUIOnceAtStart", 0.1f);
    }

    void GetStats()
    {
        maxHealth = StatsManager.instance.health;
        healthGeneration = StatsManager.instance.healthGeneration;
        healthGenerationDelay = StatsManager.instance.healthGenerationDelay;
        moveSpeed = StatsManager.instance.speed;

        score = StatsManager.instance.score;

        bulletSpeed = StatsManager.instance.bulletSpeed;
        shootDelayInSeconds = StatsManager.instance.shootDelay;

        specialAttackCharges = StatsManager.instance.specialAttackCharges;

        health = maxHealth;
    }

    void UpdateUIOnceAtStart()
    {
        UIManager.instance.UpdateScoreText(score);
        UIManager.instance.UpdateHealthBar(health, maxHealth);
        UIManager.instance.UpdateChargesText(specialAttackCharges);
    }

    // InvokeRepeating
    void RegenerateHealth()
    {
        isGeneratingHealth = true;

        if (health < maxHealth)
        {
            health += healthGeneration;

            // Hitpoint UI
            UIManager.instance.SpawnHitPoint(transform.position, Colors.green, -healthGeneration);
            // Update Healthbar
            UIManager.instance.UpdateHealthBar(health, maxHealth);
        }
        if (health > maxHealth)    // Dont want it to go over max lol
        {
            health = maxHealth;

            // Hitpoint UI
            UIManager.instance.SpawnHitPoint(transform.position, Colors.green, -healthGeneration);
            // Update Healthbar
            UIManager.instance.UpdateHealthBar(health, maxHealth);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == Layer.AI)
        {
            ReceiveDamage(other.GetComponent<AI>().Damage());
        }
        if (other.tag == "Fire")
            ReceiveDamage(5);   // Fire damage I guess
    }

    // Called in PlayerInput
    public void Move(float x, float y)
    {
        Vector3 direction = new Vector3(x, 0, y);
        rBody.MovePosition(transform.position + (direction * moveSpeed * Time.deltaTime));
    }

    // Called in PlayerInput
    public void LookAt(Vector3 direction)
    {
        transform.LookAt(transform.position + direction);
    }

    // Called in PlayerInput
    public void InitiateShootLeftPistol()
    {
        if (!canShootLeft)
            return;
        canShootLeft = false;

        Shoot(true); // Left
    }

    // Called in PlayerInput
    public void InitiateShootRightPistol()
    {
        if (!canShootRight)
            return;
        canShootRight = false;

        Shoot(false); // Right
    }

    public void Shoot(bool shootLeft)
    {
        GameObject shootingPistol = (shootLeft) ? leftPistol : rightPistol;

        Vector3 startPosition = shootingPistol.transform.position;
        Vector3 direction = transform.forward;

        Bullet bullet = BulletManager.instance.SpawnBullet(startPosition, transform.eulerAngles.y);

        bullet.rBody.velocity = Vector3.zero;
        bullet.rBody.AddForce(direction * bulletSpeed);

        string shootDelayMethodName = (shootLeft) ? "ShootDelayLeft" : "ShootDelayRight";
        Invoke(shootDelayMethodName, shootDelayInSeconds);
    }

    void ShootDelayLeft()
    {
        canShootLeft = true;
    }

    void ShootDelayRight()
    {
        canShootRight = true;
    }

    // Called in PlayerInput
    public void SpecialAttack()
    {
        if (specialAttackCharges <= 0)
            return;
        specialAttackCharges--;

        Vector3 startPosition = transform.position;
        startPosition.y = 0.7f; // Pistol height
        Vector3 direction = transform.forward;

        Bullet bullet = BulletManager.instance.SpawnSpecialBullet(startPosition, transform.eulerAngles.y);
        bullet.rBody.velocity = Vector3.zero;
        bullet.rBody.AddForce(direction * bulletSpeed);

        // Update Text
        UIManager.instance.UpdateChargesText(specialAttackCharges);
    }

    // Called in PlayerInput
    public void Pause()
    {
        pause = !pause;
        Time.timeScale = System.Convert.ToInt32(!pause);
        Cursor.visible = pause;
        UIManager.instance.TogglePausePanel(pause);
    }

    // Called in Bullet.cs
    public void ApplyScore(int specialMultiplier = 1)
    {
        score += RandomScore() * specialMultiplier;

        if (score > maxScore)   // If it somehow gets to this point lmao
            score = maxScore;

        UIManager.instance.UpdateScoreText(score);
    }

    int RandomScore()
    {
        return Mathf.RoundToInt(Random.Range(20, 30) * StatsManager.instance.difficultyMultiplier);
    }

    void ReceiveDamage(int damage)
    {
        if (isDamaged || isDead)
            return;
        isDamaged = true;

        animator.SetTrigger("Damage");

        health -= damage;
        if (health <= 0)
            Death();

        // Hitpoint UI
        UIManager.instance.SpawnHitPoint(transform.position, Colors.red, damage);
        // Flash
        UIManager.instance.Flash();
        // Update health bar
        UIManager.instance.UpdateHealthBar(health, maxHealth);

        Invoke("DamageDelay", gettingHitDelay);
    }

    void DamageDelay()
    {
        isDamaged = false;
    }

    void Death()
    {
        health = 0;
        isDead = true;

        animator.SetBool("Death", true);
        rBody.constraints = RigidbodyConstraints.FreezeAll;

        CancelInvoke();

        UIManager.instance.ShowWavePanel("Game over!");
        Invoke("LoadGameOver", 3); // 3 = delay
    }

    void LoadUpgradeShop()
    {
        SceneManager.LoadScene("UpgradeShop");
    }

    void LoadGameOver()
    {
        // Remove stored data
        StatsManager.instance.OnApplicationQuit();
        SceneManager.LoadScene("GameOver");
    }

    public void WaveComplete()
    {
        UIManager.instance.ShowWavePanel("Wave completed!");

        int nextWave = StatsManager.instance.wave + 1;
        float difficultyMultiplier = nextWave * 0.33f;

        // Save score
        int oldScore = PlayerPrefs.GetInt("Score");
        PlayerPrefs.SetInt("Score", score + oldScore);

        // Save wave
        PlayerPrefs.SetInt("Wave", nextWave);

        // Set difficulty
        PlayerPrefs.SetFloat("DifficultyMultiplier", difficultyMultiplier);

        Cursor.visible = true;

        Invoke("LoadUpgradeShop", 5); // 5 = delay in seconds
    }
}