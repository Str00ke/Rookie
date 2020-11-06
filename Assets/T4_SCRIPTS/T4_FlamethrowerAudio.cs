using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T4_FlamethrowerAudio : MonoBehaviour
{
    #region Variables
    public AudioClip startAudio;
    public AudioClip loop;
    AudioSource audioS;

    #endregion
    void Awake()
    {
        audioS = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        audioS.clip = startAudio;
        audioS.Play();
    }

    void Update()
    {
        if (!audioS.isPlaying)
        {
            audioS.clip = loop;
            audioS.Play();
        }
    }
}
