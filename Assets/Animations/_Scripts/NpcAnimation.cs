using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public Transform[] waypoints; // Assign waypoints in Inspector
    private int currentWaypointIndex = 0;
    private bool shouldMove = false;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            MoveToWaypoint();
            StartMoving();
        }
    }

    public void StartMoving()
    {
        shouldMove = true;
        animator.SetFloat("vAxisInput", 1.0f); // Set walking input to forward
    }

    void MoveToWaypoint()
    {
        if (currentWaypointIndex < waypoints.Length)
        {
            Transform target = waypoints[currentWaypointIndex];
            Vector3 direction = (target.position - transform.position).normalized;
            direction.y = 0; // Prevent tilting

            // Rotate the NPC to face the next waypoint
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 5f);

            // Check if NPC has reached the waypoint
            float distance = Vector3.Distance(transform.position, target.position);
            if (distance < 0.5f)
            {
                currentWaypointIndex++;
                if (currentWaypointIndex >= waypoints.Length)
                {
                    shouldMove = false;
                    animator.SetFloat("vAxisInput", 0.0f); // Stop walking at the final point
                }
            }
        }
    }
}
