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
    [SerializeField] Color normalColor = Color.green;

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

        Waypoint startWaypoint = pathfinder.GetStartWaypoint();
        Waypoint endWaypoint = pathfinder.GetEndWaypoint();

        startWaypoint.SetTopColor(startColor);
        endWaypoint.SetTopColor(endColor);
        
        if (waypoint != startWaypoint && waypoint != endWaypoint)
        {
            waypoint.SetTopColor(normalColor);
        }
        
    }
}