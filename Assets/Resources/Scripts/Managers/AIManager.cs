using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class AIManager : MonoBehaviour
{
    //public static AIManager instance;

    //public Terrain terrain;

    //[HideInInspector]
    //public List<GameObject> currentFlockingAIList, currentNormalAIList;
    //public List<GameObject> flockingAIPrefab, normalAIPrefab;
    //public List<Vector3> spawnPoints;

    //public int flockSize = 10;


    //void Awake()
    //{
    //    instance = this;

    //    if (flockingAIPrefab.Count == 0 || normalAIPrefab.Count == 0)
    //        Debug.LogError("No AI prefab(s) configured. (Please configure them both)");

    //    if (!terrain)
    //        terrain = GameObject.Find("Terrain").GetComponent<Terrain>();
    //}

    //int RandomIndex(int max)
    //{
    //    return Random.Range(0, max);
    //}

    //public void SpawnFlockingAIGroup()
    //{
    //    GameObject prefab = flockingAIPrefab[RandomIndex(flockingAIPrefab.Count)];
    //    Vector3 spawnPosition = spawnPoints[RandomIndex(spawnPoints.Count)];
    //    for (int i = 0; i < flockSize; i++)
    //    {
    //        GameObject ai = Instantiate(prefab);
    //    }
    //}

    public GameObject boidPrefab;

    public int spawnCount = 10;

    public float spawnRadius = 4.0f;

    [Range(0.1f, 20.0f)]
    public float velocity = 6.0f;

    [Range(0.0f, 0.9f)]
    public float velocityVariation = 0.5f;

    [Range(0.1f, 20.0f)]
    public float rotationCoeff = 4.0f;

    [Range(0.1f, 10.0f)]
    public float neighborDist = 2.0f;

    public LayerMask searchLayer;

    void Start()
    {
        for (var i = 0; i < spawnCount; i++) Spawn();
    }

    public GameObject Spawn()
    {
        return Spawn(transform.position + Random.insideUnitSphere * spawnRadius);
    }

    public GameObject Spawn(Vector3 position)
    {
        var rotation = Quaternion.Slerp(transform.rotation, Random.rotation, 0.3f);
        var boid = Instantiate(boidPrefab, position, rotation) as GameObject;
        boid.GetComponent<AI>().controller = this;
        return boid;
    }
}