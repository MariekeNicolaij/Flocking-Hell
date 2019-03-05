using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    Player player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        Move();
        Shoot();
        FaceCursorDirection();
    }

    void Move()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            if (!player.animator.GetBool("Move"))
                player.animator.SetBool("Move", true);
            player.Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }
        else
        {
            if (player.animator.GetBool("Move"))
                player.animator.SetBool("Move", false);
        }
    }

    void Shoot()
    {
        //Debug.Log("Left " + Input.GetAxis("Shoot Left"));
        //Debug.Log("Right " + Input.GetAxis("Shoot Right"));
        if (Input.GetAxis("Shoot Left") != 0)
        {
            player.InitiateShootLeftPistol();
        }
        if (Input.GetAxis("Shoot Right") != 0)
        {
            player.InitiateShootRightPistol();
        }
    }

    void FaceCursorDirection()
    {
        if (Input.mousePosition != Vector3.zero)
        {
            Vector3 playerPositionScreenPoint = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 direction = Input.mousePosition - playerPositionScreenPoint;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            angle -= 90;    // Rotate correctly to mouse direction (It is 90 degrees off)

            transform.rotation = Quaternion.AngleAxis(-angle, Vector3.up);
        }
    }
}