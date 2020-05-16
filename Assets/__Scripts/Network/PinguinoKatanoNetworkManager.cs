using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PinguinoKatano.Network
{
    public class PinguinoKatanoNetworkManager : NetworkManager
    {
        [SerializeField] private int minPlayers = 2;
        [Scene] [SerializeField] private string menuScene = string.Empty;

        [Header("Maps")]
        [SerializeField] private int numberOfPoints = 1;
        [SerializeField] private Map[] maps = null;

        [Header("Room")]
        [SerializeField] private RoomPlayer roomPlayerPrefab = null;
        [SerializeField] public GameObject PlayerListLobbyRoot;
        [SerializeField] public GameObject PlayerLobbyInfoPrefab;

        [Header("Game")]
        [SerializeField] private GamePlayer gamePlayerPrefab = null;
        [SerializeField] private GameObject playerSpawnSystem = null;
        [SerializeField] private GameObject roundSystem = null;

        private MapHandler mapHandler;

        public static event Action OnClientConnected;
        public static event Action OnClientDisconnected;
        public static event Action<NetworkConnection> OnServerReadied;
        public static event Action OnServerStopped;

        public List<RoomPlayer> RoomPlayers { get; } = new List<RoomPlayer>();
        public List<RoomPlayer> GamePlayers { get; } = new List<RoomPlayer>();

        public override void OnStartServer() 
        { 
            spawnPrefabs = Resources.LoadAll<GameObject>("SpawnablePrefabs").ToList();
            //UpdatePlayersList();
        }

        public override void OnStartClient()
        {
            var spawnablePrefabs = Resources.LoadAll<GameObject>("SpawnablePrefabs");
            foreach (var prefab in spawnablePrefabs)
            {
                ClientScene.RegisterPrefab(prefab);
            }
            UpdatePlayersListOnClients();
        }

        public override void OnClientConnect(NetworkConnection conn)
        {
            base.OnClientConnect(conn);
            //UpdatePlayersList();
            UpdatePlayersListOnClients();
            OnClientConnected?.Invoke();
        }

        public override void OnClientDisconnect(NetworkConnection conn)
        {
            base.OnClientDisconnect(conn);
            //UpdatePlayersList();
            UpdatePlayersListOnClients();
            OnClientDisconnected?.Invoke();
        }

        public override void OnServerConnect(NetworkConnection conn)
        {
            if (numPlayers >= maxConnections)
            {
                conn.Disconnect();
                Debug.LogError("numPlayers >= maxConnections");
                return;
            }

            if (SceneManager.GetActiveScene().path != menuScene)
            {
                conn.Disconnect();
                Debug.LogError($"{SceneManager.GetActiveScene().path} != {menuScene}");
                return;
            }
            UpdatePlayersListOnClients();
            //UpdatePlayersList();
        }

        public override void OnServerAddPlayer(NetworkConnection conn)
        {
            if (SceneManager.GetActiveScene().path == menuScene)
            {
                bool isLeader = RoomPlayers.Count == 0;

                RoomPlayer roomPlayerInstance = Instantiate(roomPlayerPrefab);

                roomPlayerInstance.IsLeader = isLeader;

                NetworkServer.AddPlayerForConnection(conn, roomPlayerInstance.gameObject);
                UpdatePlayersListOnClients();
                //UpdatePlayersList();
            }
        }

        public override void OnServerDisconnect(NetworkConnection conn)
        {
            if (conn.identity != null)
            {
                var player = conn.identity.GetComponent<RoomPlayer>();

                RoomPlayers.Remove(player);

                NotifyPlayersOfReadyState();

                UpdatePlayersListOnClients();
                //UpdatePlayersList();
            }

            base.OnServerDisconnect(conn);
        }

        public override void OnStopServer()
        {
            OnServerStopped?.Invoke();

            //UpdatePlayersList();

            RoomPlayers.Clear();
            GamePlayers.Clear();
        }

        public void NotifyPlayersOfReadyState()
        {
            foreach (var player in RoomPlayers)
            {
                //player.HandleReadyToStart(IsReadyToStart());
            }
        }

        private bool IsReadyToStart()
        {
            if (numPlayers < minPlayers) { return false; }

            foreach (var player in RoomPlayers)
            {
                if (!player.IsReady) { return false; }
            }

            return true;
        }

        public void StartGame()
        {
            if (SceneManager.GetActiveScene().path == menuScene)
            {
                if (!IsReadyToStart()) { return; }

                mapHandler = new MapHandler(maps, numberOfPoints);

                ServerChangeScene(mapHandler.NextMap);
            }
        }

        public override void ServerChangeScene(string newSceneName)
        {
            if (SceneManager.GetActiveScene().path == menuScene && newSceneName.StartsWith("map_"))
            {
                for (int i = RoomPlayers.Count - 1; i >= 0; i--)
                {
                    var conn = RoomPlayers[i].connectionToClient;
                    var gameplayerInstance = Instantiate(gamePlayerPrefab);
                    //gameplayerInstance.SetDisplayName(RoomPlayers[i].DisplayName);

                    NetworkServer.Destroy(conn.identity.gameObject);

                    NetworkServer.ReplacePlayerForConnection(conn, gameplayerInstance.gameObject);
                }
            }

            base.ServerChangeScene(newSceneName);
        }

        public override void OnServerSceneChanged(string sceneName)
        {
            if (sceneName.StartsWith("map_"))
            {
                GameObject playerSpawnSystemInstance = Instantiate(playerSpawnSystem);
                NetworkServer.Spawn(playerSpawnSystemInstance);

                GameObject roundSystemInstance = Instantiate(roundSystem);
                NetworkServer.Spawn(roundSystemInstance);
            }
        }

        /*public void ClearPlayersList()
        {
            foreach (Transform child in PlayerListLobbyRoot.transform)
            {
                Destroy(child.gameObject);
            }
        }

        public void UpdatePlayersList()
        {
            ClearPlayersList();
            foreach (RoomPlayer roomPlayer in RoomPlayers)
            {
                GameObject playerInfo = Instantiate(PlayerLobbyInfoPrefab, PlayerListLobbyRoot.transform);
                PlayerInList playerInListComponent = playerInfo.GetComponent<PlayerInList>();
                if (playerInListComponent != null)
                {
                    playerInListComponent.PlayerName.text = roomPlayer.DisplayName;
                    playerInListComponent.SetReady(roomPlayer.IsReady);
                }
            }
            Debug.Log("Updated players list. Players in RoomPlayers list " + RoomPlayers.Count);
        }*/

        public void UpdatePlayersListOnClients()
        {
            foreach (RoomPlayer roomPlayer in RoomPlayers)
            {
                roomPlayer.UpdatePlayersList();
            }
        }

        public override void OnServerReady(NetworkConnection conn)
        {
            base.OnServerReady(conn);
            //UpdatePlayersList();
            UpdatePlayersListOnClients();
            OnServerReadied?.Invoke(conn);
        }
    }
}