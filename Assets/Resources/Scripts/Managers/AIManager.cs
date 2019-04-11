using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class AIManager : MonoBehaviour
{
    public static AIManager instance;

    public Terrain terrain;

    [HideInInspector]
    public List<List<GameObject>> aliveFlockingAI = new List<List<GameObject>>(); // Keep track of the ai groups 
    [HideInInspector]
    public List<GameObject> aliveNormalAI = new List<GameObject>();

    public List<GameObject> flockingAIPrefab;//, normalAIPrefab;
    public GameObject bossPrefab;
    bool bossSpawned;
    int bossWaveFrequency = 5;

    [HideInInspector]
    public List<Vector3> groupSpawnPoints;  // Spawn points for the group

    public Player player;
    public GameObject AIParent;

    [HideInInspector]
    public int flockGroupAmount;    // How many groups
    int groupMultiplier = 3;        // ^ + waveNumber / groupMultiplier = Flocking Hell 👀
    [HideInInspector]
    public int flockSize;          // AI per group

    public float minVelocity;       // Min speed
    public float maxVelocity;       // Max speed

    public Vector3[] flockCenter;       // Center of the flocking groups
    public Vector3[] flockVelocity;     // Average velocity of the flocking groups

    public int aliveAICount;

    public float difficultyMultiplier;


    void Start()
    {
        instance = this;

        if (!terrain)
        {
            terrain = GameObject.Find("Terrain").GetComponent<Terrain>();
        }
        if (!player)
        {
            player = GameObject.Find("Player").GetComponent<Player>();
        }
        if (flockingAIPrefab.Count == 0)// || normalAIPrefab.Count == 0)
        {
            Debug.LogError("No AI prefab(s) configured. (Please configure them both)");
            Destroy(this);
        }

        GetStats();

        GenerateSpawnPoints();
        SpawnFlockingAIGroups();
        Invoke("SetAIAliveTextForTheFirstTime", 0.1f);
    }

    // Yeah..
    void SetAIAliveTextForTheFirstTime()
    {
        UIManager.instance.UpdateAIAliveText(aliveAICount);
    }

    void GetStats()
    {
        difficultyMultiplier = StatsManager.instance.difficultyMultiplier;

        flockGroupAmount = StatsManager.instance.aiGroupSize + Mathf.RoundToInt(StatsManager.instance.wave / groupMultiplier);
        flockSize = Mathf.RoundToInt(StatsManager.instance.aiFlockSize + difficultyMultiplier);

        minVelocity = StatsManager.instance.aiMinVelocity;
        maxVelocity = StatsManager.instance.aiMaxVelocity;
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
                script.SetAI(true); // isFlock

                aliveAICount++;
                flockGroup.Add(ai);
            }

            aliveFlockingAI.Add(flockGroup);
        }
    }

    //public void SpawnNormalAI()
    //{
    //    aliveAICount++;
    //}

    public void SpawnBoss()
    {
        bossSpawned = true;
        UIManager.instance.ShowWavePanel("Boss spawned");

        GameObject ai = Instantiate(bossPrefab);
        AI script = ai.GetComponent<AI>();

        ai.transform.parent = AIParent.transform;
        ai.transform.position = new Vector3(50, 0, 50); // Center of the map

        script.SetAI(false, true); // No flock, is boss

        aliveAICount++;
        UIManager.instance.UpdateAIAliveText(aliveAICount);
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

        for (int i = 0; i < aliveFlockingAI.Count; i++)
        {
            Vector3 center = Vector3.zero;
            Vector3 velocity = Vector3.zero;
            foreach (GameObject ai in aliveFlockingAI[i])
            {
                center += ai.transform.position;
                velocity += ai.GetComponent<Rigidbody>().velocity;
            }

            flockCenter[i] = center / aliveFlockingAI[i].Count;
            flockVelocity[i] = velocity / aliveFlockingAI[i].Count;
        }
    }

    public void DestroyAI(GameObject ai, bool isFlock)
    {
        // When you shoot 2 bullets at the exact same time it might occur
        // that it wants to delete the same ai again.
        // Now that is not very handy
        AI script = ai.GetComponent<AI>();
        if (script.isBeingDestroyed)
            return;
        script.isBeingDestroyed = true;

        if (isFlock)
        {
            int groupIndex = script.groupIndex;
            aliveFlockingAI[groupIndex].Remove(ai);
        }
        else
        {
            aliveNormalAI.Remove(ai);
        }

        aliveAICount--;
        UIManager.instance.UpdateAIAliveText(aliveAICount);
        AudioManager.instance.PlaySound(GetComponentInChildren<AudioSource>(), Sounds.AIDies);

        Destroy(ai);

        if (aliveAICount <= 0)
        {
            if (StatsManager.instance.wave % bossWaveFrequency == 0 && !bossSpawned)
            {
                SpawnBoss();
                return; // Dont complete the wave yet
            }
            player.WaveComplete();
        }
    }
}