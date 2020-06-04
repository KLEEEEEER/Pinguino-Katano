using PinguinoKatano.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;
using PinguinoKatano.Network;

public class PlayersList : GlobalEventListener
{
    //private List<PlayerInfo> players = new List<PlayerInfo>();
    private Dictionary<BoltEntity, PlayerInfo> players = new Dictionary<BoltEntity, PlayerInfo>();
    [SerializeField] private GameObject playerInfoPrefab;

    public static PlayersList Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    public PlayerInfo AddNewPlayerInfo(BoltEntity boltEntity, string playerName)
    {
        GameObject newPlayerInfo = Instantiate(playerInfoPrefab, transform);
        PlayerInfo playerInfo = newPlayerInfo.GetComponent<PlayerInfo>();
        playerInfo.SetPlayerNameText(playerName);
        playerInfo.SetPlayerKillsText("0");
        playerInfo.SetPlayerDeathsText("0");
        players.Add(boltEntity, playerInfo);
        return playerInfo;
    }

    public void RemovePlayerInfo(BoltEntity boltEntity)
    {
        Destroy(players[boltEntity].gameObject);
        players.Remove(boltEntity);
    }

    public override void OnEvent(KDChanged evnt)
    {
        foreach (Health player in NetworkCallbacks.AllPlayers)
        {
            PlayerInfo playerInfo = players[player.entity];
            playerInfo.SetPlayerNameText(player.state.PlayerName);
            playerInfo.SetPlayerKillsText(player.state.Kills.ToString());
            playerInfo.SetPlayerDeathsText(player.state.Deaths.ToString());

            Debug.Log(player.state.PlayerName + " has now " + player.state.Kills.ToString() + " kills!");
        }
    }
}
