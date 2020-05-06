using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PinguinoKatano.Core.Movement
{
    public abstract class State
    {
        public virtual void OnEnterState(MainPlayerMovementFSM playerFSM) { }
        public virtual void OnUpdate(MainPlayerMovementFSM playerFSM) { }
        public virtual void OnFixedUpdate(MainPlayerMovementFSM playerFSM) { }
        public virtual void OnTriggerEnter(MainPlayerMovementFSM playerFSM, Collider other) { }
        public virtual void OnTriggerExit(MainPlayerMovementFSM playerFSM, Collider other) { }
        public virtual void OnCollisionEnter(MainPlayerMovementFSM playerFSM, Collision collision) { }
        public virtual void OnCollisionExit(MainPlayerMovementFSM playerFSM, Collision collision) { }
    }
}