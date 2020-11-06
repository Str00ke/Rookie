using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T4_AudioMananger : MonoBehaviour
{
    public T4_GameManager gameManager;
    AudioSource audioSource;
    public AudioClip levelAudio;
    public AudioClip winAudio;
    public AudioClip loseAudio;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (gameManager.hasFinished == true)
        {
            if (gameManager.hasWin == true && audioSource.clip != winAudio)
            {
                audioSource.loop = false;
                audioSource.clip = winAudio;
                audioSource.Play(0);
            }
            else if (gameManager.hasWin == false && audioSource.clip != loseAudio)
            {
                audioSource.loop = false;
                audioSource.clip = loseAudio;
                audioSource.Play(0);
            }
        }

        else if (audioSource.clip != levelAudio) 
        {
            audioSource.loop = true;
            audioSource.clip = levelAudio;
            audioSource.Play(0);
        }
    }
}
