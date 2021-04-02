using System.Collections;
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

    public int oldPlayerScore = 0;

    public int player1Score;
    public int player2Score;

    public List<ScoreData> HighScoreTable;

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
        SceneManager.LoadScene(1);
    }

    public void SpawnEnemies(int numberToSpawn)
    {
        for (int enemy = 0; enemy < numberToSpawn; enemy++)
        {
            EnemySpawnPoints randomSpawnPoint = enemySpawnPoints[Random.Range(0, enemySpawnPoints.Count)];
            randomSpawnPoint.SpawnRandomEnemy();
        }
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
