using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;
using PinguinoKatano.Network;
using PinguinoKatano.UI;

[RequireComponent(typeof(Rigidbody))]
public class Health : Bolt.EntityEventListener<IPenguinState>
{
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    [SerializeField] private bool startWithMaxHealth;
    [SerializeField] private GameObject bodyToDiactivate;
    [SerializeField] private Collider colliderToDiactivate;
    [SerializeField] private PlayerInfo playerInScoreboard;
    private Rigidbody rigidbody;
    private bool isDead = false;

    public void SetPlayerInfo(PlayerInfo playerInfo)
    {
        playerInScoreboard = playerInfo;
    }

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public override void Attached()
    {
        if (entity.IsOwner)
        {
            state.PlayerName = PlayerNameInput.DisplayName;

            if (startWithMaxHealth)
                //health = maxHealth;
                state.Health = maxHealth;
            else
                state.Health = health;
        }

        NetworkCallbacks.AllPlayers.Add(this);
        playerInScoreboard = PlayersList.Instance.AddNewPlayerInfo(entity, state.PlayerName);
        state.AddCallback("Health", HealthChangedCallback);
        state.AddCallback("IsDead", IsDeadChangedCallback);
        state.AddCallback("PlayerName", KDChangedCallback);
        state.AddCallback("Kills", KDChangedCallback);
        state.AddCallback("Deaths", KDChangedCallback);
    }

    public override void Detached()
    {
        PlayersList.Instance.RemovePlayerInfo(entity);
        NetworkCallbacks.AllPlayers.Remove(this);
        playerInScoreboard = null;
    }

    public void HealthChangedCallback()
    {
        health = state.Health;

        /*if (state.Health <= 0)
        {
            Dead();
        }*/
    }

    public void KDChangedCallback()
    {
        KDChanged newEvent = KDChanged.Create();
        newEvent.Entity = entity;
        newEvent.Send();
    }

    public void IsDeadChangedCallback()
    {
        isDead = state.IsDead;

        if (isDead)
        {
            bodyToDiactivate.SetActive(false);
        }
        else
        {
            bodyToDiactivate.SetActive(true);
        }
    }

    private void Dead()
    {
        if (entity)
        {
            colliderToDiactivate.enabled = false;
            rigidbody.isKinematic = true;
            state.IsDead = true;
            state.RespawnFrame = BoltNetwork.ServerFrame + (4 * BoltNetwork.FramesPerSecond);
        }
    }

    public void Spawn()
    {
        if (entity.IsOwner)
        {
            entity.transform.position = RandomSpawn();
        }

        if (entity)
        {
            state.IsDead = false;
            state.Health = maxHealth;
            colliderToDiactivate.enabled = true;
            rigidbody.isKinematic = false;
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

       if (entity.IsOwner)
        {
            if (health <= 0 && !isDead)
            {
                Killed.Create(evnt.From).Send();
                state.Deaths += 1;
                isDead = true;
                Dead();
            }
        }
       
    }
}
