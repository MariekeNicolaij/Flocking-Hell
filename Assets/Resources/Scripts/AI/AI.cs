using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AI : MonoBehaviour
{
    StateManager stateManager;

    [HideInInspector]
    public Rigidbody rBody;

    public int groupIndex;  // Used if AI is flocking
    [HideInInspector]
    public float minVelocity;      // Min speed
    [HideInInspector]
    public float maxVelocity;      // Max speed

    // Health
    public int health;
    public float minDamage, maxDamage;

    [HideInInspector]
    public bool isBeingDestroyed;   // So it does not get 'destroyed twice' (because sometimes when you shoot with 2 bullets this would happen)


    public void SetAI(bool isFlock = false)
    {
        rBody = GetComponent<Rigidbody>();
        GetStats();

        stateManager = new StateManager(this, (isFlock) ? (State)new FlockState() : (State)new NormalState());
    }

    void GetStats()
    {
        health = StatsManager.instance.aiHealth;
        minDamage = StatsManager.instance.aiMinDamage;
        maxDamage = StatsManager.instance.aiMaxDamage;
        minVelocity = AIManager.instance.minVelocity;
        maxVelocity = AIManager.instance.maxVelocity;
    }

    void Update()
    {
        stateManager.Update();
    }

    void RotateTowardsDirection()
    {

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
