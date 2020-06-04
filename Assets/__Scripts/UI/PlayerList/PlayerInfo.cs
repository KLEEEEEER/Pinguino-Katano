using UnityEngine;
using UnityEngine.UI;
using Bolt;

namespace PinguinoKatano.UI
{
    public class PlayerInfo : MonoBehaviour
    {
        [SerializeField] private Text PlayerNameText;
        [SerializeField] private Text PlayerKillsText;
        [SerializeField] private Text PlayerDeathsText;

        public void SetPlayerNameText(string text)
        {
            PlayerNameText.text = text;
        }
        public void SetPlayerKillsText(string text)
        {
            PlayerKillsText.text = text;
        }
        public void SetPlayerDeathsText(string text)
        {
            PlayerDeathsText.text = text;
        }
    }
}