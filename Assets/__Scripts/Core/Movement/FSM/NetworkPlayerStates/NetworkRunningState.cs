using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PinguinoKatano.Core.Movement
{
    public class NetworkRunningState : NetworkState
    {
        public override void OnEnterState(MainPlayerMovementFSM playerFSM)
        {
            playerFSM.anim.SetTrigger("RunningState");
        }
    }
}