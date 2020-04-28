using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowersController : MonoBehaviour
{
    [SerializeField] Tower tower1 = null;
    public float damagePerShot = 10f;
    public float shotsPerSecond = 5f;
    [SerializeField] float targetDistance = 30f;
    [SerializeField] float towerAimSpeed = 5f;

    [SerializeField] GameObject deathFX = null;

    Queue<Tower> towerQueue;
    int maxTowers;

    PlayerStats playerStats;

    void Start()
    {
        towerQueue = new Queue<Tower>(maxTowers);
        playerStats = FindObjectOfType<PlayerStats>();
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
                    if (targetWaypoint.enabled == true)
                    {
                        if (targetWaypoint.isPlaceable)
                        {
                            PlaceTowerHere(targetWaypoint);
                        }
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
            MoveExistingTower(waypoint);
        }
        else
        {
            CreateNewTower(waypoint);
        }
    }

    private void MoveExistingTower(Waypoint waypoint)
    {
        Tower currentTower = towerQueue.Dequeue();
        currentTower.currentWaypoint.isPlaceable = true;
        currentTower.transform.position = waypoint.GetTowerSpawnPosition();
        currentTower.currentWaypoint = waypoint;
        towerQueue.Enqueue(currentTower);
    }

    private void CreateNewTower(Waypoint waypoint)
    {
        playerStats.NewTowerCreated();
        Vector3 towerSpawnPosition = waypoint.GetTowerSpawnPosition();
        Tower newTower = Instantiate(tower1, towerSpawnPosition, Quaternion.identity, transform);
        newTower.currentWaypoint = waypoint;
        newTower.SetDeathFX(deathFX);
        UpdateTowerStats(newTower);
        towerQueue.Enqueue(newTower);
    }

    private void UpdateTowerStats(Tower newTower)
    {
        newTower.damagePerShot = damagePerShot;
        newTower.shotsPerSecond = shotsPerSecond;
        newTower.targetDistance = targetDistance;
        newTower.towerAimSpeed = towerAimSpeed;
    }

    public void UpdateTowerCount(int newMaxTowers)
    {
        maxTowers = newMaxTowers;
    }

    public void UpgradeTower ()
    {
        damagePerShot *= 1.1f;
        shotsPerSecond *= 1.1f;
        towerAimSpeed *= 0.9f;
    }
}
