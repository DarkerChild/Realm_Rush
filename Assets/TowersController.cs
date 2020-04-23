using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowersController : MonoBehaviour
{
    [SerializeField] Tower tower1 = null;
    [Range(1,10)] [SerializeField] int maxTowers = 3;

    [SerializeField] Queue<Tower> towerQueue;

    void Start()
    {
        towerQueue = new Queue<Tower>(maxTowers);
    }

    public Tower GetTower1()
    {
        return tower1;
    }

    public void PlaceTowerHere(Waypoint waypoint)
    {
        waypoint.isPlaceable = false;
        print("Current Queue Size = " + towerQueue.Count);
        print("Max Towers = " + maxTowers);

        if (towerQueue.Count == maxTowers)
        {
            Tower currentTower = towerQueue.Dequeue();
            currentTower.currentWaypoint.isPlaceable=true;
            currentTower.transform.position = waypoint.GetTowerSpawnPosition();
            towerQueue.Enqueue(currentTower);
        }
        else
        {
            Vector3 towerSpawnPosition = waypoint.GetTowerSpawnPosition();
            Tower newTower = Instantiate(tower1, towerSpawnPosition, Quaternion.identity, transform);
            newTower.currentWaypoint = waypoint;
            towerQueue.Enqueue(newTower);
        }
    }
}
