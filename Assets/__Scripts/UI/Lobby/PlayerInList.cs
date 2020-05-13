using UnityEngine;
using UnityEngine.UI;

public class PlayerInList : MonoBehaviour
{
    public Text PlayerName;
    public Text Ready;

    public void SetReady(bool isReady)
    {
        Ready.text = (isReady) ? "Ready" : "Not ready";
    }
}
