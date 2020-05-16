using PinguinoKatano.Core.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private float attackRadius = 2f;
    [SerializeField] MainPlayerMovementFSM mainPlayerMovementFSM;
    [SerializeField] LayerMask layerMask;
    [SerializeField] Transform weaponPosition;

    private void FixedUpdate()
    {
        Collider[] colliders = Physics.OverlapSphere(weaponPosition.position, attackRadius, layerMask);
        if (mainPlayerMovementFSM.currentState == mainPlayerMovementFSM.AttackingReadyState && colliders.Length > 0)
        {
            foreach (Collider collider in colliders)
            {
                Damageable player = collider.gameObject.GetComponent<Damageable>();
                if (player != null)
                {
                    Debug.Log("Attacking player");
                    player.TakeDamage(1);
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
