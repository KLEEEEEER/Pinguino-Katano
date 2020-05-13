﻿using PinguinoKatano.Network;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject changeNameScreen;
    [SerializeField] private GameObject mainTitleScreen;
    [SerializeField] private GameObject createLobbyScreen;
    [SerializeField] private GameObject joinLobbyScreen;
    [SerializeField] private GameObject newLobbyScreen;
    [SerializeField] private GameObject[] screensToDisableFunction;

    [SerializeField] private PinguinoKatanoNetworkManager networkManager = null;

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
    }

    public void OnMainMenu_CreateLobbyClick()
    {
        networkManager.StartHost();
        DisableAllScreens();
        newLobbyScreen.SetActive(true);
    }

    public void BackToMainMenuClick()
    {
        DisableAllScreens();
        mainTitleScreen.SetActive(true);
    }

    public void DisableAllScreens()
    {
        foreach (GameObject screen in screensToDisableFunction)
        {
            screen.SetActive(false);
        }
    }
}
