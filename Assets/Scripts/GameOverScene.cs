using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScene : MonoBehaviour
{
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
}
