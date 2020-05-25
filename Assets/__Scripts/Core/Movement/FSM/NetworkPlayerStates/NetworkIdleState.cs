using UnityEngine;
namespace PinguinoKatano.Core.Movement {
    public class NetworkIdleState : NetworkState
    {
        bool firstLaunch = true;
        public override void OnEnterState(MainPlayerMovementFSM playerFSM)
        {
            if (firstLaunch)
            {
                lastX = playerFSM.boltState.Transform.Position.x;
                lastY = playerFSM.boltState.Transform.Position.y;
                lastZ = playerFSM.boltState.Transform.Position.z;
                firstLaunch = false;
            }
        }
        public override void OnUpdate(MainPlayerMovementFSM playerFSM)
        {
            //Debug.Log($"Updating NetworkIdleState"); //  lastX = {lastX} lastY = {lastY} lastZ = {lastZ}
            currentX = playerFSM.boltState.Transform.Position.x;
            currentY = playerFSM.boltState.Transform.Position.y;
            currentZ = playerFSM.boltState.Transform.Position.z;

            

            lastX = currentX;
            lastY = currentY;
            lastZ = currentZ;
        }
    }
}