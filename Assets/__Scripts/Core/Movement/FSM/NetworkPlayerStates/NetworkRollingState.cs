using UnityEngine;

namespace PinguinoKatano.Core.Movement
{
    public class NetworkRollingState : State
    {
        private float timerTime;
        public override void OnEnterState(MainPlayerMovementFSM playerFSM)
        {
            timerTime = 0;
            playerFSM.anim.SetTrigger("RollingState");
        }

        public override void OnUpdate(MainPlayerMovementFSM playerFSM)
        {
            timerTime += Time.deltaTime;

            if (timerTime >= playerFSM.timeBeforeRollCompleted)
            {
                playerFSM.anim.SetTrigger("RollingEnds");
                playerFSM.EnterState(playerFSM.idleState, true);
            }
        }
    }
}