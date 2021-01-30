using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TankData))]
[RequireComponent(typeof(TankMotor))]
[RequireComponent(typeof(TankShooter))]
public class AIController : MonoBehaviour
{
    private TankData data;
    private TankMotor motor;
    private TankShooter shooter;

    // Start is called before the first frame update
    void Start()
    {
        shooter = GetComponent<TankShooter>();
        motor = GetComponent<TankMotor>();
        data = GetComponent<TankData>();
    }

    // Update is called once per frame
    void Update()
    {
        shooter.Shoot();
    }
}
