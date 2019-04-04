using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleAnimation : MonoBehaviour
{
    public float maxDuration = 2;
    float duration = 0;

    Vector3 minSize = Vector3.one, maxSize = new Vector3(1.5f, 1.5f, 1.5f);
    bool turn;


    void Start()
    {
        InvokeRepeating("Turn", 0, maxDuration);
    }

    void Turn()
    {
        turn = !turn;
    }

    void Update()
    {
        if (turn)
            duration += Time.deltaTime;
        else
            duration -= Time.deltaTime;
        LerpSize();
    }

    void LerpSize()
    {
        Vector3 size = transform.localScale;
        transform.localScale = Vector3.Lerp(minSize, maxSize, duration / maxDuration);
    }
}
