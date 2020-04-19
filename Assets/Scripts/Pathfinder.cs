using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    [SerializeField] GameObject startPosition = null, endPosition = null;

    // Start is called before the first frame update
    void Start()
    {
        LoadBlocks();
    }

    private void LoadBlocks()
    {
        Waypoint[] waypoints = FindObjectsOfType<Waypoint>();
        foreach (Waypoint waypoint in waypoints)
        {
            Vector2Int gridPos = waypoint.GetGridPosition();

            if (grid.ContainsKey(gridPos))
            {
                Debug.LogWarning("Overlapping block at " + gridPos / waypoint.GetGridSize());
            }
            else
            {
                grid.Add(gridPos, waypoint);
            }
            
        }
    }

    public GameObject[] GetStartAndEndObjects()
    {
        GameObject[] gameObjects = new GameObject[2];
        gameObjects[0] = startPosition;
        gameObjects[1] = endPosition;
        return gameObjects;
    }
}
