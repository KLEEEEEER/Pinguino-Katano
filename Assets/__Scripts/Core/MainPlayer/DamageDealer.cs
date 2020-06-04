using PinguinoKatano.Core.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;

public class DamageDealer : EntityEventListener<IPenguinState>
{
    [SerializeField] private float attackRadius = 2f;
    [SerializeField] MainPlayerMovementFSM mainPlayerMovementFSM;
    [SerializeField] LayerMask layerMask;
    [SerializeField] Transform weaponPosition;

    private void FixedUpdate()
    {
        using (var hits = BoltNetwork.OverlapSphereAll(weaponPosition.position, attackRadius))
        {
            if (mainPlayerMovementFSM.currentState == mainPlayerMovementFSM.attackingReadyState && hits.count > 0)
            {
                for (int i = 0; i < hits.count; ++i)
                {
                    var hit = hits.GetHit(i);
                    var serializer = hit.body.GetComponent<Damageable>();
                    if (serializer != null)
                    {
                        serializer.TakeDamage(1);
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(weaponPosition.position, attackRadius);
    }
}
