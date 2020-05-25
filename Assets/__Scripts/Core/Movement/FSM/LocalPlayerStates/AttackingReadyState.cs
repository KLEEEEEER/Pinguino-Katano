using PinguinoKatano.Core.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingReadyState : State
{
    public override void OnEnterState(MainPlayerMovementFSM playerFSM)
    {
        playerFSM.IsAttacking = false;
    }
    public override void OnUpdate(MainPlayerMovementFSM playerFSM)
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            playerFSM.WeaponSlot.SetActive(true);
            playerFSM.state.IsAttacking = true;
        }
        else
        {
            playerFSM.WeaponSlot.SetActive(false);
            playerFSM.state.IsAttacking = false;
            playerFSM.EnterState(playerFSM.idleState, true);
            return;
        }
    }

    public override void OnFixedUpdate(MainPlayerMovementFSM playerFSM)
    {
        if (playerFSM.AttackControl && playerFSM.IsAttacking)
        {
            playerFSM.MoveFixed(playerFSM.AttackControlMovementMultiplier);
        }
    }
}
