using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour, IKillable
{
    public Text scoreText;
    public GameObject gameOverScreen;

    public void OnKilled(Attack attackData)
    {
        gameOverScreen.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = GameManager.Instance.oldPlayerScore.ToString();
    }
}
