using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalState : State
{
    Transform player;


    public override void Enter()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void Execute()
    {
        // Follow player
        owner.transform.position = Vector3.MoveTowards(owner.transform.position, player.position, owner.maxVelocity * Time.deltaTime);
    }

    public override void Exit()
    {

    }
}
