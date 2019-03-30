using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnvironmentManager : MonoBehaviour
{
    public static EnvironmentManager instance;

    public Transform platformParent, wallParent, fireParent;
    public Terrain terrain;

    public GameObject wallPrefab;
    public List<GameObject> platformPrefabs;

    public GameObject firePrefab;
    int fireSpawnChance = 25; //%

    Vector3 spawnStartPosition;

    int chunkOffset = 10;
    float maxTerrainX, maxTerrainZ;

    void Start()
    {
        instance = this;

        if (!terrain)
            terrain = GameObject.Find("Terrain").GetComponent<Terrain>();
        if (!platformParent)
            platformParent = transform;
        if (!wallParent)
            wallParent = transform;
        if (!fireParent)
            fireParent = transform;
        if (platformPrefabs.Count == 0)
        {
            Debug.LogError("No platforms/parent/terrain selected!");
            Destroy(this);
        }

        maxTerrainX = terrain.terrainData.size.x;
        maxTerrainZ = terrain.terrainData.size.z;

        SpawnWalls();
        SpawnPlatforms();
    }

    void SpawnWalls()
    {
        // Wall bottomleft to upleft
        for (int i = chunkOffset; i <= maxTerrainZ; i += chunkOffset)
            SpawnWall(new Vector3(0, 0, i), 90);
        // Wall upleft to upright
        for (int i = 0; i < maxTerrainX; i += chunkOffset)
            SpawnWall(new Vector3(i, 0, maxTerrainZ));
        // Wall upright to bottomright
        for (int i = (int)maxTerrainZ; i > 0; i -= chunkOffset)
            SpawnWall(new Vector3(maxTerrainX, 0, i), 90);
        // Wall bottomright to bottomleft
        for (int i = (int)maxTerrainX; i > -chunkOffset; i -= chunkOffset)
            SpawnWall(new Vector3(i, 0, 0));
    }

    void SpawnWall(Vector3 position, float yRotation = 0)
    {
        GameObject wall = Instantiate(wallPrefab);
        wall.transform.SetParent(wallParent);
        wall.transform.position = position;
        wall.transform.eulerAngles = new Vector3(0, yRotation, 0);
    }

    void SpawnPlatforms()
    {
        for (int z = 0; z < maxTerrainZ; z += chunkOffset)
        {
            for (int x = 0; x < maxTerrainX; x += chunkOffset)
            {
                GameObject platform = Instantiate(platformPrefabs[Random.Range(0, platformPrefabs.Count)]);
                platform.transform.SetParent(platformParent);
                platform.transform.position = new Vector3(x, 0, z);

                int magicNumber = Random.Range(1, 100);

                if (magicNumber <= fireSpawnChance)
                    SpawnFire(platform.transform.position);
            }
        }

    }

    void SpawnFire(Vector3 platformPosition)
    {
        Vector3 randomPosition = new Vector3(
            platformPosition.x + Random.Range(0, chunkOffset),
            0,
            platformPosition.z + Random.Range(0, chunkOffset)
            );

        GameObject fire = Instantiate(firePrefab);
        fire.transform.SetParent(fireParent);
        fire.transform.position = randomPosition;
    }
}
