using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Normal : MonoBehaviour
{
    Rigidbody rBody;

    //public int groupIndex;
    //float minVelocity;
    //float maxVelocity;

    public int health;
    public float minDamage, maxDamage;

    [HideInInspector]
    public bool isBeingDestroyed;


    public void Start()
    {
        rBody = GetComponent<Rigidbody>();

        GetStats();
    }

    void GetStats()
    {
        health = StatsManager.instance.aiHealth;
        minDamage = StatsManager.instance.aiMinDamage * 2; // Double
        maxDamage = StatsManager.instance.aiMaxDamage * 2; // damage
        //minVelocity = AIManager.instance.minVelocity;
        //maxVelocity = AIManager.instance.maxVelocity;
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
