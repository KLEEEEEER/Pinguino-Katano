using PinguinoKatano.Core.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkAttackingReadyState : NetworkState
{

    public override void OnUpdate(MainPlayerMovementFSM playerFSM)
    {
        //Debug.Log($"Updating NetworkAttackingReadyState");

        if (playerFSM.IsAttacking)
        {
            Debug.Log("IsAttacking! im attacking");
            //playerFSM.WeaponSlot.SetActive(true);
            playerFSM.anim.SetBool("IsAttackingState", true);
        }
        else
        {
            Debug.Log("not IsAttacking! im not attacking");
            //playerFSM.WeaponSlot.SetActive(false);
            playerFSM.anim.SetBool("IsAttackingState", false);
            return;
        }
    }
}
