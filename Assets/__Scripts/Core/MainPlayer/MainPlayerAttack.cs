using Bolt;
using PinguinoKatano.Core.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MainPlayerMovementFSM))]
public class MainPlayerAttack : EntityBehaviour<IPenguinState>
{
    [SerializeField] MainPlayerMovementFSM mainPlayerMovementFSM;
    [SerializeField] private float attackRadius = 2f;
    [SerializeField] LayerMask layerMask;
    [SerializeField] Transform weaponPosition;

    void FixedUpdate()
    {
        if (!entity.IsOwner) return;
        if (!mainPlayerMovementFSM.IsAttacking) return;

        //using (var hits = BoltNetwork.OverlapSphereAll(weaponPosition.position, attackRadius, BoltNetwork.ServerFrame))
        //using (var hits = BoltNetwork.OverlapSphereAll(state.Transform.Position, 10f, BoltNetwork.ServerFrame))
        Collider[] hits = Physics.OverlapSphere(weaponPosition.position, attackRadius, layerMask);
        //{
            //Debug.Log($"hits.count = {hits.Length}");
            if (hits.Length > 0)
            {
                for (int i = 0; i < hits.Length; ++i)
                {
                    Collider hit = hits[i];
                    if (hit.gameObject == gameObject) continue;
                    var serializer = hit.GetComponent<BoltEntity>();
                    if (serializer != null && serializer.IsAttached)
                    {
                        TakeDamage newEvent = TakeDamage.Create(serializer);
                        newEvent.Amount = 1;
                        newEvent.Send();
                    }
                }
            }
       // }


        /*Collider[] colliders = Physics.OverlapSphere(weaponPosition.position, attackRadius, layerMask);
        if (mainPlayerMovementFSM.currentState == mainPlayerMovementFSM.AttackingReadyState && colliders.Length > 0)
        {
            foreach (Collider collider in colliders)
            {
                if (collider.gameObject == gameObject) continue;

                //takeDamage(1, collider.gameObject);
                takeDamage(1, collider.gameObject);
            }
        }*/
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
}
