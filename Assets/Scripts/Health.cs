using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TankData))]
public class Health : MonoBehaviour
{
    private TankData data;
    private int currentHealth = 5;
    public int maxHealth = 5;

    //Call the attached Audiosource
    private AudioSource audioSource;

    public int CurrentHealth
    {
        get { return currentHealth;  }
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

    private void Start()
    {
        data = GetComponent<TankData>();
        currentHealth = maxHealth;
        //Attach audiosource to gameobjects audiosource
        audioSource = GetComponent<AudioSource>();
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
            print("is my health 0?");
            PointScore.pointScore += 5;
            GameManager.Instance.GameOver();
            // PointScore.enemyPointScore += 5;
            Die(attackData);
        }
    }

    private void Die(Attack attackData)
    {
        print("did I die?");
        print(data.gameObject.name);
        data.lives -= 1;

        audioSource.Play();

        // Check for actual game-over
        if (GameManager.Instance.isMultiplayer)
        {
            int livesRemaining =
                GameManager.Instance.Players[0].GetComponent<TankData>().lives +
                GameManager.Instance.Players[1].GetComponent<TankData>().lives;
            if (livesRemaining <= 0)
            {
                GameManager.Instance.GameOver();
            }
        }
        else
        {
            if (data.lives <= 0)
            {
                GameManager.Instance.GameOver();
            }
        }
        
        IKillable[] killables = GetComponentsInChildren<IKillable>();
        foreach (IKillable killable in killables)
        {
            killable.OnKilled(attackData);
        }

        if (data.lives > 0)
        {
            IRespawnable[] respawnables = GetComponentsInChildren<IRespawnable>();

            foreach (IRespawnable respawnable in respawnables)
            {
                respawnable.OnRespawn();
            }
        }
        //Destroy(this.gameObject);
    }
}
