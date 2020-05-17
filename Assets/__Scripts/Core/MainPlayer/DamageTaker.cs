using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageTaker
{
    [SerializeField] private int health;
    [SerializeField] Color deadColor;
    [SerializeField] Renderer meshRenderer;
    public void TakeDamage(int amount)
    {
        health -= amount;
            //MainUIManager.Instance.ChangeHealthString(health.ToString());

        if (health <= 0)
        {
            meshRenderer.material.color = deadColor;
            Debug.Log("Player is dead");
        }
    }
}
