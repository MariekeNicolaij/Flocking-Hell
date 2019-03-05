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
        if (Input.GetAxis("Shoot Left") > 0 || Input.GetButton("Shoot Left"))
        {
            player.InitiateShootLeftPistol();
        }
        if (Input.GetAxis("Shoot Right") > 0 || Input.GetButton("Shoot Right"))
        {
            player.InitiateShootRightPistol();
        }
    }

    void FaceCursorDirection()
    {
        float x = 0, y = 0, angle = 0;

        if (Input.mousePosition != Vector3.zero)
        {
            Vector3 playerPositionScreenPoint = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 direction = Input.mousePosition - playerPositionScreenPoint;
            x = direction.x;
            y = direction.y;
        }
        if (Input.GetAxis("Right Stick Horizontal") != 0 || Input.GetAxis("Right Stick Vertical") != 0)
        {
            x = Input.GetAxis("Right Stick Horizontal");
            y = -Input.GetAxis("Right Stick Vertical");
        }

        angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
        angle -= 90;    // Rotate correctly to mouse direction (It is 90 degrees off)
        transform.rotation = Quaternion.AngleAxis(-angle, Vector3.up);
    }
}