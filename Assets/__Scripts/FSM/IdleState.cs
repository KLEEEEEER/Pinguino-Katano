using UnityEngine;
namespace PinguinoKatano.Core.Movement {
    public class IdleState : State
    {
        public override void OnUpdate(MainPlayerMovementFSM playerFSM)
        {
            playerFSM.Move();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerFSM.EnterState(playerFSM.jumpingState);
            }
        }
    }
}