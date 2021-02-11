using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TankData))]
[RequireComponent(typeof(TankMotor))]
[RequireComponent(typeof(TankShooter))]
public class AIController2 : MonoBehaviour
{

    private TankData data;
    private TankMotor motor;
    private TankShooter shooter;
    private Health health;

    public float fleeDistance = 1.0f;
    public float closeEnough = 4.0f;

    public enum AttackState { Chase, Flee };
    public AttackState attackState = AttackState.Chase; 

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
            // Do state behavior
            Chase(GameManager.Instance.Players[0]);
            // Check for transitions
            if (health.currentHealth < 3)
            {
                attackState = AttackState.Flee;
            }
        }
        else if (attackState == AttackState.Flee)
        {
            // Do state behavior
            Flee(GameManager.Instance.Players[0]);
            // Check for transitions
            if (health.currentHealth >= 3)
            {
                attackState = AttackState.Chase;
            }
        }
        else
        {
            Debug.LogWarning("[AIController2] Unhandled State in Update Method");
        }
    }

    public void Chase(GameObject target)
    {
        if (motor.RotateTowards(target.transform.position, data.turnSpeed))
        {
            // Do Nothing
        }
        else
        {
            if (Vector3.SqrMagnitude(transform.position - target.transform.position) >= (closeEnough * closeEnough))
            {
                motor.Move(data.moveSpeed);
            }
            
        }
    }

    public void Flee(GameObject target)
    {
        // Get the vector to our target
        Vector3 vectorToTarget = target.transform.position - transform.position;

        // Get the vector away from our target
        Vector3 vectorAwayFromTarget = -1 * vectorToTarget;

        // Normalize our vector away from our target
        vectorAwayFromTarget.Normalize();

        // Adjust for flee distance
        vectorAwayFromTarget *= fleeDistance;

        // Set our flee position
        Vector3 fleePosition = vectorAwayFromTarget + transform.position;

        if (motor.RotateTowards(vectorAwayFromTarget.normalized, data.turnSpeed))
        {
            // Do nothing
        }
        else
        {
            motor.Move(data.moveSpeed);
        }
    }
}
