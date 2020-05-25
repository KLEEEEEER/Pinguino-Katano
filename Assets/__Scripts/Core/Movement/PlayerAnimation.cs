using Bolt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PinguinoKatano.Core.Movement
{ 
    public class PlayerAnimation : EntityBehaviour<IPenguinState>
    {
        [SerializeField] private MainPlayerMovementFSM mainPlayerMovementFSM;
    }
}
