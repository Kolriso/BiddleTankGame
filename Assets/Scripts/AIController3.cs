using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TankData))]
[RequireComponent(typeof(TankMotor))]
[RequireComponent(typeof(TankShooter))]

public class AIController3 : MonoBehaviour
{
    private TankData data;
    private TankMotor motor;
    private TankShooter shooter;
    private Health health;

    public enum AttackState { Chase };
    public AttackState attackState = AttackState.Chase;

    public enum AvoidenceStage { NotAvoiding, ObstacleDetected, AvoidingObstacle };
    public AvoidenceStage avoidenceStage = AvoidenceStage.NotAvoiding;

    public float avoidenceTime = 2.0f;
    private float exitTime = 2.0f;
    public float closeEnough = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        shooter = GetComponent<TankShooter>();
        motor = GetComponent<TankMotor>();
        data = GetComponent<TankData>();
        health = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        shooter.Shoot();

        if (attackState == AttackState.Chase)
        {
            if (avoidenceStage != AvoidenceStage.NotAvoiding)
            {
                Avoid();
            }
            else
            {
                Chase(GameManager.Instance.Players[0]);
            }
        }
    }

    public void Chase(GameObject target)
    {
        if (motor.RotateTowards(target.transform.position, data.turnSpeed))
        {
            // Do Nothing
        }
        else if (!CanMove(data.moveSpeed))
        {
            avoidenceStage = AvoidenceStage.ObstacleDetected;
        }
        else
        {
            if (Vector3.SqrMagnitude(transform.position - target.transform.position) >= (closeEnough * closeEnough))
            {
                motor.Move(data.moveSpeed);
            }

        }
    }

    public void Avoid()
    {
        if (avoidenceStage == AvoidenceStage.ObstacleDetected)
        {
            avoidenceStage = AvoidenceStage.AvoidingObstacle
            exitTime = avoidenceTime;
        }
        else if (avoidenceStage == AvoidenceStage.AvoidingObstacle)
        {
            exitTime -= Time.deltaTime;
            motor.Move(data.moveSpeed);
        }
        else
        {
            avoidenceStage = AvoidenceStage.ObstacleDetected;
        }
    }

    bool CanMove(float speed)
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, speed))
        {
            if (!hit.collider.CompareTag("Player"))
            {
                return false;
            }
        }
        // TODO: Check if we can move forward by "speed" units
        return true;
    }


    
}
