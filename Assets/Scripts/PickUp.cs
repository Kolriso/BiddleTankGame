using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public PowerUp powerup;
    public AudioClip soundEffect;

    private void OnTriggerEnter(Collider other)
    {
        PowerUpController powerupController = other.gameObject.GetComponent<PowerUpController>();

        if (powerupController != null)
        {
            // Add the powerup to the powerup controller
            powerupController.Add(powerup);

            // Play the sound effect if one exists.
            if (soundEffect != null)
            {
                // Use PlayClipAtPoint to play audio when you're destroying the source of that audio.
                AudioSource.PlayClipAtPoint(soundEffect, transform.position);
            }

            // Destroy this gameobject because it has been collected.
            Destroy(this.gameObject);
        }
    }
}
