using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    float moveTime;
    float waitTime;

    Waypoint currentWaypoint;
    Waypoint nextWaypoint;
    Waypoint endWaypoint;

    Pathfinder pathfinder;

    bool needsToMove = false;


    void Start()
    {
        pathfinder = FindObjectOfType<Pathfinder>();
        currentWaypoint = pathfinder.GetStartWaypoint();
        nextWaypoint = currentWaypoint;
        endWaypoint = pathfinder.GetEndWaypoint();
        StartCoroutine(FollowPath());
    }

    private void Update()
    {
        MoveToNextWaypoint();
    }

    IEnumerator FollowPath()
    {
        while (currentWaypoint != endWaypoint)
        {
            List<Waypoint> path = pathfinder.GetPath();

            foreach (Waypoint waypoint in path)
            {
                nextWaypoint = waypoint;
                needsToMove = true;
                yield return new WaitForSeconds(waitTime+moveTime);
                currentWaypoint = waypoint;
               
            }
        }
        GetComponent<EnemyCombat>().StartDamagingEnemyBase();
    }

    private void MoveToNextWaypoint()
    {
        if (needsToMove)
        {
            needsToMove = false;
            StartCoroutine(MoveToNextWaypoint(currentWaypoint, nextWaypoint, moveTime));
        }
    }

    IEnumerator MoveToNextWaypoint(Waypoint old, Waypoint next, float duration)
    {
        for (float t = 0f; t < duration; t +=Time.deltaTime)
        {
            transform.position = Vector3.Lerp(
                old.transform.position,
                next.transform.position,
                t / duration
                );
            yield return 0;
        }
        transform.position = next.transform.position;
        currentWaypoint = next;
    }

    public void SetEnemyVariables(float spawnTime, float moveWaitRatio)
    {
        if (spawnTime > 2f)
        {
            moveTime = spawnTime * moveWaitRatio;
            waitTime = spawnTime * (1 - moveWaitRatio);
        }
        else
        {
            moveTime = spawnTime;
            waitTime = 0f;
        }
    }
}
