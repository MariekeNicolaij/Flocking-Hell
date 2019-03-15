using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Flock : MonoBehaviour
{
    Rigidbody rBody;

    public int groupIndex;
    float minVelocity;
    float maxVelocity;

    public int health;
    public float minDamage, maxDamage;

    [HideInInspector]
    public bool isBeingDestroyed;


    public void SetController()
    {
        rBody = GetComponent<Rigidbody>();

        GetStats();

        StartCoroutine("BoidSteering");
    }

    void GetStats()
    {
        health = StatsManager.instance.aiHealth;
        minDamage = StatsManager.instance.aiMinDamage;
        maxDamage = StatsManager.instance.aiMaxDamage;
        minVelocity = AIManager.instance.minVelocity;
        maxVelocity = AIManager.instance.maxVelocity;
    }

    IEnumerator BoidSteering()
    {
        while (true)
        {
            rBody.velocity = rBody.velocity + Calc() * Time.deltaTime;

            // enforce minimum and maximum speeds for the boids
            float speed = GetComponent<Rigidbody>().velocity.magnitude;
            if (speed > maxVelocity)
            {
                rBody.velocity = rBody.velocity.normalized * maxVelocity;
            }
            else if (speed < minVelocity)
            {
                rBody.velocity = rBody.velocity.normalized * minVelocity;
            }

            float waitTime = Random.Range(0.2f, 0.5f);
            yield return new WaitForSeconds(waitTime);
        }
    }

    /// <summary>
    /// Calculates
    /// </summary>
    /// <returns></returns>
    Vector3 Calc()
    {
        if (AIManager.instance.flockCenter.Length == 0 || AIManager.instance.flockVelocity.Length == 0)
            return Vector3.zero;
        Vector3 randomize = new Vector3((Random.value * 2) - 1, (Random.value * 2) - 1, (Random.value * 2) - 1);

        randomize.Normalize();
        Vector3 flockCenter = AIManager.instance.flockCenter[groupIndex];
        Vector3 flockVelocity = AIManager.instance.flockVelocity[groupIndex];
        Vector3 follow = AIManager.instance.player.transform.position;

        flockCenter -= transform.position;
        flockVelocity -= rBody.velocity;
        follow -= transform.position;

        return (flockCenter + flockVelocity + follow * 2 + randomize);
    }

    /// <summary>
    /// Does random calculated damage based on min and max value
    /// </summary>
    /// <returns></returns>
    public int Damage()
    {
        return Mathf.RoundToInt(Random.Range(minDamage, maxDamage));
    }
}
