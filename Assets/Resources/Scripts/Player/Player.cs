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
    float moveSpeed;

    // Damaged
    float gettingHitDelay = 0.5f;
    bool isDamaged;
    bool isDead;

    // Health
    bool isGeneratingHealth;
    public int health, maxHealth;
    int healthGeneration, healthGenerationDelay;

    // Score
    public int score;

    // Shooting
    bool canShootLeft = true, canShootRight = true;
    float bulletSpeed;
    float shootDelayInSeconds;


    void Start()
    {
        // Stats
        GetStats();
        
        // Animator
        animator = GetComponent<Animator>();
        animator.SetBool("Aiming", true);

        // Player
        rBody = GetComponent<Rigidbody>();
        // enable regeneration
        InvokeRepeating("RegenerateHealth", healthGenerationDelay, healthGenerationDelay);

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

        health = maxHealth;
    }

    // InvokeRepeating
    void RegenerateHealth()
    {
        isGeneratingHealth = true;

        if (health < maxHealth)
            health += healthGeneration;
        else if (health > maxHealth)    // Dont want it to go over max lol
            health = maxHealth;

        UIManager.instance.UpdateHealthBar(health, maxHealth);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == Layer.AI)
            ReceiveDamage(other.GetComponent<AI>());
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

    // Called in Bullet.cs
    public void ApplyScore(int specialMultiplier = 1)
    {
        score += RandomScore() * specialMultiplier;
        UIManager.instance.UpdateScoreText(score);
    }

    int RandomScore()
    {
        return Mathf.RoundToInt(Random.Range(100, 150) * StatsManager.instance.difficultyMultiplier);
    }

    void ReceiveDamage(AI ai)
    {
        if (isDamaged || isDead)
            return;
        isDamaged = true;

        animator.SetTrigger("Damage");

        health -= ai.Damage();
        if (health <= 0)
        {
            Death();
        }

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
        SceneManager.LoadScene("GameOver");
    }

    public void WaveComplete()
    {
        UIManager.instance.ShowWavePanel("Wave completed!");

        int nextWave = StatsManager.instance.wave + 1;
        float difficultyMultiplier = nextWave * 0.5f;

        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.SetInt("Wave", nextWave);
        PlayerPrefs.SetFloat("DifficultyMultiplier", difficultyMultiplier);

        Invoke("LoadUpgradeShop", 5); // 5 = delay in seconds
    }
}