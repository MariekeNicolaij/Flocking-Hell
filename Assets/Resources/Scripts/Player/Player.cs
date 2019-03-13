using UnityEngine;

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

    float damageDelay = 1;
    bool isDamaged;
    bool dead;

    bool isGeneratingHealth;
    int health, maxHealth, healthGeneration, healthGenerationDelay;
    int knockbackForce;
    int score;
    int money;

    float moveSpeed = 2f;

    // Shooting
    bool canShootLeft = true, canShootRight = true;
    float bulletSpeed = 200;
    float shootDelayInSeconds = 0.25f;


    void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("Aiming", true);
        rBody = GetComponent<Rigidbody>();

        GetStats();
    }

    void GetStats()
    {
        maxHealth = StatsManager.instance.health;
        healthGeneration = StatsManager.instance.healthGeneration;
        healthGenerationDelay = StatsManager.instance.healthGenerationDelay;

        score = StatsManager.instance.score;

        bulletSpeed = StatsManager.instance.bulletSpeed;
        shootDelayInSeconds = StatsManager.instance.shootDelay;

        knockbackForce = StatsManager.instance.knockbackForce;

        health = maxHealth;
    }

    void Update()
    {
        RegenerateHealth();
    }

    void RegenerateHealth()
    {
        if (isGeneratingHealth)
            return;

        isGeneratingHealth = true;

        if (health < maxHealth)
            health += healthGeneration;
        else if (health > maxHealth)    // Dont want it to go over max lol
            health = maxHealth;

        Invoke("RegenerateHealthDelay", healthGenerationDelay);
    }

    void RegenerateHealthDelay()
    {
        isGeneratingHealth = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == Layer.AI)
        {
            ReceiveDamage(other.GetComponent<AI>());
        }
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

    void ReceiveDamage(AI ai)
    {
        if (isDamaged)
            return;
        isDamaged = true;

        health -= ai.Damage();
        UIManager.instance.UpdateHealthBar(health, maxHealth);

        Invoke("DamageDelay", damageDelay);
    }

    void DamageDelay()
    {
        isDamaged = false;
    }
}