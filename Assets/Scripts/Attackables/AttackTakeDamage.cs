using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class AttackTakeDamage : MonoBehaviour, IAttackable
{
    private Health health;

    void Start()
    {
        health = GetComponent<Health>();
    }
    public void OnAttacked(Attack attack)
    {
        health.TakeDamage(attack);
    }
}
