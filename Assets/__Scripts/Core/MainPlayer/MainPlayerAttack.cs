using Bolt;
using PinguinoKatano.Core.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MainPlayerMovementFSM))]
public class MainPlayerAttack : EntityEventListener<IPenguinState>
{
    [SerializeField] MainPlayerMovementFSM mainPlayerMovementFSM;
    [SerializeField] private float attackRadius = 2f;
    [SerializeField] LayerMask layerMask;
    [SerializeField] Transform weaponPosition;

    void FixedUpdate()
    {
        if (!entity.IsOwner) return;
        if (!mainPlayerMovementFSM.IsAttacking) return;

        Collider[] hits = Physics.OverlapSphere(weaponPosition.position, attackRadius, layerMask);
        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; ++i)
            {
                Collider hit = hits[i];
                if (hit.gameObject == gameObject) continue;
                var serializer = hit.GetComponent<BoltEntity>();
                if (serializer != null && serializer.IsAttached && !serializer.GetState<IPenguinState>().IsDead)
                {
                    TakeDamage newEvent = TakeDamage.Create(serializer);
                    newEvent.Amount = 1;
                    newEvent.From = entity;
                    newEvent.Send();
                }
            }
        }
    }

    private void takeDamage(int amount, GameObject _gameObject)
    {
        Damageable player = _gameObject.GetComponent<Damageable>();
        if (player != null)
        {
            Debug.Log("Attacking player");
            player.TakeDamage(amount);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(weaponPosition.position, attackRadius);
    }

    public override void OnEvent(Killed evnt)
    {
        if (entity.IsOwner)
        {
            state.Kills += 1;
            Debug.Log("I Killed someone! My kills now: " + state.Kills);
        }
    }
}
