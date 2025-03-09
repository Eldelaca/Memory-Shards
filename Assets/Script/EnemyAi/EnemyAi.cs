using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent navAgent;
    public Transform[] waypoints; // Array of waypoints
    private int currentWaypoint = 0; // Current waypoint index

    private float idleTimer = 0f;
    private bool isIdle = false;

    public PlayerMovement playerMovement; // Reference to player script
    public GameObject playerWaypointPrefab; // Prefab for flash waypoint
    private GameObject playerWaypoint = null; // Stores the active player waypoint

    // Sight variables
    public float fieldOfViewAngle = 90.0f;  // Field of View (degrees)

    public Transform targetObject; // Target (player) reference
    private bool isVisible = false;

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        PatrolState();
    }

    void Update()
    {
        // Check if player is within line of sight and visible
        isPlayerVisible();

        // Decision making based on perception
        if (isVisible)
        {
            SeekState(); // Seek player if visible
        }
        else
        {
            if (playerWaypoint == null)
            {
                PatrolState(); // Patrol if not visible
            }
        }

        // If the player flashes, create a waypoint
        if (playerMovement.IsFlashPressed())
        {
            FlashWaypoint();
        }

        if (playerWaypoint != null)
        {
            SeekState();
        }
    }

    void PatrolState()
    {
        if (waypoints.Length > 0)
        {
            navAgent.SetDestination(waypoints[currentWaypoint].position);

            // Pick a random waypoint
            if (!navAgent.pathPending && navAgent.remainingDistance < 0.5f)
            {
                isIdle = true;
                idleTimer = Random.Range(2f, 5f);
                currentWaypoint = Random.Range(0, waypoints.Length);
            }
        }

    }

    void IdleState()
    {
        idleTimer -= Time.deltaTime;
        if (idleTimer <= 0f)
        {
            isIdle = false;
        }
    }

    void FlashWaypoint()
    {
        // Delete the old waypoint if it exists
        if (playerWaypoint != null)
        {
            Destroy(playerWaypoint);
        }

        // Instantiate a new playerWaypoint at the player's position
        playerWaypoint = Instantiate(playerWaypointPrefab, playerMovement.transform.position, Quaternion.identity);

        // Immediately move to the new waypoint
        SeekState();
    }

    void SeekState()
    {
        if (playerWaypoint != null)
        {
            navAgent.SetDestination(playerWaypoint.transform.position);
        }

        // Check if the enemy has reached the player waypoint
        if (!navAgent.pathPending && navAgent.remainingDistance < 0.5f)
        {
            Destroy(playerWaypoint); // Remove the waypoint
            playerWaypoint = null; // Clear reference
        }
    }

    // Checks if Player is visible using Raycast (instead of range)
    void isPlayerVisible()
    {
        Vector3 direction = targetObject.position - transform.position;

        // Calculate angle between forward direction and direction to player
        float angle = Vector3.Angle(direction, transform.forward);

        // If player is within the field of view angle, check for obstacles
        if (angle < fieldOfViewAngle * 0.5f)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction, out hit))
            {
                // If ray hits the player (targetObject), it means the player is visible
                if (hit.transform == targetObject)
                {
                    isVisible = true;
                    Debug.Log("Player is visible!");
                }
                else
                {
                    isVisible = false;
                    Debug.Log("Player is not visible!");
                }
            }
            else
            {
                isVisible = false;
            } 
        }
        else
        {
            isVisible = false;
            Debug.Log("Player is out of field of view");
        }
    }

    // Commenting out isClose() and similar functions
    // void isPlayerClose()
    // {
    //     // Optionally, check if player is within a certain proximity (e.g., 5 units)
    //     if (Vector3.Distance(transform.position, targetObject.position) < 5.0f)
    //     {
    //         isClose = true;
    //     }
    //     else
    //     {
    //         isClose = false;
    //     }
    // }
}
