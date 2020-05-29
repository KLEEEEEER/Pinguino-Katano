using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;
using UnityEngine.SceneManagement;

namespace PinguinoKatano.Network
{
    [BoltGlobalBehaviour]
    public class NetworkCallbacks : GlobalEventListener
    {
        public static List<Health> AllPlayers = new List<Health>();

        public override void SceneLoadLocalDone(string scene)
        {
            var spawnPosition = Health.RandomSpawn();

            // instantiate cube
            BoltNetwork.Instantiate(BoltPrefabs.MainPlayer, spawnPosition, Quaternion.identity);
        }

        List<string> logMessages = new List<string>();

        void OnGUI()
        {
            // only display max the 5 latest log messages
            int maxMessages = Mathf.Min(5, logMessages.Count);

            GUILayout.BeginArea(new Rect(Screen.width / 2 - 200, Screen.height - 100, 400, 100), GUI.skin.box);

            for (int i = 0; i < maxMessages; ++i)
            {
                GUILayout.Label(logMessages[i]);
            }

            GUILayout.EndArea();
        }

        public override void OnEvent(LogEvent evnt)
        {
            logMessages.Insert(0, evnt.Message);
        }

        private void FixedUpdate()
        {
            foreach (Health player in AllPlayers)
            {
                if (player.entity && player.state.IsDead && player.state.RespawnFrame <= BoltNetwork.ServerFrame)
                {
                    player.Spawn();
                }
            }
        }

        

        public override void Disconnected(BoltConnection connection)
        {
            SceneManager.LoadScene(0);
        }
    }
}