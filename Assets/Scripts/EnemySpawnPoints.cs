using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoints : MonoBehaviour
{
    private void Awake()
    {
        // GameManager.Instance.enemySpawnPoints.Add(this);
    }

    private void OnDestroy()
    {
        // If the game manager doesn't exist, we can't access it.
        if (GameManager.Instance == null)
        {
            return;
        }
        // Only remove ourselves from the list if we are on the list.
        if (GameManager.Instance.enemySpawnPoints.Contains(this))
        {
            GameManager.Instance.enemySpawnPoints.Remove(this);
        }
    }
    public void SpawnRandomEnemy()
    {
        // Select a random enemy to spawn
        GameObject prefabToSpawn = GameManager.Instance.EnemyAIPrefabs[Random.Range(0, GameManager.Instance.EnemyAIPrefabs.Length)];

        GameObject spawnedEnemy = Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
        // Example of how you could set personality randomly at spawn time.
        // spawnedEnemy.AiController.Personality = PersonalityType.Cowardly;
    }
}