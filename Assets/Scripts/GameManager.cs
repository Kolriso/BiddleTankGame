using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameObject playerPrefab;

    public GameObject[] Players = new GameObject[2];

    public GameObject[] EnemyAIPrefabs;

    public List<GameObject> healthPowerups = new List<GameObject>();

    public List<EnemySpawnPoints> enemySpawnPoints = new List<EnemySpawnPoints>();

    protected override void Awake()
    {
        base.Awake();
    }

    public void SpawnEnemies(int numberToSpawn)
    {
        for (int enemy = 0; enemy < numberToSpawn; enemy++)
        {
            EnemySpawnPoints randomSpawnPoint = enemySpawnPoints[Random.Range(0, enemySpawnPoints.Count)];
            randomSpawnPoint.SpawnRandomEnemy();
        }
    }
}
