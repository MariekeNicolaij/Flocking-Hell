using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSight : MonoBehaviour
{
    public GameObject targetForward;
    LineRenderer laser;
    Color laserColor = new Color(100, 0, 255);

    float laserLength = 3;
    float laserWidth = 0.01f;


    void Start()
    {
        if (!targetForward)
            targetForward = GameObject.FindGameObjectWithTag("Player");
        SetLaser();
    }

    void SetLaser()
    {
        laser = gameObject.AddComponent<LineRenderer>();
        laser.material.color = laserColor;
        laser.widthMultiplier = laserWidth;

        laser.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
    }

    void Update()
    {
        Laser();
    }

    void Laser()
    {
        Vector3 direction = targetForward.transform.forward;
        direction.y = 0;

        laser.SetPositions(new Vector3[2] { transform.position, transform.position + (direction * laserLength) });
    }
}
