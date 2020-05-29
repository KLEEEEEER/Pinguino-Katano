using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUIManager : MonoBehaviour
{
    [SerializeField] GameObject scoreboard;

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            scoreboard.SetActive(!scoreboard.activeSelf);
        }
    }
}
