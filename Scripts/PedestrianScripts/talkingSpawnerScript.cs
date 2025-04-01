using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class talkingSpawnerScript : MonoBehaviour
{
    public GameObject[] talkingPedestrianPrefabs; // Array of different talking pedestrian prefabs
    public int numberOfGroups = 3; // Number of groups to spawn
    public Transform[] spawnPoints; // Locations where groups can spawn
    public int minGroupSize = 2;
    public int maxGroupSize = 5;

    void Start()
    {
        for (int i = 0; i < numberOfGroups; i++)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            int groupSize = Random.Range(minGroupSize, maxGroupSize + 1);

            SpawnGroup(spawnPoint.position, groupSize);
        }
    }

    void SpawnGroup(Vector3 position, int groupSize)
    {
        float spacing = 1.5f; // Distance between each character in the group

        for (int i = 0; i < groupSize; i++)
        {
            Vector3 spawnOffset = new Vector3(Random.Range(-spacing, spacing), 0, Random.Range(-spacing, spacing));

            // Pick a random talking pedestrian prefab
            GameObject randomPedestrianPrefab = talkingPedestrianPrefabs[Random.Range(0, talkingPedestrianPrefabs.Length)];

            Instantiate(randomPedestrianPrefab, position + spawnOffset, Quaternion.identity);
        }
    }
}
