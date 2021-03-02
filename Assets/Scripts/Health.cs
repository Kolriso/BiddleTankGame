using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Health
{
    private int currentHealth = 5;
    public int maxHealth = 5;

    public int CurrentHealth
    {
        get { return currentHealth;  }
        set
        {
            currentHealth = value;
            if (currentHealth <= 0)
            {
                Die();
            }
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
        }
    }

    public Health(int MaxHealth)
    {
        maxHealth = MaxHealth;
        currentHealth = maxHealth;
    }
    public void TakeDamage(Attack attackData)
    {
        currentHealth -= attackData.attackDamage;

        // check to see if we died
        if (currentHealth <= 0)
        {
            PointScore.pointScore += 5;
            // PointScore.enemyPointScore += 5;
            Die();
        }
    }

    private void Die()
    {
       // Destroy(gameObject);
    }
}
