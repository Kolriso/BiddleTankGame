using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TankData))]
[RequireComponent(typeof(TankMotor))]
[RequireComponent(typeof(TankShooter))]
public class AIController : MonoBehaviour
{
    // TODO: We need a way to keep track of all the waypoints.
    public GameObject[] waypoints;
    // We need a way to keep track of the current waypoint.
    private int currentWaypoint = 1;
    public float closeEnough = 1.0f;
    public enum LoopType { Stop, Loop, PingPong };
    public LoopType loopType = LoopType.Stop;
    private bool isLoopingForward = true;

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
        
        // If we've arrived at our waypoint, then go to the next one.
        if (loopType == LoopType.Stop)
        {
            if (Vector3.SqrMagnitude(transform.position - waypoints[currentWaypoint].transform.position) <= (closeEnough * closeEnough))
            {
                if (currentWaypoint < (waypoints.Length - 1))
                {
                    currentWaypoint++;
                }
            }
                
        }

        else if (loopType == LoopType.Loop)
        {
            if (Vector3.SqrMagnitude(transform.position - waypoints[currentWaypoint].transform.position) <= (closeEnough * closeEnough))
            {
                if (currentWaypoint < (waypoints.Length - 1))
                {
                    currentWaypoint++;
                }

                else
                {
                    currentWaypoint = 0;
                }
            }
        }
        else if (loopType == LoopType.PingPong)
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
            else
            {
                if (Vector3.SqrMagnitude(transform.position - waypoints[currentWaypoint].transform.position) <= (closeEnough * closeEnough))
                {
                    if (currentWaypoint > 0)
                    {
                        currentWaypoint--;
                    }

                    else
                    {
                        isLoopingForward = true;
                    }
                }
            }
        }

        else
        {
            Debug.LogWarning("[AIController] Unexpected LoopType");
        }
    }
}
