using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PinguinoKatano.Core.Movement
{
    public class MainPlayerMovementFSM : MonoBehaviour
    {
        private State currentState;
        public Rigidbody rigidbody;
        public float movementSpeed;
        public float JumpingForce;
        public bool AirControl = false;

        [Header("Transforms")]
        public Transform GroundCheckPoint;
        public float GroundCheckRadius = 0.2f;
        public LayerMask GroundMask;

        public float horizontalInput;
        public float verticalInput;

        public State idleState;
        public State jumpingState;

        private void Start()
        {
            idleState = new IdleState();
            jumpingState = new JumpingState();

            currentState = idleState;
        }

        public void EnterState(State state)
        {
            currentState = state;
            state.OnEnterState(this);
        }

        private void Update()
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput   = Input.GetAxisRaw("Vertical");

            currentState.OnUpdate(this);
        }

        public void Move(float speedModifier = 1f)
        {
            Vector3 tempVelocity = rigidbody.velocity;
            tempVelocity.x = horizontalInput * movementSpeed * speedModifier;
            tempVelocity.z = verticalInput * movementSpeed * speedModifier;
            rigidbody.velocity = tempVelocity;
        }

        private void FixedUpdate()
        {
            currentState.OnFixedUpdate(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            currentState.OnTriggerEnter(this, other);
        }

        private void OnTriggerExit(Collider other)
        {
            currentState.OnTriggerExit(this, other);
        }

        private void OnCollisionEnter(Collision collision)
        {
            currentState.OnCollisionEnter(this, collision);
        }

        private void OnCollisionExit(Collision collision)
        {
            currentState.OnCollisionExit(this, collision);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(GroundCheckPoint.position, GroundCheckRadius);
        }
    }
}