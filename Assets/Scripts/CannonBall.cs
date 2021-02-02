﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public GameObject attacker;
    public int attackDamage;


    private void OnCollisionEnter(Collision collision)
    {
        Attack attackData = new Attack(attacker, attackDamage);
        collision.gameObject.SendMessage("TakeDamage", attackData, SendMessageOptions.RequireReceiver);
        // Destroy our cannon ball when it runs into another object.
        Destroy(this.gameObject);
    }

}