using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardToCamera : MonoBehaviour
{
    void Update()
    {
        // Make the object face the camera
        transform.LookAt(Camera.main.transform);
    }
}
