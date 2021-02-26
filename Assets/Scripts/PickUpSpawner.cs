using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{
    public GameObject pickupToSpawnIn;
    public GameObject spawnedPickup;
    public float secondsUntilSpawn = 30.0f;
    private float secondsRemaining;

    private void Start()
    {
        SpawnPickup();
    }

    private void Update()
    {
        if (spawnedPickup == null)
        {
            secondsRemaining -= Time.deltaTime;
            if (secondsRemaining <= 0f)
            {
                SpawnPickup();
            }
        }
    }
    private void SpawnPickup()
    {
        // Spawn in the powerup
        spawnedPickup = Instantiate(pickupToSpawnIn, transform.position, Quaternion.identity);
        // Reset the timer
        secondsRemaining = secondsUntilSpawn;
    }
}
