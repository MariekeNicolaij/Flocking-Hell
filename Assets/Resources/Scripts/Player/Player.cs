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
    bool dead;

    int health, maxHealth;
    int moveSpeed = 2;
    int knockbackForce;
    int score;
    int money;

    float rotateOffset = 15;

    // Shooting
    bool canShootLeft = true, canShootRight = true;
    float shootDelayInSeconds = 0.25f;

    float shootSpeed = 200;


    void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("Aiming", true);
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
        transform.position += direction * Time.smoothDeltaTime * moveSpeed;
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
        Debug.Log(shootLeft);
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