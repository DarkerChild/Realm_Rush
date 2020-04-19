using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
[RequireComponent(typeof(Waypoint))]
public class CubeEditor : MonoBehaviour
{
    Waypoint waypoint;

    [SerializeField] Color startColor = Color.cyan;
    [SerializeField] Color endColor = Color.magenta;

    void Update()
    {
        GetWaypoint();
        SnapToGrid();
        UpdateLabelAndName();
        SetStartAndEndColor();
    }

    private void GetWaypoint()
    {
        if (waypoint == null)
        {
            waypoint = GetComponent<Waypoint>();
        }
    }

    private void SnapToGrid()
    {
        GetWaypoint();
        int gridSize = waypoint.GetGridSize();

        transform.position = new Vector3(
            waypoint.GetGridPosition().x * gridSize,
            0f,
            waypoint.GetGridPosition().y * gridSize
        );        
    }

    private void UpdateLabelAndName()
    {
        GetWaypoint();
        int gridSize = waypoint.GetGridSize();
        TextMesh textMesh = GetComponentInChildren<TextMesh>();
        string labelText =
            waypoint.GetGridPosition().x
            + "," 
            + waypoint.GetGridPosition().y;
        textMesh.text = labelText;
        gameObject.name = labelText;
    }

    private void SetStartAndEndColor()
    {
        Pathfinder pathfinder = FindObjectOfType<Pathfinder>();

        GameObject[] gameobjects = pathfinder.GetStartAndEndObjects();
        if (waypoint.gameObject == gameobjects[0])
        {
            waypoint.SetTopColor(Color.cyan);
        }
        else if (waypoint.gameObject == gameobjects[1])
        {
            waypoint.SetTopColor(Color.magenta);
        }
        else
        {
            waypoint.SetTopColor(Color.green);
        }
    }
}