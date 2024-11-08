using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_AttackSounds : MonoBehaviour
{
    public AudioClip attackSound;
    private AudioSource audioSource;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayAttackSound()
    {
        audioSource.PlayOneShot(attackSound);
    }
}
