using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUIManager : MonoBehaviour
{
    [SerializeField] Text healthText;

    #region Singleton
    public static MainUIManager Instance;

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
    #endregion

    public void ChangeHealthString(string text)
    {
        healthText.text = text;
    }
}
