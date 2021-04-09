using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPlayer : MonoBehaviour, IRespawnable
{
    public void OnRespawn()
    {
        // Respawn the player at a new spawn point
        transform.position = GameManager.Instance.playerSpawnPoints[0].transform.position;
    }
}
