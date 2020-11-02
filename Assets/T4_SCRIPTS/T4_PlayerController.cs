using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T4_PlayerController : MonoBehaviour
{
    public GameObject[] characters;
    int characterIndex = 0;
    public GameObject bullet;
    public Quaternion bulletRotation;
    Vector3 bulletPositionOffset;
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

        if (Input.GetButtonDown("Fire1") && characterIndex == 0)
        {
            Shoot();
        }

        characters[characterIndex].transform.position = transform.position;

        

    }


    private void FixedUpdate()
    {

        
            

        
        // Does the ray intersect any objects excluding the player layer
        /*if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
        }*/

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
        bulletPositionOffset = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z + 1);
        bulletRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 90, transform.rotation.eulerAngles.z);
        Instantiate(bullet, transform.position, bulletRotation);
        
    }
}
