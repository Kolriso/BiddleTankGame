﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionsMenu;

    public Slider musicSlider;
    public Slider sfxSlider;
    public Dropdown mapDropdown;

    public enum MenuState { mainMenu, optionsMenu };
    public MenuState currentMenuState = MenuState.mainMenu;

    //Call the attached Audiosource
    private AudioSource audioSource;

    private void Start()
    {
        //Attach audiosource to gameobjects audiosource
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

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

    public void SwapMultiplayer()
    {
        GameManager.Instance.isMultiplayer = !GameManager.Instance.isMultiplayer;
    }
    public void ChangeState(MenuState nextState)
    {
        currentMenuState = nextState;
    }

    public void OnSaveChangesClicked()
    {
        // Save options
        GameManager.Instance.SavePreferences();

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
        // Revert options
        GameManager.Instance.LoadPreferences();

        // Go back to main menu
        GoToMainFromOptions();
    }
    public void OnChangeMusicVolume()
    {
        GameManager.Instance.musicVolume = musicSlider.value;
    }

    public void OnChangeSFXVolume()
    {
        GameManager.Instance.sfxVolume = sfxSlider.value;
    }

    public void OnChangeMapType()
    {
        GameManager.Instance.mapType = (GameManager.MapGenerationType)mapDropdown.value;
    }
}
