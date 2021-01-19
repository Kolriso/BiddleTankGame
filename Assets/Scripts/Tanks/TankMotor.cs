using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TankData))]
[RequireComponent(typeof(CharacterController))]

public class TankMotor : MonoBehaviour
{
    private TankData data;
    private CharacterController charController;

    // Start is called before the first frame update and how to create our variables
    public void Start()
    {
        data = GetComponent<TankData>();
        charController = GetComponent<CharacterController>();
    }

    /// <summary>
    /// The Move method moves the tank
    /// </summary>
    /// <param name="moveSpeed">Movement speed in meters per second</param>

    //How to move the tank forward and backwards
    public void Move(float moveSpeed)
    {
        //Create a vector to hold our speed data
        //Start with with the vector pointing the same direction as the GameObject this script is on.
        //Now instead of our vector being 1 unit in length, apply our speed value

        Vector3 speedVector = transform.forward * moveSpeed;

        //Call SimpleMove() and send it our vector
        //Note that SimpleMove() will apply Time.deltaTime, and convert to meters per second for us!

        charController.SimpleMove(speedVector);
    }
    // How to rotaat the tank left and right
    public void Rotate(float rotateSpeed)
    {
        //Create a vector to hold our rotation data
        //Start by rotationg right by one degree per frame draw. Left is just a negative right.
        //Adjust our rotation to be based on our speed
        //Transform.Rotate() doesn't account for speed, so we need to change our ratation to "per second"
        //See the lecture on Timers for details on how this works.

        Vector3 rotateVector = Vector3.up * rotateSpeed * Time.deltaTime;

        //Now, rotate our tank by this value - we want to rotate in our local space (not in world space).

        transform.Rotate(rotateVector, Space.Self);
    }
}
