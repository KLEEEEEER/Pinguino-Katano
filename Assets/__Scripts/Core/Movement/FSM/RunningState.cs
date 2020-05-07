using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PinguinoKatano.Core.Movement
{
    public class RunningState : State
    {
        public override void OnUpdate(MainPlayerMovementFSM playerFSM)
        {
            if (Mathf.Abs(playerFSM.horizontalInput) == 0 && Mathf.Abs(playerFSM.verticalInput) == 0)
            {
                playerFSM.EnterState(playerFSM.idleState);
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

            if (Input.GetKeyDown(KeyCode.LeftShift) && playerFSM.IsMoving())
            {
                Debug.Log("Shift pressed!");
                playerFSM.EnterState(playerFSM.RollingState);
            } 
        }

        public override void OnFixedUpdate(MainPlayerMovementFSM playerFSM)
        {
            playerFSM.MoveFixed();
        }
    }
}