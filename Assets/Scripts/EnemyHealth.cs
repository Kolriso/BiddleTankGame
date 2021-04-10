using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private int currentHealth = 5;
    public int maxHealth = 5;

    public int CurrentHealth
    {
        get { return currentHealth; }
        set
        {
            currentHealth = value;
            if (currentHealth <= 0)
            {
                //Die(attackData);
            }
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
        }
    }

    public EnemyHealth(int MaxHealth)
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
            Die(attackData);
        }
    }

    private void Die(Attack attackData)
    {
        IKillable[] killables = GetComponentsInChildren<IKillable>();
        foreach (IKillable killable in killables)
        {
            killable.OnKilled(attackData);
        }

        Destroy(this.gameObject);
    }
}
