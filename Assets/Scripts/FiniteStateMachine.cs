using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine : MonoBehaviour
{
    public enum EnemyPersonality { Guard, Cowardly, Aggressive, };
    public EnemyPersonality personality = EnemyPersonality.Guard;

    public enum AIState { Chase, ChaseAndFire, CheckForFlee, Flee, Rest };
    public AIState aiState = AIState.Chase;

    private float health;
    private float maxHealth;

    public float stateExitTime;

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
            default:
                Debug.LogWarning("[FiniteStateMachine] Unimplemented finite state machine");
                break;
        }
    }

    void GuardFSM()
    {
        switch (aiState)
        {
            case AIState.Chase:
                // Do behaviors
                Chase();
                // Check for transitions
                if (health < maxHealth * 0.5)
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
                ChaseAndFire();
                // Check for transitions
                if (health < maxHealth * 0.5)
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
                // Check for transitions
                break;
            case AIState.Flee:
                // Do behaviors
                // Check for transitions
                break;
            case AIState.Rest:
                // Do behaviors
                // Check for transitions
                break;
            default:
                Debug.LogWarning("[FiniteStateMachine] State doesn't exist.");
                break;
        }
    }

    void CowardlyFSM()
    {
        // TODO: Write your own behaviors
    }

    void ChangeState(AIState newState)
    {
        aiState = newState;

        stateExitTime = Time.time;
    }

    private void ChaseAndFire()
    {
        // TODO: Write this method
    }

    private bool PlayerIsInRange()
    {
        return true;
    }

    private void Chase()
    {
        // TODO: Write this method
    }
}
