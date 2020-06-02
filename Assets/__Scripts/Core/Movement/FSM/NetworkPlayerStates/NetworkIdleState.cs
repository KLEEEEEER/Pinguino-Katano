using UnityEngine;
namespace PinguinoKatano.Core.Movement {
    public class NetworkIdleState : NetworkState
    {
        public override void OnEnterState(MainPlayerMovementFSM playerFSM)
        {
            playerFSM.anim.SetTrigger("IdleState");
        }
    }
}