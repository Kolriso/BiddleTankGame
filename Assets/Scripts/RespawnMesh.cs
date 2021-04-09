using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnMesh : MonoBehaviour, IRespawnable
{
    public GameObject VisualsToShow;
    public void OnRespawn()
    {
        VisualsToShow.SetActive(true); 
    }
}
