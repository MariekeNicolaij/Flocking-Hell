using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class AIManager : MonoBehaviour
{
    public static AIManager instance;

    public Terrain terrain;

    [HideInInspector]
    public List<List<GameObject>> currentFlockingAIList = new List<List<GameObject>>(); // Keep track of the ai groups inside another list
    [HideInInspector]
    public List<GameObject> currentNormalAIList = new List<GameObject>();
    public List<GameObject> flockingAIPrefab, normalAIPrefab;

    [HideInInspector]
    public List<Vector3> groupSpawnPoints;

    public GameObject target;
    public GameObject AIParent;

    public int flockGroupAmount = 4;
    public int flockSize = 10;

    public float minVelocity = 5;
    public float maxVelocity = 20;
    public float randomness = 1;

    public Vector3[] flockCenter;       // Center of the flocking groups
    public Vector3[] flockVelocity;     // Average velocity of the flocking groups


    void Awake()
    {
        instance = this;

        if (!terrain)
        {
            terrain = GameObject.Find("Terrain").GetComponent<Terrain>();
        }
        if (!target)
        {
            target = GameObject.Find("Player");
        }
        if (flockingAIPrefab.Count == 0 || normalAIPrefab.Count == 0)
        {
            Debug.LogError("No AI prefab(s) configured. (Please configure them both)");
            Destroy(this);
        }

        GenerateSpawnPoints();
        SpawnFlockingAIGroups();
    }

    void GenerateSpawnPoints()
    {
        int[] start = { 5, 15 };
        int offset = 10;
        float standardY = 0.7f; // Bullet height

        for (int s = 0; s < start.Length; s++)
        {
            float maxTerrainX = terrain.terrainData.size.x - start[s], maxTerrainZ = terrain.terrainData.size.z - start[s]; // Want them to spawn in the middle of a ten by ten square
            // Point bottomleft to upleft
            for (int i = start[s]; i < maxTerrainZ; i += offset)
                groupSpawnPoints.Add(new Vector3(start[s], standardY, i));
            // Point upleft to upright
            for (int i = start[s]; i < maxTerrainX; i += offset)
                groupSpawnPoints.Add(new Vector3(i, standardY, maxTerrainZ));
            // Point upright to bottomright
            for (int i = (int)maxTerrainZ; i > start[s]; i -= offset)
                groupSpawnPoints.Add(new Vector3(maxTerrainX, standardY, i));
            // Point bottomright to bottomleft
            for (int i = (int)maxTerrainX; i > start[s]; i -= offset)
                groupSpawnPoints.Add(new Vector3(i, standardY, start[s]));
        }
    }

    int RandomIndex(int max)
    {
        return Random.Range(0, max);
    }

    public void SpawnFlockingAIGroups()
    {
        for (int i = 0; i < flockGroupAmount; i++)
        {
            if (groupSpawnPoints.Count == 0)
                return;

            List<GameObject> flockGroup = new List<GameObject>();
            GameObject prefab = flockingAIPrefab[RandomIndex(flockingAIPrefab.Count)];
            Vector3 groupSpawnPosition = groupSpawnPoints[RandomIndex(groupSpawnPoints.Count)];

            groupSpawnPoints.Remove(groupSpawnPosition);    // Do not want to use this one again

            for (int j = 0; j < flockSize; j++)
            {
                GameObject ai = Instantiate(prefab);
                AI script = ai.GetComponent<AI>();

                ai.transform.parent = AIParent.transform;
                ai.transform.position = groupSpawnPosition;

                script.groupIndex = i;
                script.SetController();

                flockGroup.Add(ai);
            }

            currentFlockingAIList.Add(flockGroup);
        }
    }

    void Update()
    {
        UpdateFlockCenterAndVelocity();
    }

    void UpdateFlockCenterAndVelocity()
    {
        if (flockCenter.Length == 0)
            flockCenter = new Vector3[flockGroupAmount];
        if (flockVelocity.Length == 0)
            flockVelocity = new Vector3[flockGroupAmount];

        for (int i = 0; i < flockGroupAmount; i++)
        {
            Vector3 center = Vector3.zero;
            Vector3 velocity = Vector3.zero;
            foreach (GameObject ai in currentFlockingAIList[i])
            {
                center += ai.transform.position;
                velocity += ai.GetComponent<Rigidbody>().velocity;
            }

            flockCenter[i] = center / flockSize;
            flockVelocity[i] = velocity / flockSize;
        }
    }
}