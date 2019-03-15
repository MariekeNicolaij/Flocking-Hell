using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSight : MonoBehaviour
{
    public GameObject targetForward;            // Because pistols arent straight and in this case want to use the players forward anyway
    LineRenderer laser;

    [HideInInspector]
    public float laserLength;
    float laserWidth = 0.02f;


    void Start()
    {
        if (!targetForward)
            targetForward = GameObject.FindGameObjectWithTag("Player");
        SetLaser();
    }

    void SetLaser()
    {
        laserLength = StatsManager.instance.laserLength;

        laser = gameObject.AddComponent<LineRenderer>();
        laser.material = (Material)Resources.Load("Materials/Laser");
        laser.widthMultiplier = laserWidth;

        laser.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
    }

    // Fixes linerender flickerbug when getting hit
    void LateUpdate()
    {
        Laser();

    }

    void Laser()
    {
        laser.SetPositions(new Vector3[2] { transform.position, GetEndPosition() });
    }

    Vector3 GetEndPosition()
    {
        RaycastHit hit;
        Vector3 direction = targetForward.transform.forward;
        direction.y = 0;

        if (Physics.Raycast(transform.position, direction, out hit, laserLength))
            if (hit.collider.gameObject.layer != Layer.Player && hit.collider.gameObject.layer != Layer.Bullet)
                return hit.point;

        return transform.position + (direction * laserLength);
    }
}
