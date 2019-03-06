using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    public static EnvironmentManager instance;

    public Transform parent;
    public Terrain terrain;

    public List<GameObject> platformPrefabs;

    public Vector3 spawnStartPosition;

    float positionOffset = 10;
    float currentX = 0, currentZ = 0;
    float maxTerrainX, maxTerrainZ;

    void Start()
    {
        instance = this;

        if (!terrain)
        {
            terrain = GameObject.Find("Terrain").GetComponent<Terrain>();
            maxTerrainX = terrain.terrainData.size.x;
            maxTerrainZ = terrain.terrainData.size.z;
        }
        if (!parent)
        {
            parent = terrain.transform;
        }
        if (platformPrefabs.Count == 0 || !parent || !terrain)
        {
            Destroy(this);
            Debug.LogError("No platforms/parent/terrain selected!");
        }

        SpawnPlatforms();
    }

    void SpawnPlatforms()
    {
        for (int z = 0; z < maxTerrainZ; z += 10)
        {
            for (int x = 0; x < maxTerrainX; x += 10)
            {
                GameObject obj = Instantiate(platformPrefabs[Random.Range(0, platformPrefabs.Count)]);
                obj.transform.position = new Vector3(x, 0, z);
            }
        }
    }

    Vector3 GetNextPosition()
    {
        return Vector3.zero;
    }



    void Update()
    {

    }
}
