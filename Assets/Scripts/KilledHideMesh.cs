using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KilledHideMesh : MonoBehaviour, IKillable
{
    public GameObject VisualsToHide;
    public void OnKilled(Attack attackData)
    {
        VisualsToHide.SetActive(false);
    }
}
