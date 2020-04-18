using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CubeLabel : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        TextMesh text = GetComponent<TextMesh>();
        text.text = transform.position.x + "," + transform.position.z;
    }
}
