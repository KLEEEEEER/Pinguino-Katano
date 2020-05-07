using Mirror;
using PinguinoKatano.Core.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MainPlayerMovementFSM))]
public class MainPlayerAttack : NetworkBehaviour
{
    [SerializeField] MainPlayerMovementFSM mainPlayerMovementFSM;

    void Update()
    {
        if (!isLocalPlayer) return;

        if (mainPlayerMovementFSM.currentState != mainPlayerMovementFSM.AttackingReadyState) return;

        Debug.Log("Attacking!");
    }
}
