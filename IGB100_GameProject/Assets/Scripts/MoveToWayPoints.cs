using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToWayPoints : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform[] waypoints;
    private int currentWaypointIndex = 0;
    private float distanceXZ = 0.0f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        // if no waypoint or reach the end point
        if (waypoints.Length == 0 || currentWaypointIndex >= waypoints.Length)
            return;
        else if (GetComponent<Enemy>().GetFiring() == false)
        {
            // move towards current waypoint
            agent.destination = waypoints[currentWaypointIndex].position;
            distanceXZ = (transform.position.x - waypoints[currentWaypointIndex].position.x) * (transform.position.x - waypoints[currentWaypointIndex].position.x) + (transform.position.z - waypoints[currentWaypointIndex].position.z) * (transform.position.z - waypoints[currentWaypointIndex].position.z);
            // if close enough, change current waypoint to the next one
            if (distanceXZ < 1.0f)
            {
                currentWaypointIndex++;
            }
        }
    }
    
}
