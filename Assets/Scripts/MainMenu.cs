using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionsMenu;

    public enum MenuState { mainMenu, optionsMenu };
    public MenuState currentMenuState = MenuState.mainMenu;
    public void OnQuitClicked()
    {
        Application.Quit();
    }

    public void OnNewGameClicked()
    {
        SceneManager.LoadScene(2);
    }

    public void OnOptionsClicked()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
        ChangeState(MenuState.optionsMenu);
    }

    public void ChangeState(MenuState nextState)
    {
        currentMenuState = nextState;
    }

    public void OnSaveChangesClicked()
    {
        //TODO: Save options

        // Go back to main menu
        GoToMainFromOptions();
    }

    private void GoToMainFromOptions()
    {
        optionsMenu.SetActive(false);
        mainMenu.SetActive(true);
        ChangeState(MenuState.mainMenu);
    }

    public void OnClickedBackInOptions()
    {
        //TODO: Revert options

        // Go back to main menu
        GoToMainFromOptions();
    }
}
