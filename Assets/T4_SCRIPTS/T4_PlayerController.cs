using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T4_PlayerController : MonoBehaviour
{
    public GameObject[] characters;
    public int characterIndex = 0;
    public GameObject bullet;
    public GameObject missile;
    public Quaternion bulletRotation;
    Vector3 bulletPositionOffset;
    T4_PlayerMovement playerMovement;

    float reloadTime = 2.0f;

    private void Awake()
    {
        playerMovement = FindObjectOfType<T4_PlayerMovement>();
    }


    void Start()
    {
        characters[characterIndex].SetActive(true);
        reloadTime -= reloadTime;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (reloadTime > 0)
        {
            reloadTime -= Time.deltaTime;
        }

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
            if (reloadTime <= 0)
            {
                Shoot(playerMovement.isFirstAttack);
                reloadTime = 2.0f;
            }
            
                

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

        if (characterIndex == 2)
        {
            reloadTime -= reloadTime;
        }
        
        GetComponent<T4_PlayerMovement>().isFirstAttack = true;
        GetComponent<T4_PlayerMovement>().isCouroutineInactive = false;

    }

    void Shoot(bool isFirstAttack)
    {
        if (isFirstAttack)
        {
            
            bulletPositionOffset = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z + 1);
            bulletRotation = Quaternion.Euler(transform.rotation.eulerAngles.x + 90, transform.rotation.eulerAngles.y + 90, transform.rotation.eulerAngles.z);
            Instantiate(missile, transform.position, bulletRotation);
            //GetComponent<T4_PlayerMovement>().isFirstAttack = false;
        } else
        {
            bulletPositionOffset = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z + 1);
            bulletRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 90, transform.rotation.eulerAngles.z);
            Instantiate(bullet, transform.position, bulletRotation);
        }
        
        
    }
}
