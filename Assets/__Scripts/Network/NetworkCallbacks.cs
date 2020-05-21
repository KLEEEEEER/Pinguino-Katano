using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;

namespace PinguinoKatano.Network
{
    [BoltGlobalBehaviour]
    public class NetworkCallbacks : GlobalEventListener
    {
        public override void SceneLoadLocalDone(string scene)
        {
            // randomize a position
            var spawnPosition = new Vector3(Random.Range(-16, 16), 0, Random.Range(-16, 16));

            // instantiate cube
            BoltNetwork.Instantiate(BoltPrefabs.MainPlayer, spawnPosition, Quaternion.identity);
        }
    }
}