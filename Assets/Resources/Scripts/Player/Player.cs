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
    bool dead;

    int health, maxHealth;
    int knockbackForce;
    int score;
    int money;

    float moveSpeed = 2f;

    // Shooting
    bool canShootLeft = true, canShootRight = true;
    float shootDelayInSeconds = 0.25f;

    float shootSpeed = 200;


    void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("Aiming", true);
        rBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        // enemy shit?
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

        Bullet bullet = BulletManager.instance.SpawnBullet();
        bullet.transform.position = startPosition;
        bullet.transform.eulerAngles = new Vector3(bullet.transform.eulerAngles.x, transform.eulerAngles.y, bullet.transform.eulerAngles.z);

        bullet.rBody.velocity = Vector3.zero;
        bullet.rBody.AddForce(direction * shootSpeed);

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
}