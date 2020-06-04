using Bolt.Matchmaking;
using System.Collections;
using System.Collections.Generic;
using UdpKit;
using UnityEngine;
using UnityEngine.UI;

namespace PinguinoKatano.Network
{
    [RequireComponent(typeof(Button))]
    public class GameSessionButton : MonoBehaviour
    {
        public UdpSession sessionObject;
        public Text sessionName;

        public void SetSessionInfo(UdpSession _sessionObject)
        {
            sessionName.text = _sessionObject.HostName;
            sessionObject = _sessionObject;
            GetComponent<Button>().onClick.AddListener(StartSession);
        }

        public void StartSession()
        {
            BoltMatchmaking.JoinSession(sessionObject);
        }
    }
}