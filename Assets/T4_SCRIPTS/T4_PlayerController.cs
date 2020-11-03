using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T4_PlayerController : MonoBehaviour
{
    public GameObject[] characters;
    public int characterIndex = 0;
    public GameObject bullet;
    public Quaternion bulletRotation;
    Vector3 bulletPositionOffset;
    T4_PlayerMovement playerMovement;

    private void Awake()
    {
        playerMovement = FindObjectOfType<T4_PlayerMovement>();
    }


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
            ChangeCharacter(1);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            ChangeCharacter(-1);
        }

        if (Input.GetButtonDown("Fire1") && characterIndex == 2)
        {
            Shoot();

        }

        characters[characterIndex].transform.position = transform.position;

        

    }


    private void FixedUpdate()
    {

        
            

        
        

    }

    void ChangeCharacter(int value)
    {
        characters[characterIndex].SetActive(false);

        

        if (value == -1)
        {
            if (characterIndex - 1 < 0)
            {
                characterIndex = characters.Length - 1;
                characters[characterIndex].SetActive(true);
            }
            else
            {
                characterIndex += value;
                characters[characterIndex].SetActive(true);
            }
        } else
        {
            if (characterIndex + 1 == characters.Length)
            {
                characterIndex = 0;
                characters[characterIndex].SetActive(true);
            }
            else
            {
                characterIndex += value;
                characters[characterIndex].SetActive(true);
            }
        }

        
        GetComponent<T4_PlayerMovement>().isFirstAttack = true;

    }

    void Shoot()
    {
        bulletPositionOffset = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z + 1);
        bulletRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 90, transform.rotation.eulerAngles.z);
        Instantiate(bullet, transform.position, bulletRotation);
        
    }
}
