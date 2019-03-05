using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform target;

    public Vector3 positionOffest = new Vector3(0, 3, -2);
    public Vector3 rotation = new Vector3(45, 0, 0);

    void Start()
    {
        if (!target)
            target = GameObject.FindGameObjectWithTag("Player").transform;
        transform.eulerAngles = rotation;
    }

    void Update()
    {
        transform.position = target.position + positionOffest;
    }
}
