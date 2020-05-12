using Mirror;
using PinguinoKatano.Core.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MainPlayerMovementFSM))]
public class MainPlayerAttack : NetworkBehaviour
{
    [SerializeField] MainPlayerMovementFSM mainPlayerMovementFSM;
    [SerializeField] private float attackRadius = 2f;
    [SerializeField] LayerMask layerMask;
    [SerializeField] Transform weaponPosition;

    void FixedUpdate()
    {
        if (!isLocalPlayer) return;

        if (mainPlayerMovementFSM.currentState != mainPlayerMovementFSM.AttackingReadyState) return;

        Collider[] colliders = Physics.OverlapSphere(weaponPosition.position, attackRadius, layerMask);
        if (mainPlayerMovementFSM.currentState == mainPlayerMovementFSM.AttackingReadyState && colliders.Length > 0)
        {
            foreach (Collider collider in colliders)
            {
                if (collider.gameObject == gameObject) continue;

                //takeDamage(1, collider.gameObject);
                CmdTakeDamageOnServer(1, collider.gameObject);
            }
        }
    }

    [Command]
    public void CmdTakeDamageOnServer(int amount, GameObject _gameObject)
    {
        RpcTakeDamageOnServer(amount, _gameObject);
    }

    [ClientRpc]
    public void RpcTakeDamageOnServer(int amount, GameObject _gameObject)
    {
        takeDamage(amount, _gameObject);
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
