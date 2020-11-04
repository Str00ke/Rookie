using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T4_PlayerController : MonoBehaviour
{
    public GameObject[] characters;
    public int characterIndex = 0;
    public GameObject bullet;
    public GameObject missile;
    public GameObject flame;
    //bool isFlameThrowering = false;
    //bool isInstansiated = false;
    public Quaternion bulletRotation;
    Vector3 bulletPositionOffset;
    T4_PlayerMovement playerMovement;
    //T4_FlameThrower flameScript;
    //GameObject flameObj;

    public GameObject dirMouse;

    float reloadTime = 2.0f;

    public float damageDeal;
    public float maxLife;
    public float currentLife;

    private void Awake()
    {
        playerMovement = FindObjectOfType<T4_PlayerMovement>();
        flame.SetActive(false);
        
    }


    void Start()
    {
        currentLife = maxLife;

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

        if (Input.GetButton("Fire1") && characterIndex == 1)
        {
            bool attack = GetComponent<T4_PlayerMovement>().isFirstAttack;

            if (!attack)
            {
                flame.SetActive(true);
            }
            

        } else
        {
            flame.SetActive(false);
        }
        /*else if (isFlameThrowering && !Input.GetButton("Fire1"))
        {
            isFlameThrowering = false;
            

        }*/

        /*if (isFlameThrowering && !isInstansiated)
        {
            flameObj = Instantiate(flame, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
            flameScript = FindObjectOfType<T4_FlameThrower>();
            isInstansiated = true;
        } else if (isInstansiated && !isFlameThrowering)
        {

            Destroy(flameObj);
            //flameScript.Destroy();
            
            isInstansiated = false;
        }

        if (isFlameThrowering && isInstansiated)
        {
            flameObj.transform.position = new Vector3(transform.position.x + 1, transform.position.y + transform.rotation.y, transform.position.z + 1);
            //flameObj.transform.rotation = transform.rotation;
            Debug.Log(transform.rotation.y);

            flameObj.transform.LookAt(transform, Vector3.left);

        }*/

        characters[characterIndex].transform.position = transform.position;

        /*Vector3 mouse_pos = Input.mousePosition;

        Vector3 object_pos = Camera.main.WorldToScreenPoint(transform.position);
        mouse_pos.x = mouse_pos.x - object_pos.x;
        mouse_pos.y = mouse_pos.y - object_pos.y;
        float angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, -angle, 0));*/

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
        GetComponent<T4_PlayerMovement>().ChangeCharaAnim();

    }

    void Shoot(bool isFirstAttack)
    {
        if (isFirstAttack)
        {
            
            bulletPositionOffset = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z + 1);
            bulletRotation = Quaternion.Euler(dirMouse.transform.rotation.eulerAngles.x + 90, dirMouse.transform.rotation.eulerAngles.y + 90, dirMouse.transform.rotation.eulerAngles.z);
            //Instantiate(missile, transform.position, bulletRotation);
            //GetComponent<T4_PlayerMovement>().isFirstAttack = false;
        } else
        {
            bulletPositionOffset = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z + 1);
            bulletRotation = Quaternion.Euler(dirMouse.transform.rotation.eulerAngles.x, dirMouse.transform.rotation.eulerAngles.y + 90, dirMouse.transform.rotation.eulerAngles.z);
            Instantiate(bullet, transform.position, bulletRotation);
        }
        
        
    }


    public void TakeDamage(float damageValue)
    {
        currentLife -= damageDeal;

        if (currentLife <= 0)
        {
            Debug.Log("Dead");
        }

    }

    public void DealDamage(float damageValue, GameObject hit)
    {
        hit.GetComponent<T4_EnemyController>().TakeDamage(damageValue);
    }
}
