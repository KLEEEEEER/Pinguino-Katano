using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PinguinoKatano.Core.Movement
{
    public class NetworkRunningState : NetworkState
    {
        public override void OnUpdate(MainPlayerMovementFSM playerFSM)
        {
            //Debug.Log($"Updating NetworkRunningState");

            currentX = playerFSM.boltState.Transform.Position.x;
            currentY = playerFSM.boltState.Transform.Position.y;
            currentZ = playerFSM.boltState.Transform.Position.z;

            

            lastX = currentX;
            lastY = currentY;
            lastZ = currentZ;
        }
    }
}