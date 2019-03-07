using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class AIManager : MonoBehaviour
{
    public static AIManager instance;

    public Terrain terrain;

    [HideInInspector]
    public List<GameObject> currentFlockingAIList, currentNormalAIList;
    public List<GameObject> flockingAIPrefab, normalAIPrefab;
    public List<Vector3> spawnPoints;

    public int flockSize = 10;

    public float minVelocity = 5;
    public float maxVelocity = 20;
    public float randomness = 1;
    public GameObject chasee;
    public GameObject AIParent;

    public Vector3 flockCenter;
    public Vector3 flockVelocity;


    void Awake()
    {
        instance = this;

        if (flockingAIPrefab.Count == 0 || normalAIPrefab.Count == 0)
            Debug.LogError("No AI prefab(s) configured. (Please configure them both)");

        //if(spawnPoints.Count == 0)
        //    Debug.LogError("No spawnpoints selected!");

        if (!terrain)
            terrain = GameObject.Find("Terrain").GetComponent<Terrain>();

        SpawnFlockingAIGroup();
    }

    int RandomIndex(int max)
    {
        return Random.Range(0, max);
    }

    public void SpawnFlockingAIGroup()
    {
        GameObject prefab = flockingAIPrefab[RandomIndex(flockingAIPrefab.Count)];
        //Vector3 spawnPosition = spawnPoints[RandomIndex(spawnPoints.Count)];
        for (int i = 0; i < flockSize; i++)
        {
            GameObject ai = Instantiate(prefab, transform.position, transform.rotation);
            Vector3 position = new Vector3(50, 1, 50);

            ai.transform.parent = AIParent.transform;
            ai.transform.localPosition = position;
            ai.GetComponent<AI>().SetController(gameObject);
            currentFlockingAIList.Add(ai);
        }
    }

    void Update()
    {
        Vector3 theCenter = Vector3.zero;
        Vector3 theVelocity = Vector3.zero;

        foreach (GameObject ai in currentFlockingAIList)
        {
            theCenter = theCenter + ai.transform.localPosition;
            theVelocity = theVelocity + ai.GetComponent<Rigidbody>().velocity;
        }

        flockCenter = theCenter / (flockSize);
        flockVelocity = theVelocity / (flockSize);
    }
}