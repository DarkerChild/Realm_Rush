using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    const int gridSize = 10;
    Vector2Int gridPos;

    public bool isExplored = false;
    public Waypoint exploredFrom = null;
    public bool isPlaceable = true;

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

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            print(gameObject.name + " - Clicked");
        }
    }
}
