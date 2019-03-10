using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AI : MonoBehaviour
{
    public List<GameObject> droneBlades;
    Rigidbody rBody;

    public int groupIndex;
    float minVelocity;
    float maxVelocity;


    public void SetController()
    {
        rBody = GetComponent<Rigidbody>();
        minVelocity = AIManager.instance.minVelocity;
        maxVelocity = AIManager.instance.maxVelocity;

        StartCoroutine("BoidSteering");
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

            //Vector3 p = transform.position;
            //p.y = 0.7f;
            //transform.position = p;
            float waitTime = Random.Range(0.2f, 0.5f);
            yield return new WaitForSeconds(waitTime);
        }
    }

    private Vector3 Calc()
    {
        if (AIManager.instance.flockCenter.Length == 0 || AIManager.instance.flockVelocity.Length == 0)
            return Vector3.zero;
        Vector3 randomize = new Vector3((Random.value * 2) - 1, (Random.value * 2) - 1, (Random.value * 2) - 1);

        randomize.Normalize();
        Vector3 flockCenter = AIManager.instance.flockCenter[groupIndex];
        Vector3 flockVelocity = AIManager.instance.flockVelocity[groupIndex];
        Vector3 follow = AIManager.instance.target.transform.position;

        flockCenter -= transform.position;
        flockVelocity -= rBody.velocity;
        follow -= transform.position;

        return (flockCenter + flockVelocity + follow * 2 + randomize);
    }
}
