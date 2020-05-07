using PinguinoKatano.Core.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingReadyState : State
{
    private bool isAttacking;
    public override void OnEnterState(MainPlayerMovementFSM playerFSM)
    {
        isAttacking = false;
    }
    public override void OnUpdate(MainPlayerMovementFSM playerFSM)
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
            playerFSM.EnterState(playerFSM.idleState);
            return;
        }
    }

    public override void OnFixedUpdate(MainPlayerMovementFSM playerFSM)
    {
        if (playerFSM.AttackControl && isAttacking)
        {
            playerFSM.MoveFixed(playerFSM.AttackControlMovementMultiplier);
        }
    }
}
