using UnityEngine;
namespace PinguinoKatano.Core.Movement
{
    public class NetworkJumpingState : NetworkState
    {
        public override void OnEnterState(MainPlayerMovementFSM playerFSM)
        {
            playerFSM.anim.SetTrigger("JumpingState");
        }
    }
}