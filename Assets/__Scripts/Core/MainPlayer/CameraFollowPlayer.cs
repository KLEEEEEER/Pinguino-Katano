using Bolt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : EntityBehaviour<IPenguinState>
{
    Camera mainCamera;
    [SerializeField] Vector3 offset;
    public override void Attached()
    {
        if (entity.IsOwner)
         mainCamera = Camera.main;
    }

    private void Update()
    {
        if (entity.IsOwner)
            mainCamera.transform.position = transform.position + offset;
    }
}
