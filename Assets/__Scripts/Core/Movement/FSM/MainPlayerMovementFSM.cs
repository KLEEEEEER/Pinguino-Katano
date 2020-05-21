using UnityEngine;
using Bolt;

namespace PinguinoKatano.Core.Movement
{
    public class MainPlayerMovementFSM : EntityBehaviour<IPenguinState>
    {
        public State currentState;
        public Rigidbody rigidbody;
        public float movementSpeed;
        public float JumpingForce;
        public float RollingForce;
        public GameObject WeaponSlot;
        public bool AirControl = false;
        public bool AttackControl = false;
        [Range(0f, 10f)]
        public float AttackControlMovementMultiplier = 1f;

        [Header("Transforms")]
        public Transform GroundCheckPoint;
        public float GroundCheckRadius = 0.2f;
        public LayerMask GroundMask;

        public float horizontalInput;
        public float verticalInput;

        public State idleState;
        public State jumpingState;
        public State RunningState;
        public State AttackingReadyState;
        public State RollingState;

        private float timerChangingRigidbodyVelocity;

        public override void Attached()
        {
            state.SetTransforms(state.Transform, transform);
        }

        private void Start()
        {
            idleState = new IdleState();
            jumpingState = new JumpingState();
            RunningState = new RunningState();
            AttackingReadyState = new AttackingReadyState();
            RollingState = new RollingState();

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

        public override void SimulateOwner()
        {
            currentState.OnFixedUpdate(this);
        }

        public void MoveFixed(float speedModifier = 1f)
        {
            Vector3 tempVelocity = new Vector3(horizontalInput, 0f, verticalInput);
            tempVelocity = Vector3.ClampMagnitude(tempVelocity, 1f);
            tempVelocity = tempVelocity * movementSpeed * speedModifier * Time.fixedDeltaTime;
            tempVelocity.y = rigidbody.velocity.y;

            timerChangingRigidbodyVelocity += Time.fixedDeltaTime;

            if (rigidbody.velocity != tempVelocity)
            {
                rigidbody.velocity = Vector3.Lerp(rigidbody.velocity, tempVelocity, 0.1f);
            }
        }

        public void ApplyForce(Vector3 force, ForceMode fMode)
        {
            rigidbody.AddForce(force, fMode);
        }


        public bool IsMoving()
        {
            return (Mathf.Abs(horizontalInput) > 0 || Mathf.Abs(verticalInput) > 0);
        }

        /*private void FixedUpdate()
        {
            currentState.OnFixedUpdate(this);
        }*/

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