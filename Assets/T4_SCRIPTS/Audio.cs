using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Audio : MonoBehaviour
{
    void Awake()
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();

        AudioSource audioSource = audioSources[Random.Range(0, audioSources.Length)];
        audioSource.Play();
    }
}
