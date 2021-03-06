﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TankData))]
[RequireComponent(typeof(Health))]

public class PowerUpController : MonoBehaviour
{
    private TankData data;
    private Health health;
    public List<PowerUp> powerups = new List<PowerUp>();

    private void Start()
    {
        data = GetComponent<TankData>();
        health = GetComponent<Health>();
    }
    public void Add(PowerUp powerup)
    {
        powerup.OnActivate(data, health);
        if (!powerup.isPermanent)
        { 
            powerups.Add(powerup);
        }
    }

    void Update()
    {
        // Create a list to hold expired powerups
        List<PowerUp> expiredPowerups = new List<PowerUp>();

        // Loop through all the powers in the list
        foreach (PowerUp power in powerups)
        {
            // Subtract from timer
            power.duration -= Time.deltaTime;

            // Assemble a list of expired powerups
            if (power.duration <= 0)
            {
                expiredPowerups.Add(power);
            }
        }
        // Now that we've looked in our list, use our list of expired powerups to remove the expired on deactivate
        foreach (PowerUp power in expiredPowerups)
        {
            power.OnDeactivate(data, health);
            powerups.Remove(power);
        }
        // Since our expiredPowerups is local, it will "poof" into nothing when this function ends,
        /// but let's clear it to learn how to empty a list
        expiredPowerups.Clear();
    }
}
