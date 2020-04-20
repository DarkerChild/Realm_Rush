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

    void Update()
    {
        GetWaypoint();
        SnapToGrid();
        UpdateLabelAndName();
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
}