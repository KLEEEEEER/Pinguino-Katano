using PinguinoKatano.Core.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MainPlayerMovementFSM))]
public class MainPlayerAttack : MonoBehaviour
{
    [SerializeField] MainPlayerMovementFSM mainPlayerMovementFSM;

    void Update()
    {
        if (mainPlayerMovementFSM.currentState != mainPlayerMovementFSM.AttackingReadyState) return;

        Debug.Log("MainPlayerAttack is active!");
    }
}
