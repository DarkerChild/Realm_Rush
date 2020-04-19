﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
public class EditorSnap : MonoBehaviour
{
    [SerializeField] [Range(1f,20f)] float gridSize = 10f;

    TextMesh textMesh;

    void Update()
    {
        SnapPosition();
        UpdateLabelAndName();
    }

    private void UpdateLabelAndName()
    {
        textMesh = GetComponentInChildren<TextMesh>();
        string labelText = transform.position.x / gridSize + "," + transform.position.z / gridSize;
        textMesh.text = labelText;
        gameObject.name = "Cube " + labelText;
    }

    private void SnapPosition()
    {
        Vector3 snapPosition;
        snapPosition.x = Mathf.RoundToInt(transform.position.x / gridSize) * gridSize;
        snapPosition.y = Mathf.RoundToInt(transform.position.y / gridSize) * gridSize;
        snapPosition.z = Mathf.RoundToInt(transform.position.z / gridSize) * gridSize;
        transform.position = snapPosition;
    }
}