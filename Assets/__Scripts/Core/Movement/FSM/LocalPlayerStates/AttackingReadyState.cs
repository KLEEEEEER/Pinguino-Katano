using PinguinoKatano.Core.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingReadyState : State
{
    public override void OnEnterState(MainPlayerMovementFSM playerFSM)
    {
        playerFSM.IsAttacking = false;
        playerFSM.anim.SetBool("IsAttackingState", true);
    }
    public override void OnUpdate(MainPlayerMovementFSM playerFSM)
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            //playerFSM.WeaponSlot.SetActive(true);
            playerFSM.anim.SetBool("IsAttackingState", true);
            playerFSM.state.IsAttacking = true;
        }
        else
        {
            //playerFSM.WeaponSlot.SetActive(false);
            playerFSM.state.IsAttacking = false;
            playerFSM.anim.SetBool("IsAttackingState", false);
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
