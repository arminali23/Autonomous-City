using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PedestrianAI : MonoBehaviour
{
    public Transform[] waypoints;
    private NavMeshAgent agent;
    private int currentWaypointIndex = 0;
    private bool hasStartedMoving = false;
    public float stoppingThreshold = 0.2f;
    public float stuckTimeLimit = 3f;
    private float stuckTimer = 0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = Random.Range(0.5f, 2.5f);
        agent.acceleration = Random.Range(4f,8f);
        agent.stoppingDistance = stoppingThreshold;
        agent.autoBraking = false;
        agent.avoidancePriority = Random.Range(0, 99);
        agent.radius = 0.3f;
        agent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;

        if (waypoints != null && waypoints.Length > 0)
        {
            StartCoroutine(DelayedStartMovement());
        }
        else
        {
            Debug.LogError($"{gameObject.name} has no assigned waypoints!");
        }
    }

    IEnumerator DelayedStartMovement()
    {
        yield return new WaitForSeconds(0.5f);
        hasStartedMoving = true;
        MoveToNextWaypoint();
    }

    void Update()
    {
        if (!hasStartedMoving || agent.pathPending)
        {
            return;
        }

        if (agent.velocity.magnitude < 0.1f && agent.remainingDistance > stoppingThreshold)
        {
            stuckTimer += Time.deltaTime;
            if (stuckTimer >= stuckTimeLimit)
            {
                Debug.Log($"{gameObject.name} stuck in crowd at {transform.position}");
                agent.ResetPath();
                if (Random.value > 0.5f)
                {
                    MoveToNextWaypoint();
                }
                else
                {
                    StartCoroutine(WaitAndRetry(1f));
                }
                stuckTimer = 0f;
            }
        }
        else
        {
            stuckTimer = 0f;
        }

        Collider[] nearbyAgents = Physics.OverlapSphere(transform.position, 1f, LayerMask.GetMask("Pedestrian"));
        foreach (Collider col in nearbyAgents)
        {
            if (col.gameObject != gameObject)
            {
                Vector3 awayDirection = (transform.position - col.transform.position).normalized;
                agent.Move(awayDirection * 0.1f);
            }
        }

        if (!agent.pathPending && agent.remainingDistance < stoppingThreshold)
        {
            MoveToNextWaypoint();
        }
    }

    void MoveToNextWaypoint()
    {
        if (waypoints.Length == 0) return;
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        agent.SetDestination(waypoints[currentWaypointIndex].position);
    }

    IEnumerator WaitAndRetry(float waitTime)
    {
        agent.isStopped = true;
        yield return new WaitForSeconds(waitTime);
        agent.isStopped = false;
        MoveToNextWaypoint();
    }

    public void SetWaypoints(Transform[] assignedWaypoints)
    {
        waypoints = assignedWaypoints;
        currentWaypointIndex = 0;
    }

}