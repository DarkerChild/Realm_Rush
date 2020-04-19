using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] List<Waypoint> path = null;
    [SerializeField] float waitTime = 1f;


    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(FollowPath(waitTime));
    }

    IEnumerator FollowPath(float waitTime)
    {
        print("Starting Patrol");
        foreach (Waypoint waypoint in path)
        {
            print("Moving to position: " + waypoint.name);
            transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(waitTime);
        }
        print("Finished Partol");
    }
}
