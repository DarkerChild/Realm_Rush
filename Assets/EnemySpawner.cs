using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] EnemyMovement enemy1 = null;
    [SerializeField] float timeBetweenSpawns = 1f;
    [SerializeField] int noOfEnemies = 5;

    Pathfinder pathfinder;
    Waypoint startWaypoint;
    
    void Start()
    {
        pathfinder = FindObjectOfType<Pathfinder>();
        startWaypoint = pathfinder.GetStartWaypoint();
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        for (int i=0; i<noOfEnemies; i++)
        {
            Instantiate(enemy1, startWaypoint.transform.position, Quaternion.identity);
            enemy1.transform.parent = transform;
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }
}
