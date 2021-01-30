using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int attackDamage;
    public GameObject attacker;

    public Attack(GameObject Attacker, int Damage)
    {
        attackDamage = Damage;
        attacker = Attacker;
    }
}
