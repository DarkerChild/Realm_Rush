using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    public Transform followTransform;
    public Transform cameraTransform;

    [SerializeField] float normalSpeed = 1;
    [SerializeField] float fastSpeed = 5;
    [SerializeField] float movementTime = 5;
    
    [SerializeField] float keyRotationAmount = 1;
    [SerializeField] float mouseRotateAmount = 5;

    [SerializeField] Vector3 zoomAmount = new Vector3(0f,-10f,10f);

    [SerializeField] float timeMultiplier = 1.5f;

    Vector3 newPosition;
    Quaternion newRotation;
    Vector3 newZoom;

    //Mouse control variables
    Vector3 dragStartPosition;
    Vector3 dragCurrentPosition;
    Vector3 rotateStartPosition;
    Vector3 rotateCurrentPosition;

    float movementSpeed = 1;

    void Start()
    {
        instance = this;
        newPosition = transform.position;
        newRotation = transform.rotation;
        newZoom = cameraTransform.localPosition;
    }


    void Update()
    {
        if (followTransform != null)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                followTransform = null;
            }
            else
            {
                transform.position = followTransform.position;
            } 
        }
        else
        {
            HandleMovementInput();
            HandleRotationInput();
            HandleZoomInput();
            HandleMouseInput();
        }
        CheckForSpeedBoost();
    }

    private void CheckForSpeedBoost()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Time.timeScale = 1f;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Time.timeScale = timeMultiplier;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Time.timeScale = timeMultiplier*2;
        }
    }

    private void HandleMovementInput()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = fastSpeed;
        }
        else
        {
            movementSpeed = normalSpeed;
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) 
        {
            newPosition += (transform.forward * movementSpeed);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            newPosition += (transform.forward * -movementSpeed);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            newPosition += (transform.right * -movementSpeed);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            newPosition += (transform.right * movementSpeed);
        }

        transform.position = Vector3.Lerp(transform.position,newPosition, Time.deltaTime * movementTime);
    }

    private void HandleRotationInput()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            newRotation *= Quaternion.Euler(Vector3.up * keyRotationAmount);
        }
        if (Input.GetKey(KeyCode.E))
        {
            newRotation *= Quaternion.Euler(Vector3.up * -keyRotationAmount);
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movementTime);
    }

    private void HandleZoomInput()
    {
        if (Input.GetKey(KeyCode.R))
        {
            newZoom += zoomAmount;
        }
        if (Input.GetKey(KeyCode.F))
        {
            newZoom -= zoomAmount;
        }

        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movementTime);
    }

    private void HandleMouseInput()
    {
        if(Input.mouseScrollDelta.y != 0)
        {
            newZoom += Input.mouseScrollDelta.y * zoomAmount;
        }

        if (Input.GetMouseButtonDown(1))
        {
            rotateStartPosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(1))
        {
            rotateCurrentPosition = Input.mousePosition;
            Vector3 difference = rotateStartPosition - rotateCurrentPosition;

            rotateStartPosition = rotateCurrentPosition;

            newRotation *= Quaternion.Euler(Vector3.up * (-difference.x / mouseRotateAmount));
        }

        if (Input.GetMouseButtonDown(0))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;

            if(plane.Raycast(ray, out entry))
            {
                dragStartPosition = ray.GetPoint(entry);
            }
        }

        if (Input.GetMouseButton(0))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;

            if (plane.Raycast(ray, out entry))
            {
                dragCurrentPosition = ray.GetPoint(entry);

                newPosition = transform.position + dragStartPosition - dragCurrentPosition;
            }
        }
    }
}
