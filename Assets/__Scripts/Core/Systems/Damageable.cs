using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] Color deadColor;
    [SerializeField] Renderer meshRenderer;
    public void TakeDamage(float amount)
    {
        health.Remove(amount);
            //MainUIManager.Instance.ChangeHealthString(health.ToString());

        if (health.Current <= 0)
        {
            meshRenderer.material.color = deadColor;
            Debug.Log("Player is dead");
        }
    }
}
