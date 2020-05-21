﻿using Bolt;
using Bolt.Matchmaking;
using PinguinoKatano.Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UdpKit;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : GlobalEventListener
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
        BoltLauncher.StartClient();
        newLobbyScreen.SetActive(true);
        searchingLobbyText.SetActive(true);
    }

    public void OnMainMenu_CreateLobbyClick()
    {
        BoltLauncher.StartServer();
        DisableAllScreens();
        newLobbyScreen.SetActive(true);
        searchingLobbyText.SetActive(false);
    }

    public override void BoltStartDone()
    {
        if (BoltNetwork.IsServer)
        {
            string matchName = Guid.NewGuid().ToString();

            BoltMatchmaking.CreateSession(
                sessionID: matchName,
                sceneToLoad: "map_SnowTemple"
            );
        }
    }

    public override void SessionListUpdated(Map<Guid, UdpSession> sessionList)
    {
        Debug.LogFormat("Session list updated: {0} total sessions", sessionList.Count);

        foreach (var session in sessionList)
        {
            UdpSession photonSession = session.Value as UdpSession;

            if (photonSession.Source == UdpSessionSource.Photon)
            {
                BoltMatchmaking.JoinSession(photonSession);
            }
        }
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

    /*private void NetworkManager_OnClientDisconnected()
    {
        DisableAllScreens();
        mainTitleScreen.SetActive(true);
    }

    private void NetworkManager_OnClientConnected()
    {
        searchingLobbyText.SetActive(false);
    }*/

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
