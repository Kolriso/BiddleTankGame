using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TankData))]
public class TankShooter : MonoBehaviour
{
    // Use this point in space for instantiating cannon balls
    public GameObject firePoint;
    public GameObject cannonBallPrefab;
    private TankData data;
    // Start is called before the first frame update
    void Start()
    {
        data = GetComponent<TankData>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
        // throw new NotImplementedException();
        //Check cooldown timer to see if we can shoot. 

        // Instantiate the cannon ball.
        GameObject firedCannonBall = Instantiate(cannonBallPrefab);

        // Propel the cannon ball forward with Rigidbody.AddForce()

        // The cannon ball needs some data: Who fired it, and how much damage will it do?
        CannonBall cannonBall = firedCannonBall.GetComponent<CannonBall>();
        cannonBall.attacker = this.gameObject;
        cannonBall.attackDamage = data.cannonBallDamage;
    }
}
