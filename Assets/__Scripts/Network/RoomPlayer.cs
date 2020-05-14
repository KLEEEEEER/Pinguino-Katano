using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PinguinoKatano.Network
{
    public class RoomPlayer : NetworkBehaviour
    {
        [SerializeField] private GameObject lobbyUI;

        [SyncVar(hook = nameof(HandleDisplayNameChanged))]
        public string DisplayName = "LOH";
        [SyncVar(hook = nameof(HandleReadyStatusChanged))]
        public bool IsReady = false;

        PlayerInList playerInListComponent;

        private bool isLeader;
        public bool IsLeader
        {
            set
            {
                isLeader = value;
                //startGameButton.gameObject.SetActive(value);
            }
        }

        private PinguinoKatanoNetworkManager room;
        private PinguinoKatanoNetworkManager Room
        {
            get
            {
                if (room != null) { return room; }
                return room = NetworkManager.singleton as PinguinoKatanoNetworkManager;
            }
        }

        public override void OnStartAuthority()
        {
            CmdSetDisplayName(PlayerNameInput.DisplayName);

            InstantiatePlayerInfo();
        }

        public override void OnStartClient()
        {
            Room.RoomPlayers.Add(this);

            if (!hasAuthority)
            {
                InstantiatePlayerInfo();
            }
            else
            {
                MainMenuUI.Instance.OnReadyButtonClicked += CmdReadyUp;
            }
        }
        public override void OnStopClient()
        {
            Room.RoomPlayers.Remove(this);
            Destroy(playerInListComponent.gameObject);
            return;
        }

        private void InstantiatePlayerInfo()
        {
            GameObject playerInfo = Instantiate(Room.PlayerLobbyInfoPrefab, Room.PlayerListLobbyRoot.transform);
            playerInListComponent = playerInfo.GetComponent<PlayerInList>();
            if (hasAuthority)
                CmdUpdatePlayerInfo();
        }

        [Command]
        private void CmdSetDisplayName(string newName)
        {
            DisplayName = newName;
        }

        public void HandleDisplayNameChanged(string oldValue, string newValue) => CmdUpdatePlayerInfo();
        public void HandleReadyStatusChanged(bool oldValue, bool newValue) => CmdUpdatePlayerInfo();

        [Command]
        public void CmdUpdatePlayerInfo()
        {
            RpcUpdatePlayerInfoOnClients();
        }

        [ClientRpc]
        public void RpcUpdatePlayerInfoOnClients()
        {
            if (playerInListComponent != null)
            {
                playerInListComponent.PlayerName.text = DisplayName;
                playerInListComponent.SetReady(IsReady);
            }
        }

        [Command]
        public void CmdReadyUp()
        {
            RpcReadyUp();
            //Room.NotifyPlayersOfReadyState();
        }
        [ClientRpc]
        public void RpcReadyUp()
        {
            IsReady = !IsReady;
        }

        private void OnDestroy()
        {
            MainMenuUI.Instance.OnReadyButtonClicked -= CmdReadyUp;
        }
    }
}