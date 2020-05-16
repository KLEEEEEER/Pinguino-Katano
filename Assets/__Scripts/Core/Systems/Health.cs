using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;
    [SerializeField] private bool startWithMaxHealth;

    private void Start()
    {
        if (startWithMaxHealth)
            health = maxHealth;
    }

    public float Current { get => health; }
    public void Remove(float amount)
    {
        health -= amount;
    }
}
