using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class T4_Audio : MonoBehaviour
{
    public AudioClip[] audioClips;
    AudioSource audioSource;
    private void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        AudioClip Clip = audioClips[Random.Range(0, audioClips.Length)];
        audioSource.clip = Clip;
        audioSource.Play();
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.9f);
        gameObject.SetActive(false);
    }
}
