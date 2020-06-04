using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;
using PinguinoKatano.Network;
using PinguinoKatano.UI;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class Health : Bolt.EntityEventListener<IPenguinState>
{
    [SerializeField] private float respawnTime = 4f;
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    [SerializeField] private bool startWithMaxHealth;
    [SerializeField] private GameObject bodyToDiactivate;
    [SerializeField] private Collider colliderToDiactivate;
    [SerializeField] private PlayerInfo playerInScoreboard;
    [SerializeField] private RectTransform currentHealthBar;
    [SerializeField] private Text nameHealthBar;
    [SerializeField] private GameObject ragdollPrefab;
    [SerializeField] private Vector3 damagePosition = Vector3.zero;
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
        state.AddCallback("Health", HealthChangedCallback);
        state.AddCallback("IsDead", IsDeadChangedCallback);
        state.AddCallback("PlayerName", KDChangedCallback);
        state.AddCallback("PlayerName", HealthbarNameChangedCallback);
        state.AddCallback("Kills", KDChangedCallback);
        state.AddCallback("Deaths", KDChangedCallback);

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
    }

    public override void Detached()
    {
        if (PlayersList.Instance != null)
            PlayersList.Instance.RemovePlayerInfo(entity);
        NetworkCallbacks.AllPlayers.Remove(this);
        playerInScoreboard = null;
    }

    public void HealthChangedCallback()
    {
        health = state.Health;
        UpdateHealthBarValue();

        /*if (state.Health <= 0)
        {
            Dead();
        }*/
    }

    public void HealthbarNameChangedCallback()
    {
        nameHealthBar.text = state.PlayerName;
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
            colliderToDiactivate.enabled = false;
            rigidbody.isKinematic = true;
            StartCoroutine(spawnRagdoll());
        }
        else
        {
            bodyToDiactivate.SetActive(true);
            colliderToDiactivate.enabled = true;
            rigidbody.isKinematic = false;
        }
    }

    private void Dead()
    {
        if (entity)
        {
            state.IsDead = true;
            state.RespawnFrame = BoltNetwork.ServerFrame + ((int)respawnTime * BoltNetwork.FramesPerSecond);
        }
    }

    IEnumerator spawnRagdoll()
    {
        /*BoltEntity ragdoll = BoltNetwork.Instantiate(ragdollPrefab, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(respawnTime);
        BoltNetwork.Destroy(ragdoll);*/
        Quaternion ragdollRotation = transform.rotation;
        ragdollRotation.eulerAngles += new Vector3(0, 180f, 0);
        GameObject ragdoll = Instantiate(ragdollPrefab, transform.position, ragdollRotation);
        Rigidbody rigidbody = ragdoll.GetComponent<Rigidbody>();
        if (rigidbody != null)
        {
            if (damagePosition != Vector3.zero)
            {
                rigidbody.AddForce(transform.position - damagePosition, ForceMode.Impulse);
                damagePosition = Vector3.zero;
            }
        } 
        yield return new WaitForSeconds(respawnTime);
        Destroy(ragdoll);
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
            UpdateHealthBarValue();
        }
    }

    public static Vector3 RandomSpawn()
    {
        float x = Random.Range(-5f, 5f);
        float z = Random.Range(-5f, 5f);
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
        damagePosition = evnt.From.transform.position;

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

    private void UpdateHealthBarValue()
    {
        Vector3 tempAnchorMax = currentHealthBar.anchorMax;
        float value = (float)health / maxHealth;
        tempAnchorMax.x = value;
        currentHealthBar.anchorMax = tempAnchorMax;
    }
}
