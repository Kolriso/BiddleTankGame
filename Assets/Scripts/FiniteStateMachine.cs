using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TankData))]
[RequireComponent(typeof(TankMotor))]
[RequireComponent(typeof(TankShooter))]
[RequireComponent(typeof(AISenses))]

public class FiniteStateMachine : MonoBehaviour
{
    private TankData data;
    private TankMotor motor;
    private TankShooter shooter;
    private AISenses CanSee;

    public enum AvoidenceStage { NotAvoiding, ObstacleDetected, AvoidingObstacle };
    public AvoidenceStage avoidenceStage = AvoidenceStage.NotAvoiding;

    public enum EnemyPersonality { Guard, Cowardly, Aggressive, Wanderer };
    public EnemyPersonality personality = EnemyPersonality.Guard;

    public enum AIState { Chase, ChaseAndFire, CheckForFlee, Flee, Rest };
    public AIState aiState = AIState.Chase;

    public float currentHealth;
    public float maxHealth;

    public float stateExitTime;

    // TODO: We need a way to keep track of all the waypoints.
    public GameObject[] waypoints;
    // We need a way to keep track of the current waypoint.
    private int currentWaypoint = 1;
    public float closeEnough = 4.0f;
    public enum LoopType { Stop, Loop, PingPong };
    public LoopType loopType = LoopType.Stop;
    private bool isLoopingForward = true;

    public float avoidenceTime = 2.0f;
    private float exitTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (personality)
        {
            case EnemyPersonality.Guard:
                GuardFSM();
                break;
            case EnemyPersonality.Cowardly:
                CowardlyFSM();
                break;
            case EnemyPersonality.Aggressive:
                AggressiveFSM();
                break;
            case EnemyPersonality.Wanderer:
                WandererFSM();
                break;
            default:
                Debug.LogWarning("[FiniteStateMachine] Unimplemented finite state machine");
                break;
        }

        // We need to see if we are already at the waypoint.
        // If we are not at the waypoint, turn to face it.
        if (motor.RotateTowards(waypoints[currentWaypoint].transform.position, data.turnSpeed))
        {
            // Do nothing!
        }

        // If we are facing the waypoint, move towards it.
        else
        {
            // Move forward.
            motor.Move(data.moveSpeed);
        }
    }

    void GuardFSM()
    {
        switch (aiState)
        {
            case AIState.Chase:
                // Do behaviors
                Chase(GameManager.Instance.Players[0]);
                // Check for transitions
                if (currentHealth < maxHealth * 0.5)
                {
                    ChangeState(AIState.CheckForFlee);
                }
                else if (PlayerIsInRange())
                {
                    ChangeState(AIState.ChaseAndFire);
                }
                break;
            case AIState.ChaseAndFire:
                // Do behaviors
                ChaseAndFire(GameManager.Instance.Players[0]);
                // Check for transitions
                if (currentHealth < maxHealth * 0.5)
                {
                    ChangeState(AIState.CheckForFlee);
                }
                else if (!PlayerIsInRange())
                {
                    ChangeState(AIState.Chase);
                }
                break;
            case AIState.CheckForFlee:
                // Do behaviors
                CheckForFlee();
                // Check for transitions
                if (PlayerIsInRange())
                {
                    ChangeState(AIState.Flee);
                }
                else if (!PlayerIsInRange())
                {
                    ChangeState(AIState.Rest);
                }
                break;
            case AIState.Flee:
                // Do behaviors
                Flee();
                // Check for transitions
                if (avoidenceStage == AvoidenceStage.ObstacleDetected)
                {
                    ChangeState(AIState.CheckForFlee);
                }
                break;
            case AIState.Rest:
                // Do behaviors
                Rest();
                // Check for transitions
                if (PlayerIsInRange())
                {
                    ChangeState(AIState.Flee);
                }
                else if (currentHealth == maxHealth)
                {
                    ChangeState(AIState.Chase);
                }
                break;
            default:
                Debug.LogWarning("[FiniteStateMachine] State doesn't exist.");
                break;
        }
    }

    void CowardlyFSM()
    {
        switch (aiState)
        {
            case AIState.CheckForFlee:
                // Do behaviors
                CheckForFlee();
                // Check for transitions
                if (PlayerIsInRange())
                {
                    ChangeState(AIState.Flee);
                }
                else if (!PlayerIsInRange())
                {
                    ChangeState(AIState.Rest);
                }
                break;
            case AIState.Flee:
                // Do behaviors
                Flee();
                // Check for transitions
                if (avoidenceStage == AvoidenceStage.ObstacleDetected)
                {
                    ChangeState(AIState.CheckForFlee);
                }
                break;
            case AIState.Rest:
                // Do behaviors
                Rest();
                // Check for transitions
                if (PlayerIsInRange())
                {
                    ChangeState(AIState.Flee);
                }
                break;
            default:
                Debug.LogWarning("[FiniteStateMachine] State doesn't exist.");
                break;
        }
    }

    private void AggressiveFSM()
    {
        switch (aiState)
        {
            case AIState.Chase:
                // Do behaviors
                Chase(GameManager.Instance.Players[0]);
                // Check for transitions
                if (currentHealth < maxHealth * 0.5)
                {
                    ChangeState(AIState.Rest);
                }
                else if (PlayerIsInRange())
                {
                    ChangeState(AIState.ChaseAndFire);
                }
                break;
            case AIState.ChaseAndFire:
                // Do behaviors
                ChaseAndFire(GameManager.Instance.Players[0]);
                // Check for transitions
                if (currentHealth < maxHealth * 0.5)
                {
                    ChangeState(AIState.Rest);
                }
                else if (!PlayerIsInRange())
                {
                    ChangeState(AIState.Chase);
                }
                break;
            case AIState.Rest:
                // Do behaviors
                Rest();
                // Check for transitions
                if (currentHealth == maxHealth)
                {
                    ChangeState(AIState.Chase);
                }
                break;
            default:
                Debug.LogWarning("[FiniteStateMachine] State doesn't exist.");
                break;
        }
    }

    private void WandererFSM()
    {
        if (loopType == LoopType.PingPong)
        {
            if (isLoopingForward)
            {
                if (Vector3.SqrMagnitude(transform.position - waypoints[currentWaypoint].transform.position) <= (closeEnough * closeEnough))
                {
                    if (currentWaypoint < (waypoints.Length - 1))
                    {
                        currentWaypoint++;
                    }

                    else
                    {
                        isLoopingForward = false;
                    }
                }
            }
            // TODO: Write own behavior
        }
    }
    void ChangeState(AIState newState)
    {
        aiState = newState;

        stateExitTime = Time.time;
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

    private void Chase(GameObject target)
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

    private void ChaseAndFire(GameObject target)
    {
        float attackRange = 20.0f;
        if (Vector3.Distance(transform.position, target.transform.position) < attackRange)
        {
            shooter.Shoot();
        }
    }

    private bool PlayerIsInRange()
    {
        return true;
    }

    private void Rest()
    {
        // TODO: Write this method
    }

    private void Flee()
    {
        if (avoidenceStage == AvoidenceStage.ObstacleDetected)
        {
            // Rotate left
            motor.Rotate(-1 * data.turnSpeed);

            // If I can now move forward, move to stage 2!
            if (CanMove(data.moveSpeed))
            {
                avoidenceStage = AvoidenceStage.AvoidingObstacle;

                // Set the number of seconds we will stay in Stage 2
                exitTime = avoidenceTime;
            }
        }
        else if (avoidenceStage == AvoidenceStage.AvoidingObstacle)
        {
            // If we can move forward, do so
            if (CanMove(data.moveSpeed))
            {
                // Subtract from our timer and move
                exitTime -= Time.deltaTime;
                motor.Move(data.moveSpeed);

                // If we have moved long enough, return to chase mode
                if (exitTime <= 0)
                {
                    avoidenceStage = AvoidenceStage.NotAvoiding;
                }
            }
            else
            {
                avoidenceStage = AvoidenceStage.ObstacleDetected;
            }
        }
    }

    private void CheckForFlee()
    {
        // TODO: Write this method
    }

}
