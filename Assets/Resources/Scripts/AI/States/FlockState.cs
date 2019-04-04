using System.Collections;
using UnityEngine;

public class FlockState : State
{
    public override void Enter()
    {
        // If I do owner.Invoke then it would search for the function inside the owner and not here :(
        owner.StartCoroutine(FlockBehaviour());
    }

    public override void Execute()
    {

    }

    public override void Exit()
    {

    }

    IEnumerator FlockBehaviour()
    {
        while (true)
        {
            owner.rBody.velocity = owner.rBody.velocity + Calc() * Time.deltaTime;

            // Speed
            float speed = owner.rBody.velocity.magnitude;
            if (speed > owner.maxVelocity)
            {
                owner.rBody.velocity = owner.rBody.velocity.normalized * owner.maxVelocity;
            }
            else if (speed < owner.minVelocity)
            {
                owner.rBody.velocity = owner.rBody.velocity.normalized * owner.minVelocity;
            }

            yield return new WaitForSeconds(Random.Range(0.2f, 0.5f)); // Delay
        }
    }

    // Calculates
    Vector3 Calc()
    {
        if (AIManager.instance.flockCenter.Length == 0 || AIManager.instance.flockVelocity.Length == 0)
            return Vector3.zero;
        Vector3 randomize = new Vector3((Random.value * 2) - 1, (Random.value * 2) - 1, (Random.value * 2) - 1);

        randomize.Normalize();
        Vector3 flockCenter = AIManager.instance.flockCenter[owner.groupIndex];
        Vector3 flockVelocity = AIManager.instance.flockVelocity[owner.groupIndex];
        Vector3 follow = AIManager.instance.player.transform.position;

        flockCenter -= owner.transform.position;
        flockVelocity -= owner.rBody.velocity;
        follow -= owner.transform.position;

        return (flockCenter + flockVelocity + follow * 2 + randomize);
    }
}
