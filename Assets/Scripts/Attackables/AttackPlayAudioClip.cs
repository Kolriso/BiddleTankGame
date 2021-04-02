using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayAudioClip : MonoBehaviour, IAttackable
{
    public AudioClip clip;

public void OnAttacked(Attack attack)
    {
        // Guardian clause
        if (clip == null)
        {
            Debug.LogWarning("[AttackedPlayAudioClip] Clip not assigned on " + gameObject.name);
            return;
        }

        AudioSource.PlayClipAtPoint(clip, transform.position);
    }
}
