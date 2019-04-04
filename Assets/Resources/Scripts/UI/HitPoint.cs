using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPoint : MonoBehaviour
{
    TextMesh textMesh;


    public void Initiate(Vector3 position, Color color, int hitpoints)
    {
        // Text
        textMesh = GetComponent<TextMesh>();
        textMesh.color = color;
        textMesh.text = ((hitpoints > 0 ) ? "-" : "+") + Mathf.Abs(hitpoints); // When regenerating health I pass a negative value so that in here it wouldput a + sign in front of it. Now it says +-'value'. Thats why Mathf.Abs

        // Position
        position.x += Random.Range(-1f, 1f); // Randomize it a bit
        transform.position = position;

        Invoke("DestroyThis", 3);
    }

    void Update()
    {
        Vector3 currentPosition = transform.position;
        currentPosition.y += 0.02f;
        transform.position = currentPosition;
    }

    void DestroyThis()
    {
        Destroy(gameObject);
    }
}
