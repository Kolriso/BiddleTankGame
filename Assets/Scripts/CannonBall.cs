using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public GameObject attacker;
    public int attackDamage;

    //Call the attached Audiosource
    private AudioSource audioSource;

    private void Start()
    {
        //Attach audiosource to gameobjects audiosource
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Bullet")
        {
            Attack attackData = new Attack(attacker, attackDamage);

            IAttackable[] attackables = collision.gameObject.GetComponents<IAttackable>();

            foreach (IAttackable attackable in attackables)
            {
                attackable.OnAttacked(attackData);
            }

            //collision.gameObject.SendMessage("TakeDamage", attackData, SendMessageOptions.RequireReceiver);
            
            // Destroy our cannon ball when it runs into another object.
            Destroy(this.gameObject);
        }
    }

}