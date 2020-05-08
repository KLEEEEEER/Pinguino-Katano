using UnityEngine;

namespace PinguinoKatano.Core.Movement
{
    public class RollingState : State
    {
        private float timerTime = 0;
        private float timeBeforeRollCompleted = 1f;
        public override void OnEnterState(MainPlayerMovementFSM playerFSM)
        {
            timerTime = 0;
            Vector3 direction = new Vector3(playerFSM.horizontalInput, 0f, playerFSM.verticalInput);
            //playerFSM.rigidbody.AddForce(direction.normalized * playerFSM.RollingForce, ForceMode.Impulse);
            playerFSM.ApplyForce(direction.normalized * playerFSM.RollingForce, ForceMode.Impulse);
        }

        public override void OnUpdate(MainPlayerMovementFSM playerFSM)
        {
            timerTime += Time.deltaTime;

            if (timerTime >= timeBeforeRollCompleted)
            {
                playerFSM.EnterState(playerFSM.idleState);
            }
        }
    }
}