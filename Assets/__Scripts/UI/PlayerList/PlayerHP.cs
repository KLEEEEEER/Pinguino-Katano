using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] private Text PlayerNameText;
    [SerializeField] private Text PlayerHPText;

    public void SetPlayerNameText(string text)
    {
        PlayerNameText.text = text;
    }
    public void SetPlayerHPText(string text)
    {
        PlayerHPText.text = text;
    }
}
