using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatObject : MonoBehaviour
{
    float rotateSpeed;
    float amplitude = 0.5f;
    float frequency = 1;

    Vector3 startPosition;
    Vector3 tempPos;


    void Start()
    {
        int chance = Random.Range(0, 100);

        if (chance <= 60)
        {
            Destroy(this);      // Dont want them all to rotate
            return;
        }
        transform.eulerAngles = new Vector3(0, Random.Range(0, 360), 0);    // So that they not all act in the exact same way

        rotateSpeed = Random.Range(10, 25);         // *
        frequency = Random.Range(0.25f, 0.75f);

        // Positioning so it doesnt float through the ground
        Vector3 pos = transform.position;
        pos.y = 0.5f;
        transform.position = pos;

        startPosition = transform.position;
    }

    void Update()
    {
        // Rotate
        transform.Rotate(new Vector3(0, Time.deltaTime * rotateSpeed, 0));

        // Float up and down
        tempPos = startPosition;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        transform.position = tempPos;
    }
}
