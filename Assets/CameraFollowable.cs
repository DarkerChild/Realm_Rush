using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowable : MonoBehaviour
{
    public void OnMouseDown()
    {
        CameraController.instance.followTransform = transform;
    }
}
