using UnityEngine;

namespace PinguinoKatano.Core.Movement
{
    public class NetworkRollingState : State
    {
        public override void OnEnterState(MainPlayerMovementFSM playerFSM)
        {
            playerFSM.anim.SetTrigger("RollingState");
        }
    }
}