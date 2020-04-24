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

    private void Update()
    {
        CheckTowerPlacement();
    }

    private void CheckTowerPlacement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = FindObjectOfType<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Waypoint targetWaypoint = hit.transform.GetComponent<Waypoint>();

                if (targetWaypoint != null)
                {
                    print("Waypoint = " + targetWaypoint.name);
                    print("Placeable = " + targetWaypoint.isPlaceable);
                    if (targetWaypoint.isPlaceable)
                    {
                        PlaceTowerHere(targetWaypoint);
                    }
                }
            }
        }
    }

    public Tower GetTower1()
    {
        return tower1;
    }

    public void PlaceTowerHere(Waypoint waypoint)
    {
        waypoint.isPlaceable = false;

        if (towerQueue.Count == maxTowers)
        {
            Tower currentTower = towerQueue.Dequeue();
            currentTower.currentWaypoint.isPlaceable=true;
            currentTower.transform.position = waypoint.GetTowerSpawnPosition();
            currentTower.currentWaypoint = waypoint;
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
