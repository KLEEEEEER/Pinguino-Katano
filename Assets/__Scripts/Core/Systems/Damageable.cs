using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : NetworkBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] Color deadColor;
    [SerializeField] Renderer meshRenderer;
    public void TakeDamage(float amount)
    {
        health.Remove(amount);
        if (isLocalPlayer)
            MainUIManager.Instance.ChangeHealthString(health.ToString());

        if (health.Current <= 0)
        {
            meshRenderer.material.color = deadColor;
            Debug.Log("Player is dead");
        }
    }
}
