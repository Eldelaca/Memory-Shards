using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent navAgent;
    public Transform[] waypoints; // Array of waypoints
    private int currentWaypoint = 0; // Current waypoint index

    // Timer variables for idle time after reaching a waypoint
    private float idleTimer = 0f; // Timer to track the idle time
    private bool isIdle = false; // Flag to track if the enemy is idling

    public PlayerMovement playerMovement; // Reference to PlayerMovement script
    public GameObject playerWaypointPrefab; // Prefab for the temporary waypoint

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>(); // Get the NavMeshAgent component
        PatrolState(); // Start patrolling to the first waypoint
    }

    void Update()
    {
        if (isIdle)
        {
            IdleState();
        }
        else
        {
            PatrolState(); // Continue patrolling when not seeking or idling
        }
    }

    // PatrolState: Moves between waypoints
    void PatrolState()
    {
        // Check if there are waypoints
        if (waypoints.Length == 0) return;

        // Set the destination to the current waypoint
        navAgent.SetDestination(waypoints[currentWaypoint].position);

        // If the enemy has reached the current waypoint, start idling
        if (!navAgent.pathPending && navAgent.remainingDistance < 0.5f)
        {
            // Start idling with a random time between 2 and 5 seconds
            isIdle = true;
            idleTimer = Random.Range(2f, 5f); // Random idle time between 2 and 5 seconds
            Debug.Log("Idling at waypoint " + currentWaypoint + " for " + idleTimer + " seconds");

            // Increment to the next waypoint after idle
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
        }
    }

    // IdleState: Waits for the idle time before resuming patrol
    void IdleState()
    {
        // Update the idle timer
        idleTimer -= Time.deltaTime;

        // Once idle time is over, resume patrolling
        if (idleTimer <= 0f)
        {
            isIdle = false; // Reset the idle state
            Debug.Log("Resuming patrol");
        }
    }
}
