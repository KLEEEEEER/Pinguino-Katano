﻿using UnityEngine;
using Bolt;
using System;

namespace PinguinoKatano.Core.Movement
{
    public class MainPlayerMovementFSM : EntityEventListener<IPenguinState>
    {
        public State currentState;
        public Rigidbody rigidbody;
        public float movementSpeed;
        public float JumpingForce;
        public float RollingForce;
        public GameObject Sword;
        public Vector3 InitialSwordRotation = new Vector3(-4.6f, 58.35f, 103.02f);
        public Vector3 InitialSwordPosition;
        public Vector3 AttackingSwordRotation = new Vector3(-64.7f, 60.7f, -75.1f);
        public Vector3 AttackingSwordPosition = new Vector3(0, 0, 0);
        public Quaternion InitialSwordRotationQuaternion;
        public Quaternion AttackingSwordRotationQuaternion;
        public bool AirControl = false;
        public bool AttackControl = false;
        public bool IsAttacking = false;
        [Range(0f, 10f)]
        public float AttackControlMovementMultiplier = 1f;

        public Animator anim;

        [Header("Transforms")]
        public Transform GroundCheckPoint;
        public float GroundCheckRadius = 0.2f;
        public LayerMask GroundMask;

        public float horizontalInput;
        public float verticalInput;

        public State idleState;
        public State jumpingState;
        public State runningState;
        public State attackingReadyState;
        public State rollingState;

        public IPenguinState boltState;

        private float timerChangingRigidbodyVelocity;

        private Camera mainCamera;
        public float timeBeforeRollCompleted = 1.5f;

        public override void Attached()
        {
            boltState = state;
            state.AddCallback("IsAttacking", IsAttackingChangedCallback);
            if (!entity.IsOwner) return;
            state.IsDead = false;
            state.SetTransforms(state.Transform, transform);
            state.IsAttacking = false;
            state.Kills = 0;
            state.Deaths = 0;
        }

        private void Start()
        {
            InitialSwordRotationQuaternion.eulerAngles = InitialSwordRotation;
            AttackingSwordRotationQuaternion.eulerAngles = AttackingSwordRotation;
            InitialSwordPosition = Sword.transform.position;

            mainCamera = Camera.main;

            if (entity.IsOwner)
            {
                idleState = new IdleState();
                jumpingState = new JumpingState();
                runningState = new RunningState();
                attackingReadyState = new AttackingReadyState();
                rollingState = new RollingState();
            }
            else
            {
                idleState = new NetworkIdleState();
                jumpingState = new NetworkJumpingState();
                runningState = new NetworkRunningState();
                attackingReadyState = new NetworkAttackingReadyState();
                rollingState = new NetworkRollingState();
            }

            currentState = idleState;
        }

        public void EnterState(State state, bool sendEvent = false)
        {
            currentState = state;

            if (sendEvent)
                SendStateChangedEvent();

            state.OnEnterState(this);
        }

        private void SendStateChangedEvent()
        {
            StateChanged stateChangedEvent = StateChanged.Create(entity);
            Type currentStateType = currentState.GetType();
            stateChangedEvent.Name = currentStateType.Name;
            stateChangedEvent.Send();
        }

        public override void OnEvent(StateChanged evnt)
        {
            if (entity.IsOwner) return;
            //Sword.SetActive(false);
            anim.SetBool("IsAttackingState", false);
            switch (evnt.Name)
            {
                case nameof(IdleState):
                    EnterState(idleState);
                    break;
                case nameof(JumpingState):
                    EnterState(jumpingState);
                    break;
                case nameof(RunningState):
                    EnterState(runningState);
                    break;
                case nameof(AttackingReadyState):
                    EnterState(attackingReadyState);
                    break;
                case nameof(RollingState):
                    EnterState(rollingState);
                    break;
            }
            Debug.Log("Changed event to " + evnt.Name);
        }

        private void Update()
        {
            //if (!entity.IsOwner) return;
            if (entity.IsOwner)
            {
                horizontalInput = Input.GetAxisRaw("Horizontal");
                verticalInput = Input.GetAxisRaw("Vertical");
            }

            currentState.OnUpdate(this);
        }

        public override void SimulateOwner()
        {
            currentState.OnFixedUpdate(this);
        }

        public void IsAttackingChangedCallback()
        {
            Debug.Log("Changed IsAttacking and IsAttackingChangedCallback Raised");
            IsAttacking = state.IsAttacking;
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