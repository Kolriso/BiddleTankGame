using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TankData))]
public class TankShooter : MonoBehaviour
{
    // Use this point in space for instantiating cannon balls
    public GameObject firePoint;
    public Rigidbody cannonBallPrefab;
    private TankData data;
    public float timerDelay = 1.0f;
    private float nextEventTime;
    public float thrust = 1.0f;
    // Start is called before the first frame update
    //Check cooldown timer to see if we can shoot.
    void Start()
    {
        data = GetComponent<TankData>();
        nextEventTime = Time.time + timerDelay;
    }
        void update()
    {
        if (Time.time >= nextEventTime)
        {
            Debug.Log("Fire Cannon!");
            nextEventTime = Time.time + timerDelay;
        }
    }

    public void Shoot()
    {
        // throw new NotImplementedException();


        // Instantiate the cannon ball.
        if (timerDelay <= nextEventTime)
        {
           Rigidbody clone = Instantiate(cannonBallPrefab, firePoint.transform);

            // Propel the cannon ball forward with Rigidbody.AddForce()
            clone.velocity = transform.TransformDirection(Vector3.forward * thrust);

            // The cannon ball needs some data: Who fired it, and how much damage will it do?
            CannonBall cannonBall = clone.GetComponent<CannonBall>();
            cannonBall.attacker = this.gameObject;
            cannonBall.attackDamage = data.cannonBallDamage;
        }
    }
}
