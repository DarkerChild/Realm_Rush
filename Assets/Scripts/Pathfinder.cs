using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    Queue<Waypoint> queue = new Queue<Waypoint>();

    [SerializeField]  Waypoint startWaypoint = null, endWaypoint = null;

    List<Waypoint> path = new List<Waypoint>();

    Vector2Int[] directions = {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };

    bool isRunning = true;

    Waypoint searchCentre;

    private void Start()
    {
        SetPath();
    }

    public Waypoint GetStartWaypoint()
    {
        return startWaypoint;
    }

    public Waypoint GetEndWaypoint()
    {
        return endWaypoint;
    }

    private void SetPath()
    {
        LoadBlocks();
        PopulateExploredFrom();
        CreatePath();
    }

    private void CreatePath()
    {
        Waypoint nextWaypoint = endWaypoint;
        while (nextWaypoint != startWaypoint)
        {
            SetAsPath(nextWaypoint);
            nextWaypoint = nextWaypoint.exploredFrom;
        }
        SetAsPath(startWaypoint);
        path.Reverse();
    }

    private void SetAsPath(Waypoint waypoint)
    {
        path.Add(waypoint);
        waypoint.isPlaceable = false;
    }

    public List<Waypoint> GetPath()
    {
        return path;
    }

    private void LoadBlocks()
    {
        grid.Clear();
        Waypoint[] waypoints = FindObjectsOfType<Waypoint>();
        foreach (Waypoint waypoint in waypoints)
        {
            Vector2Int gridPos = waypoint.GetGridPosition();

            if (grid.ContainsKey(gridPos))
            {
                Debug.LogWarning("Overlapping block at " + gridPos);
            }
            else
            {
                grid.Add(gridPos, waypoint);
            }
        }
    }

    public void PopulateExploredFrom()
    {
        queue.Enqueue(startWaypoint);

        while (queue.Count > 0 && isRunning)
        {
            searchCentre = queue.Dequeue();
            HaltIfEndFound();
            ExploreNeighbours();
            searchCentre.isExplored = true;
        }
    }

    private void HaltIfEndFound()
    {
        if (searchCentre == endWaypoint)
        {
            isRunning = false;
        }
    }

    private void ExploreNeighbours()
    {
        if (!isRunning) { return;  } //Only epxlore neighbours if pathfinding is still running.

        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighbourPosition = searchCentre.GetGridPosition() + direction;
            if (grid.ContainsKey(neighbourPosition))
            {
                QueueNewNeighbours(neighbourPosition);
            }
        }
    }

    private void QueueNewNeighbours(Vector2Int targetPosition)
    {
        Waypoint neighbour = grid[targetPosition];
        if (!(neighbour.isExplored || queue.Contains(neighbour)))
        {
            queue.Enqueue(neighbour);
            neighbour.exploredFrom = searchCentre;
        }
    }
}
