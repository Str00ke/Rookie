﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T4_PlayerController : MonoBehaviour
{
    public GameObject[] characters;
    int characterIndex = 0;
    public GameObject bullet;
    public Quaternion bulletRotation;
    void Start()
    {
        characters[characterIndex].SetActive(true);
        bulletRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 90, transform.rotation.eulerAngles.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ChangeCharacter();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            Shoot();
        }

        characters[characterIndex].transform.position = transform.position;
    }


    void ChangeCharacter()
    {
        characters[characterIndex].SetActive(false);

        if (characterIndex + 1 == characters.Length)
        {
            characterIndex = 0;
            characters[characterIndex].SetActive(true);
        } else
        {
            characterIndex++;
            characters[characterIndex].SetActive(true);
        }
    }

    void Shoot()
    {
        
        Instantiate(bullet, transform.position, bulletRotation);
        
    }
}
