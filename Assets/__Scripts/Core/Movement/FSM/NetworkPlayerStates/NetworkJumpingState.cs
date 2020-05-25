using UnityEngine;
namespace PinguinoKatano.Core.Movement
{
    public class NetworkJumpingState : NetworkState
    {
        public override void OnUpdate(MainPlayerMovementFSM playerFSM)
        {
            //Debug.Log($"Updating NetworkJumpingState");
            currentX = playerFSM.boltState.Transform.Position.x;
            currentY = playerFSM.boltState.Transform.Position.y;
            currentZ = playerFSM.boltState.Transform.Position.z;


            lastX = currentX;
            lastY = currentY;
            lastZ = currentZ;
        }
    }
}