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

    private void Start()
    {

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

        newLobbyScreen.SetActive(true);
        searchingLobbyText.SetActive(true);
    }

    public void OnMainMenu_CreateLobbyClick()
    {

        DisableAllScreens();
        newLobbyScreen.SetActive(true);
        searchingLobbyText.SetActive(false);
    }

    public void BackFromCreatingLobby()
    {

        DisableAllScreens();
        mainTitleScreen.SetActive(true);
    }

    public void BackFromFoundingLobby()
    {

        DisableAllScreens();
        mainTitleScreen.SetActive(true);
    }

    public void BackToMainMenuClick()
    {

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

    }
}
