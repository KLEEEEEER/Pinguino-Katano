using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject changeNameScreen;
    [SerializeField] private GameObject mainTitleScreen;
    [SerializeField] private GameObject createLobbyScreen;
    [SerializeField] private GameObject joinLobbyScreen;
    [SerializeField] private GameObject[] screensToDisableFunction;

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
