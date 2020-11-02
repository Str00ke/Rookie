using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T4_PlayerController : MonoBehaviour
{
    public GameObject[] characters;
    int characterIndex = 0;
    void Start()
    {
        characters[characterIndex].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ChangeCharacter();
        }
        
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
}
