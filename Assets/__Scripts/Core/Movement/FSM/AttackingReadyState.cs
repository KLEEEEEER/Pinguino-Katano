using PinguinoKatano.Core.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingReadyState : State
{
    public override void OnEnterState(MainPlayerMovementFSM playerFSM)
    {
        
    }
    public override void OnUpdate(MainPlayerMovementFSM playerFSM)
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {

        }
        else
        {
            playerFSM.EnterState(playerFSM.idleState);
            return;
        }

        if (playerFSM.AttackControl)
        {
            playerFSM.Move(playerFSM.AttackControlMovementMultiplier);
        }
    }
}
