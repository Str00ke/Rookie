using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class T4_PlayerController : MonoBehaviour
{
    public GameObject[] characters;
    public int characterIndex = 0;
    public GameObject bullet;
    public GameObject missile;
    public GameObject flame;
    public Quaternion bulletRotation;
    Vector3 bulletPositionOffset;
    T4_PlayerMovement playerMovement;
    Dictionary<int, float> flamesDict = new Dictionary<int, float>();
    public float flameRecoverTime;

    public GameObject dirMouse;

    float reloadTime = 2.0f;

    float firstAttackDamageDeal;
    float attackDamageDeal;
    public float maxLife;
    public float currentLife;

    [Header("CharactersDamageValue")]
    public float joieFirstDamageValue;
    public float joieBaseDamageValue;
    public float colereFirstDamageValue;
    public float colereBaseDamageValue;
    public float vomiFirstDamageValue;
    public float vomiBaseDamageValue;

    private void Awake()
    {
        playerMovement = FindObjectOfType<T4_PlayerMovement>();
        flame.SetActive(false);
        
    }


    void Start()
    {

        if (gameObject.gameObject.name == "Joie")
        {
            firstAttackDamageDeal = joieFirstDamageValue;
            attackDamageDeal = joieBaseDamageValue;
        }
        else if (gameObject.gameObject.name == "Colere")
        {
            firstAttackDamageDeal = colereFirstDamageValue;
            attackDamageDeal = colereBaseDamageValue;
        }
        else
        {
            firstAttackDamageDeal = vomiFirstDamageValue;
            attackDamageDeal = vomiBaseDamageValue;
        }

        currentLife = maxLife;

        characters[characterIndex].SetActive(true);
        reloadTime -= reloadTime;
    }

    // Update is called once per frame
    void Update()
    {


        Debug.Log(flamesDict.ToList().Count);
        
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

        for (int i = 0; i < flamesDict.ToList().Count; i++)
        {
            int ID = flamesDict.ElementAt(i).Key;
            float indexValue = flamesDict.ElementAt(i).Value;
            //Debug.Log(go);
            indexValue -= Time.deltaTime;
            flamesDict[ID] = indexValue;
            //Debug.Log(indexValue);
            if (indexValue <= 0)
            {
                flamesDict.Remove(flamesDict.ElementAt(i).Key);
            }


        }


        if (Input.GetButton("Fire1") && characterIndex == 1)
        {
            bool attack = GetComponent<T4_PlayerMovement>().isFirstAttack;

            if (!attack)
            {
                flame.SetActive(true);
                /*if (!isFlame)
                {
                    StartCoroutine(FlameCollider());
                    
                }*/

                

                Collider[] flames = Physics.OverlapBox(flame.transform.position, flame.transform.localScale / 2);

                foreach (Collider burn in flames)
                {
                    string[] splitName = burn.name.Split(char.Parse("_"));
                    string burnName = splitName[0];
                    if (burnName == "Ennemy")
                    {
                        if (flamesDict.ContainsKey(burn.gameObject.GetInstanceID()))
                        {

                            Debug.Log("Already");
                            /*flamesDict.Add(burn.gameObject, flameRecoverTime);
                            Debug.Log("Added: " + burn.gameObject);*/
                            
                        } else
                        {
                            
                            flamesDict.Add(burn.gameObject.GetInstanceID(), flameRecoverTime);
                            DealDamage(false, burn.gameObject);
                            Debug.Log(burn.GetInstanceID());
                        }
                        
                    }


                    



                    /*string[] splitName = burn.name.Split(char.Parse("_"));
                    string burnName = splitName[0];
                    if (burnName == "Ennemy")
                    {
                        DealDamage(false, burn.gameObject);
                    }*/




                }

            }
            

        } else
        {
            flame.SetActive(false);
        }
        

        characters[characterIndex].transform.position = transform.position;

        
        

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(100, 100, 100, 0.5f);
        Gizmos.DrawCube(flame.transform.position, flame.transform.localScale / 2);
    }

    /*IEnumerator FlameCollider()
    {
        Debug.Log("Start");
        while (isFlame)
        {
            Debug.Log("flaamme");
        }

        yield return new WaitForSeconds(0.1f);
    }*/

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
        if (gameObject.gameObject.name == "Joie")
        {
            firstAttackDamageDeal = joieFirstDamageValue;
            attackDamageDeal = joieBaseDamageValue;
        }
        else if (gameObject.gameObject.name == "Colere")
        {
            firstAttackDamageDeal = colereFirstDamageValue;
            attackDamageDeal = colereBaseDamageValue;
        }
        else
        {
            firstAttackDamageDeal = vomiFirstDamageValue;
            attackDamageDeal = vomiBaseDamageValue;
        }

    }

    void Shoot(bool isFirstAttack)
    {
        if (isFirstAttack)
        {
            
            bulletPositionOffset = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z + 1);
            bulletRotation = Quaternion.Euler(dirMouse.transform.rotation.eulerAngles.x + 90, dirMouse.transform.rotation.eulerAngles.y + 90, dirMouse.transform.rotation.eulerAngles.z);
            GameObject go = Instantiate(missile, transform.position, bulletRotation);
            go.SendMessage("getName", "Player");
            //GetComponent<T4_PlayerMovement>().isFirstAttack = false;
        } else
        {
            bulletPositionOffset = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z + 1);
            bulletRotation = Quaternion.Euler(dirMouse.transform.rotation.eulerAngles.x, dirMouse.transform.rotation.eulerAngles.y + 90, dirMouse.transform.rotation.eulerAngles.z);
            GameObject go = Instantiate(bullet, transform.position, bulletRotation);
            go.SendMessage("getName", "Player");
        }
        
        
    }


    public void TakeDamage(float damageValue)
    {
        //currentLife -= damageValue;
        currentLife--;
        Debug.Log("Player life: " + currentLife);

        if (currentLife <= 0)
        {
            Debug.Log("Dead");
        }

    }

    public void DealDamage(bool isFirstAttack, GameObject hit)
    {
        if (isFirstAttack)
        {
            hit.GetComponent<T4_EnemyController>().TakeDamage(firstAttackDamageDeal);
        } else
        {
            hit.GetComponent<T4_EnemyController>().TakeDamage(attackDamageDeal);
        }

        
        //Debug.Log(hit.name + " touché");
    }

    
}
