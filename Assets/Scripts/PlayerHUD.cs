using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour, IKillable, IRespawnable
{
    public Text scoreText;
    public Text livesText;
    public TankData data;
    public GameObject gameOverScreen;

    public void OnKilled(Attack attackData)
    {
        // If the player is not out of lives

        // If the player is out of lives
        gameOverScreen.SetActive(true);
    }

    public void OnRespawn()
    {
        gameOverScreen.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        //Attaches this playerhud to the game manager the moment it exists
        GameManager.Instance.hud = this;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = GameManager.Instance.oldPlayerScore.ToString();
        livesText.text = data.lives.ToString();
    }
}
