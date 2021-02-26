using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameObject playerPrefab;

    public GameObject[] Players = new GameObject[2];

    public List<GameObject> healthPowerups = new List<GameObject>();

    protected override void Awake()
    {
        base.Awake();
    }
}
