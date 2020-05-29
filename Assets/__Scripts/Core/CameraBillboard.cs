using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBillboard : MonoBehaviour
{
    Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (mainCamera == null) return;
        transform.LookAt(mainCamera.transform);
        Quaternion tempRotation = transform.rotation;
        Vector3 tempEulerAngles = tempRotation.eulerAngles;
        tempEulerAngles.y = mainCamera.transform.rotation.eulerAngles.y + 180;
        tempRotation.eulerAngles = tempEulerAngles;
        transform.rotation = tempRotation;
    }
}
