using UnityEngine;
using UnityEngine.AI;

public class PedestrianSpawner : MonoBehaviour
{
    public GameObject[] pedestrianPrefabs;
    public int numberOfPedestrians = 5;
    public Transform[] spawnPoints;
    public Transform[] allWaypoints; // Waypoint listesi

    void Start()
    {
        for (int i = 0; i < numberOfPedestrians; i++)
        {
            SpawnPedestrian(i);
        }
    }
    void SpawnPedestrian(int index)
    {
        if (spawnPoints.Length == 0 || allWaypoints.Length < 4)
        {
            Debug.LogError("No spawn points or not enough waypoints available!");
            return;
        }

        Transform spawnPoint = spawnPoints[index % spawnPoints.Length];
        GameObject randomPedestrian = pedestrianPrefabs[Random.Range(0, pedestrianPrefabs.Length)];
        GameObject newPedestrian = Instantiate(randomPedestrian, spawnPoint.position, Quaternion.identity);

        NavMeshAgent agent = newPedestrian.GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.Warp(spawnPoint.position);
        }
        PedestrianAI pedestrianAI = newPedestrian.GetComponent<PedestrianAI>();
        if (pedestrianAI != null)
        {
            pedestrianAI.SetWaypoints(allWaypoints);
        }
    }
}
