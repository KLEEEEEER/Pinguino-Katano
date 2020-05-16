using PinguinoKatano.Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject changeNameScreen;
    [SerializeField] private GameObject mainTitleScreen;
    [SerializeField] private GameObject createLobbyScreen;
    [SerializeField] private GameObject joinLobbyScreen;
    [SerializeField] private GameObject newLobbyScreen;
    [SerializeField] private GameObject[] screensToDisableFunction;
    [SerializeField] private GameObject searchingLobbyText;

    public event Action OnReadyButtonClicked;

    [SerializeField] private PinguinoKatanoNetworkManager networkManager = null;

    private void Start()
    {
        PinguinoKatanoNetworkManager.OnClientDisconnected += NetworkManager_OnClientDisconnected;
        PinguinoKatanoNetworkManager.OnClientConnected += NetworkManager_OnClientConnected;
    }

    #region Singleton
    private static MainMenuUI instance;
    public static MainMenuUI Instance
    {
        get => instance;
        set
        {
            instance = value;
        }
    }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    #endregion

    public void OnReadyButtonClickedEvent()
    {
        OnReadyButtonClicked?.Invoke();
    }
    public void OnClickContinueNameButton()
    {
        if (PlayerNameInput.isValidateDisplayName())
        {
            DisableAllScreens();
            mainTitleScreen.SetActive(true);
        }
    }

    public void OnMainMenu_CreateClick()
    {
        DisableAllScreens();
        createLobbyScreen.SetActive(true);
    }

    public void OnMainMenu_JoinClick()
    {
        DisableAllScreens();
        joinLobbyScreen.SetActive(true);
    }

    public void OnMainMenu_JoinLobbyClick()
    {
        DisableAllScreens();
        networkManager.StartClient();
        newLobbyScreen.SetActive(true);
        searchingLobbyText.SetActive(true);
    }

    public void OnMainMenu_CreateLobbyClick()
    {
        networkManager.StartHost();
        DisableAllScreens();
        newLobbyScreen.SetActive(true);
        searchingLobbyText.SetActive(false);
    }

    public void BackFromCreatingLobby()
    {
        networkManager.StopHost();
        DisableAllScreens();
        mainTitleScreen.SetActive(true);
    }

    public void BackFromFoundingLobby()
    {
        networkManager.StopClient();
        DisableAllScreens();
        mainTitleScreen.SetActive(true);
    }

    public void BackToMainMenuClick()
    {
        networkManager.StopClient();
        networkManager.StopHost();
        DisableAllScreens();
        mainTitleScreen.SetActive(true);
    }

    private void NetworkManager_OnClientDisconnected()
    {
        DisableAllScreens();
        mainTitleScreen.SetActive(true);
    }

    private void NetworkManager_OnClientConnected()
    {
        searchingLobbyText.SetActive(false);
    }

    public void DisableAllScreens()
    {
        foreach (GameObject screen in screensToDisableFunction)
        {
            screen.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        PinguinoKatanoNetworkManager.OnClientDisconnected -= NetworkManager_OnClientDisconnected;
    }
}
