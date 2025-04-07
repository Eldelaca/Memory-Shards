using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    // Ref
    private NavMeshAgent navAgent;
    public Transform[] waypoints; // List of Waypoints
    private int currentWaypoint = 0;
    public Transform targetObject; // Player Object
    public Animator anim;

    // Idle State Timer
    private float idleTimer = 0f;
    private bool isIdle = false;

    public PlayerMovement playerMovement;

    // Instantiate Waypoint Prefab 
    public GameObject playerWaypointPrefab;
    private GameObject playerWaypoint = null;

    // Sight var
    public float fieldOfViewAngle = 120.0f;  // Field of View (in degrees)
    private bool isVisible = false;

    // Prox var
    public float proximityRange = 15.0f;

    // Flee state
    public bool isFleeing = false;
    public float fleeTime = 15f; // Time to flee
    public float fleeTimer = 0f; // Timer to track fleeing duration

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>(); // Grabbing NavMesh Agent Component
        PatrolState(); // Default State on Start
    }

    void Update()
    {
        // If the enemy is fleeing, handle flee state
        if (isFleeing)
        {
            FleeState();
        }
        else
        {
            // Check List
            isPlayerVisible(); // Visibility Function
            isPlayerClose(); // Proxy Function

            if (isVisible)
            {
                ChaseState();
            }
            else
            {
                if (playerWaypoint != null)
                {
                    SeekState(); // Move to last known position
                }
                else
                {
                    if (isIdle)
                    {
                        IdleState();
                    }
                    else
                    {
                        PatrolState(); // Resume Default State 
                    }
                }
            }
        }

        // Check the player's action
        if (playerMovement.IsFlashPressed())
        {
            FlashWaypoint();
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
    }

    public void TakeFlashEffect()
    {
        // Trigger Flee state
        isFleeing = true;
        fleeTimer = fleeTime; // Reset flee timer
        Debug.Log("Enemy hit by flash, fleeing!");
    }

    // ***** States *****

    // Chase State
    void ChaseState()
    {
        if (playerWaypoint != null)
        {
            // Move towards the last known player position
            navAgent.SetDestination(playerWaypoint.transform.position);
            Destroy(playerWaypoint);
            playerWaypoint = null;
        }
        else
        {
            // If the player waypoint doesn't exist, go to the player itself
            navAgent.SetDestination(targetObject.transform.position);
        }
    }

    // Patrol State (Default)
    void PatrolState()
    {
        if (waypoints.Length > 0)
        {
            navAgent.SetDestination(waypoints[currentWaypoint].position);

            // Pick a random waypoint
            if (!navAgent.pathPending && navAgent.remainingDistance < 0.5f)
            {
                isIdle = true;
                idleTimer = Random.Range(3f, 7f);
                currentWaypoint = Random.Range(0, waypoints.Length);
            }
        }
    }

    // Idle State
    void IdleState()
    {
        idleTimer -= Time.deltaTime;
        if (idleTimer <= 0f)
        {
            isIdle = false;
            PatrolState(); // Ensures the patrol starts after idling time ends
        }
    }

    // Seek State
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

    // Flee State
    void FleeState()
    {
        fleeTimer -= Time.deltaTime; // Decrease timer

        if (fleeTimer <= 0f)
        {
            isFleeing = false; // End flee state timer
            PatrolState(); // Immediately return to patrolling instead of going to a waypoint
        }
        else
        {
            Vector3 fleeDirection = transform.position - targetObject.position; // Direction opposite of player
            navAgent.SetDestination(transform.position + fleeDirection.normalized * 10f); // Flee away
        }
    }

    // ***** Boolean Checks *****

    // Checks if the player is within the Vision of the Enemy
    void isPlayerVisible()
    {
        // If the enemy is fleeing, ignore visibility checks and ensure the enemy can't see the player
        if (isFleeing)
        {
            isVisible = false; // Ignore visibility when fleeing
        }
        else
        {
            Vector3 direction = targetObject.position - transform.position;

            float angle = Vector3.Angle(direction, transform.forward);

            // Using a Raycast to figure out if player is in line of sight (POV)
            if (angle < fieldOfViewAngle * 0.5f)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, direction, out hit))
                {
                    // If ray hits the player (targetObject), it means the player is visible
                    if (hit.transform == targetObject)
                    {
                        isVisible = true;
                    }
                    else
                    {
                        isVisible = false;
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
                
            }
        }
    }

    // Checks the Proximity of Player and Enemy (Distance)
    void isPlayerClose()
    {
        float distance = Vector3.Distance(transform.position, targetObject.position);

        if (distance < 15.0f)
        {
            // IF player is running and not crouching
            if (anim.GetBool("runBool") && !anim.GetBool("crouchBool"))
            {
                ChaseState();
            }
             
        }
    }
}
