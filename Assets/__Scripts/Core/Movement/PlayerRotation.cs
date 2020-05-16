using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PinguinoKatano.Core.Movement
{
    public class PlayerRotation : NetworkBehaviour
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private Transform pointOfView;

        private void Start()
        {
            if (!isLocalPlayer) return;

            if (mainCamera == null)
            {
                mainCamera = Camera.main;
            }
        }

        void Update()
        {
            if (!isLocalPlayer) return;

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
}