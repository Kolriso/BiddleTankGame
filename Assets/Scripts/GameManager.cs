﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public GameObject playerPrefab;

    public GameObject[] Players = new GameObject[2];

    public GameObject[] EnemyAIPrefabs;

    public List<GameObject> healthPowerups = new List<GameObject>();

    public List<EnemySpawnPoints> enemySpawnPoints = new List<EnemySpawnPoints>();

    public List<PlayerSpawnPoints> playerSpawnPoints = new List<PlayerSpawnPoints>();

    public int oldPlayerScore = 0;

    public int player1Score;
    public int player2Score;

    public List<ScoreData> HighScoreTable;

    public bool isMultiplayer;

    //Calls the player hudto activate on death
    public PlayerHUD hud;

   // public int[] lives = new int[2];

    public enum MapGenerationType { Random, MapOfTheDay, CustomSeed };
    public MapGenerationType mapType = MapGenerationType.Random;

    public float musicVolume;
    public float sfxVolume;


    protected override void Awake()
    {
        LoadPreferences();
        base.Awake();
    }

    private void Start()
    {
        /*
        lives[0] = 3;
        if (isMultiplayer)
        {
            lives[1] = 3;
        }
        else
        {
            lives[1] = 0;
        }
        */
        SceneManager.LoadScene(1);
    }

    private void Update()
    {

        //If player is dead and there IS a hud
        //if(Players[0] == null && hud != null)
        //{
            //hud.gameOverScreen.SetActive(true);
        //}
    }

    public void SpawnEnemies(int numberToSpawn)
    {
        for (int enemy = 0; enemy < numberToSpawn; enemy++)
        {
            EnemySpawnPoints randomSpawnPoint = enemySpawnPoints[UnityEngine.Random.Range(0, enemySpawnPoints.Count)];
            //randomSpawnPoint.SpawnRandomEnemy();
            randomSpawnPoint.SpawnEnemy(enemy);
        }
    }

    public void GameOver()
    {
        // TODO: Add the player scores to the high score table

        // Move to the game over scene
        SceneManager.LoadScene("GameOver");
    }

    public void SavePreferences()
    {
        // Save Music Volume
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
        PlayerPrefs.SetFloat("sfxVolume", sfxVolume);
        // TODO: Test this out
        PlayerPrefs.SetInt("mapType", (int) mapType);
        PlayerPrefs.Save();
    }

    public void LoadPreferences()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            musicVolume = PlayerPrefs.GetFloat("musicVolume");
        }
        else
        {
            musicVolume = 1.0f;
        }

        if (PlayerPrefs.HasKey("sfxVolume"))
        {
            sfxVolume = PlayerPrefs.GetFloat("sfxVolume");
        }
        else
        {
            sfxVolume = 1.0f;
        }
            
        if (PlayerPrefs.HasKey("mapType"))
        {
            mapType = (MapGenerationType)PlayerPrefs.GetInt("mapType");
        }
        else
        {
            mapType = MapGenerationType.Random;
        }
    }

    public void UpdateHighScoreTable(ScoreData newScore)
    {
        // Add the new score to the table
        HighScoreTable.Add(newScore);

        // Sort the table
        HighScoreTable.Sort();

        // Flip the table upside-down.
        HighScoreTable.Reverse();

        // Trim the scores that are too low
        HighScoreTable = HighScoreTable.GetRange(0, 3);
    }
}
