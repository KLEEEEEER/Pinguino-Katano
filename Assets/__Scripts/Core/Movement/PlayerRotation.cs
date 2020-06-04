using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;

namespace PinguinoKatano.Core.Movement
{
    public class PlayerRotation : EntityBehaviour<IPenguinState>
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private Transform pointOfView;

        public override void Attached()
        {
            state.SetTransforms(state.Transform, transform);
        }

        private void Start()
        {
            if (mainCamera == null)
            {
                mainCamera = Camera.main;
            }
        }

        /*void Update()
        {
            Vector3 mousePosition = Input.mousePosition;

            Vector3 playerOnCameraPosition = mainCamera.WorldToScreenPoint(pointOfView.position);

            mousePosition.x = mousePosition.x - playerOnCameraPosition.x;
            mousePosition.y = mousePosition.y - playerOnCameraPosition.y;

            float angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;
            angle -= 90f;

            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, -angle, 0));

            transform.rotation = targetRotation;
        }*/

        public override void SimulateOwner()
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
}