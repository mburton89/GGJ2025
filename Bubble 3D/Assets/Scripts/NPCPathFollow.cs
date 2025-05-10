using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCPathFollow : MonoBehaviour
{
    public Transform[] waypoints;
    public float moveSpeed;
    public float reachThreshold;

    private int currentWaypointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (waypoints.Length == 0) return;

        MoveTowardsWaypoint(); 
    }

    private void MoveTowardsWaypoint()
    {
        Vector3 targetPosition = waypoints[currentWaypointIndex].position;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < reachThreshold)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }
}
