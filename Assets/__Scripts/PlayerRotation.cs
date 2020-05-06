using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform pointOfView;

    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;

        Vector3 playerOnCameraPosition = mainCamera.WorldToScreenPoint(pointOfView.position);

        mousePosition.x = mousePosition.x - playerOnCameraPosition.x;
        mousePosition.y = mousePosition.y - playerOnCameraPosition.y;

        float angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;
        angle -= 90f;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, -angle, 0));

        transform.rotation = targetRotation;
    }
}
