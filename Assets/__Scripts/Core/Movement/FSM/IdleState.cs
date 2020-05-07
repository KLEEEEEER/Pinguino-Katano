using UnityEngine;
namespace PinguinoKatano.Core.Movement {
    public class IdleState : State
    {
        public override void OnUpdate(MainPlayerMovementFSM playerFSM)
        {
            //playerFSM.Move();

            if (Mathf.Abs(playerFSM.horizontalInput) > 0 || Mathf.Abs(playerFSM.verticalInput) > 0)
            {
                playerFSM.EnterState(playerFSM.RunningState);
                return;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerFSM.EnterState(playerFSM.jumpingState);
                return;
            }

            if (Input.GetKey(KeyCode.Mouse0))
            {
                playerFSM.EnterState(playerFSM.AttackingReadyState);
                return;
            }
        }
    }
}