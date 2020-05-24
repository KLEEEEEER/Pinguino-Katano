using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;
using PinguinoKatano.Network;

public class Health : Bolt.EntityEventListener<IPenguinState>
{
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    [SerializeField] private bool startWithMaxHealth;
    [SerializeField] private GameObject bodyToDiactivate;
    [SerializeField] private Collider colliderToDiactivate;

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

        NetworkCallbacks.AllPlayers.Add(this);
        state.AddCallback("Health", HealthChangedCallback);
    }

    public override void Detached()
    {
        NetworkCallbacks.AllPlayers.Remove(this);
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
        if (entity)
        {
            //BoltNetwork.Destroy(entity.gameObject);
            bodyToDiactivate.SetActive(false);
            colliderToDiactivate.enabled = false;
            state.IsDead = true;
            state.RespawnFrame = BoltNetwork.ServerFrame + (4 * BoltNetwork.FramesPerSecond);
        }
    }

    public void Spawn()
    {
        if (entity)
        {
            state.IsDead = false;
            state.Health = 100;
            bodyToDiactivate.SetActive(true);
            colliderToDiactivate.enabled = true;
            // teleport
            entity.transform.position = RandomSpawn();
        }
    }

    static Vector3 RandomSpawn()
    {
        float x = Random.Range(-16f, 16f);
        float z = Random.Range(-16f, 16f);
        return new Vector3(x, 1f, z);
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
