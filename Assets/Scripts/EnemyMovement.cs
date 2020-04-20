using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float waitTime = 1f;

    
    Waypoint startWaypoint;
    Waypoint endWaypoint;
    Waypoint currentWaypoint;

    Pathfinder pathfinder;


    void Start()
    {
        pathfinder = FindObjectOfType<Pathfinder>();
        startWaypoint = pathfinder.GetStartWaypoint();
        currentWaypoint = startWaypoint;
        endWaypoint = pathfinder.GetEndWaypoint();
        StartCoroutine(FollowPath(waitTime));
    }

    IEnumerator FollowPath(float waitTime)
    {
        while (transform.position != endWaypoint.transform.position)
        {
            List<Waypoint> path = pathfinder.GetPath();

            foreach (Waypoint nextWaypoint in path)
            {
                yield return new WaitForSeconds(waitTime);
                transform.position = nextWaypoint.transform.position;
            }
        }
    }
}
