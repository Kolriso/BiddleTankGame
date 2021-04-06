using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TankData))]
[RequireComponent(typeof(TankMotor))]
[RequireComponent(typeof(TankShooter))]

public class InputController : MonoBehaviour, IKillable
{
    private TankData data;
    private TankMotor motor;
    private TankShooter shooter;

    public enum InputScheme { WASD, arrowKeys };
    public InputScheme inputScheme = InputScheme.WASD;

    // Start is called before the first frame update
    void Start()
    {
        data = GetComponent<TankData>();
        motor = GetComponent<TankMotor>();
        shooter = GetComponent<TankShooter>();

        if (this.gameObject == GameManager.Instance.Players[0])
        {
            inputScheme = InputScheme.WASD;
        }
        else
        {
            inputScheme = InputScheme.arrowKeys;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (inputScheme)
        {
            case InputScheme.WASD:

                // Handling Movement
                if (Input.GetKey(KeyCode.W))
                {
                    motor.Move(data.moveSpeed);
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    motor.Move(-data.moveSpeed);
                }
                else
                {
                    motor.Move(0);
                }

                // Handling Rotation
                if (Input.GetKey(KeyCode.A))
                {
                    motor.Rotate(-data.turnSpeed);
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    motor.Rotate(data.turnSpeed);
                }

                // Handle Shooting
                if (Input.GetKey(KeyCode.Space))
                {
                    shooter.Shoot();
                }

                break;

            case InputScheme.arrowKeys:

                // Handling Movement
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    motor.Move(data.moveSpeed);
                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    motor.Move(-data.moveSpeed);
                }
                else
                {
                    motor.Move(0);
                }

                // Handling Rotation
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    motor.Rotate(-data.turnSpeed);
                }
                else if (Input.GetKey(KeyCode.RightArrow))
                {
                    motor.Rotate(data.turnSpeed);
                }

                // Handle Shooting
                if (Input.GetKey(KeyCode.M))
                {
                    shooter.Shoot();
                }

                break;

            default:
                Debug.LogError("[InputController] Input scheme not implemented.");
                
                break;
        }
    }

    public void OnKilled(Attack attackData)
    {
        // disable input on player death
        this.enabled = false;
    }
}
