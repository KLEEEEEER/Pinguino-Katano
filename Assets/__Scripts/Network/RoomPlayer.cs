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

        public bool IsReady = false;

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
            MainMenuUI.Instance.DisableAllScreens();
            lobbyUI.SetActive(true);
        }

        [Command]
        private void CmdSetDisplayName(string newName)
        {
            DisplayName = newName;
        }

        public void HandleDisplayNameChanged(string oldValue, string newValue) => UpdateDisplay();

        private void UpdateDisplay()
        {

        }
    }
}