using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float currentHealth = 5.0f;
    public float maxHealth = 5.0f;

    public void TakeDamage(Attack attackData)
    {
        currentHealth -= attackData.attackDamage;

        // check to see if we died
        if (currentHealth <= 0)
        {
            PointScore.pointScore += 5;
            PointScore.enemyPointScore += 5;
            Die();
        }
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }
}
