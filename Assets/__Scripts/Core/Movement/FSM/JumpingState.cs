using UnityEngine;
namespace PinguinoKatano.Core.Movement
{
    public class JumpingState : State
    {
        private bool isJumping = false;
        private float timerTime = 0;
        private float timeBeforeJumpCheck = 0.3f;
        public override void OnEnterState(MainPlayerMovementFSM playerFSM)
        {
            isJumping = false;
            timerTime = 0;
            //playerFSM.rigidbody.AddForce(Vector3.up * playerFSM.JumpingForce, ForceMode.Impulse);
            playerFSM.ApplyForce(Vector3.up * playerFSM.JumpingForce, ForceMode.Impulse);
        }

        public override void OnUpdate(MainPlayerMovementFSM playerFSM)
        {
            if (!isJumping) timerTime += Time.deltaTime;
        }

        public override void OnFixedUpdate(MainPlayerMovementFSM playerFSM)
        {
            if (playerFSM.AirControl)
            {
                playerFSM.MoveFixed();
            }

            if (timerTime >= timeBeforeJumpCheck)
            {
                isJumping = true;
            }

            if (isJumping)
            {
                Collider[] colliders = Physics.OverlapSphere(playerFSM.GroundCheckPoint.position, playerFSM.GroundCheckRadius, playerFSM.GroundMask);
                if (colliders.Length > 0)
                {
                    isJumping = false;
                    playerFSM.EnterState(playerFSM.idleState);
                }
            }
        }


    }
}