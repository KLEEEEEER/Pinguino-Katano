using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;

public class Health : Bolt.EntityEventListener<IPenguinState>
{
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    [SerializeField] private bool startWithMaxHealth;

    public override void Attached()
    {
        if (entity.IsOwner)
        {
            if (startWithMaxHealth)
                //health = maxHealth;
                state.Health = maxHealth;
            else
                state.Health = health;
        }

        state.AddCallback("Health", HealthChangedCallback);
    }

    public void HealthChangedCallback()
    {
        health = state.Health;

        if (state.Health <= 0)
        {
            Dead();
        }
    }

    private void Dead()
    {
        BoltNetwork.Destroy(gameObject);
        //state.IsDead = true;
        //state.respawnFrame = BoltNetwork.ServerFrame + (4 * BoltNetwork.FramesPerSecond);
    }

    public float Current { get => state.Health; }
    public void Remove(int amount)
    {
        if (entity.IsOwner)
            state.Health -= amount;
    }

    public override void OnEvent(TakeDamage evnt)
    {
        if (evnt.Amount > 0)
            Remove(evnt.Amount);
    }
}
