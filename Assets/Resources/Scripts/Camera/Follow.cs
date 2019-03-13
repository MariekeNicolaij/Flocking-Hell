using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform target;

    public Vector3 positionOffset;
    public Vector3 rotation = new Vector3(45, 0, 0);

    public float rumbleTime = 0;
    float rumbleAmount = 2;

    void Start()
    {
        if (!target)
            target = GameObject.FindGameObjectWithTag("Player").transform;

        if (transform.parent)
            transform.SetParent(null);

        positionOffset = new Vector3(0, StatsManager.instance.cameraZoom, -StatsManager.instance.cameraZoom);
        transform.eulerAngles = rotation;
    }

    void Update()
    {
        transform.position = target.position + positionOffset;

        if (rumbleTime > 0)
        {
            Camera.main.transform.position += Random.insideUnitSphere * rumbleAmount* Time.deltaTime;
            rumbleTime -= Time.deltaTime;
        }
        if (rumbleTime > 2)
            rumbleTime = 2;
    }
}
