using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform target;

    public Vector3 positionOffest = new Vector3(0, 4, -3);
    public Vector3 rotation = new Vector3(45, 0, 0);

    public float rumbleTime = 0;
    float rumbleAmount = 2;

    void Start()
    {
        if (!target)
            target = GameObject.FindGameObjectWithTag("Player").transform;
        transform.eulerAngles = rotation;
    }

    void Update()
    {
        transform.position = target.position + positionOffest;

        if (rumbleTime > 0)
        {
            Debug.Log("Rumble");
            Camera.main.transform.position += Random.insideUnitSphere * rumbleAmount* Time.deltaTime;
            rumbleTime -= Time.deltaTime;
        }
        if (rumbleTime > 2)
            rumbleTime = 2;
    }
}
