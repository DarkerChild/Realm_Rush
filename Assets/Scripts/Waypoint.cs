using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public bool isExplored = false;
    public Waypoint exploredFrom = null;
    public bool isPlaceable = true;


    Vector2Int gridPos;
    Vector3 towerSpawnPosition;
    Tower tower1;
    TowersController towerController;

    const int gridSize = 10;

    private void Start()
    {
        towerSpawnPosition = transform.position + (Vector3.up * 2);
        towerController = FindObjectOfType<TowersController>();
        tower1 = towerController.GetTower1();
    }

    public Vector2Int GetGridPosition()
    {
        return new Vector2Int(
            Mathf.RoundToInt(transform.position.x / gridSize),
            Mathf.RoundToInt(transform.position.z / gridSize)
            );
    }

    public int GetGridSize()
    {
        return gridSize;
    }

    public Vector3 GetTowerSpawnPosition()
    {
        return towerSpawnPosition;
    }
    /*
    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isPlaceable)
            {
                towerController.PlaceTowerHere(this);
            }
        }
    }
    */
}
