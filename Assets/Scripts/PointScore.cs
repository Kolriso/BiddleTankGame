using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointScore : MonoBehaviour
{
    public static int pointScore = 0;
    public static int enemyPointScore = 0;

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 20), "Score " + pointScore);
        GUI.Label(new Rect(10, 30, 100, 20), "Enemy Score " + enemyPointScore);
    }
}
