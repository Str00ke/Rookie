using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

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

    public GameObject lifeContainer;

    float reloadTime = 2.0f;

    float firstAttackDamageDeal;
    float attackDamageDeal;
    public int maxLife;
    public int currentLife;

    public float invincibilityTime;
    public bool isInvicible = false;

    T4_GameManager gameManager;

    public GameObject charaImg;
    bool isCharaLocked = false;
    public float charaWaitToChange;
    float maxTime;

    [Header("CharactersDamageValue")]
    public float joieFirstDamageValue;
    public float joieBaseDamageValue;
    public float colereFirstDamageValue;
    public float colereBaseDamageValue;
    public float vomiFirstDamageValue;
    public float vomiBaseDamageValue;

    #region Audio
    public GameObject vomiShootAudio;
    public GameObject vomiShootSplashAudio;
    public GameObject vomiShootSpeAudio;
    public GameObject vomiShootSplashSpeAudio;

    public GameObject colèreFlameAudio;
    #endregion

    private void Awake()
    {
        playerMovement = FindObjectOfType<T4_PlayerMovement>();
        flame.SetActive(false);
        gameManager = FindObjectOfType<T4_GameManager>();
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



        for (int i = 1; i < 3; i++)
        {
            charaImg.transform.GetChild(i).GetComponent<Image>().color = new Color(charaImg.transform.GetChild(i).GetComponent<Image>().color.r, charaImg.transform.GetChild(i).GetComponent<Image>().color.g, charaImg.transform.GetChild(i).GetComponent<Image>().color.b, 0.5f);
        }

        maxTime = charaWaitToChange;

    }

    // Update is called once per frame
    void Update()
    {

        if (!gameManager.isPaused)
        {


            if (reloadTime > 0)
            {
                reloadTime -= Time.deltaTime;
            }


            if (!isCharaLocked)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    ChangeCharacter(1);
                }

                if (Input.GetKeyDown(KeyCode.A))
                {
                    ChangeCharacter(-1);
                }
            } else
            {
                for (int i = 0; i < 3; i++)
                {
                    charaImg.transform.GetChild(i).GetComponent<Image>().color = new Color(0.25f, 0.25f, 0.25f, charaImg.transform.GetChild(i).GetComponent<Image>().color.a);
                }

                charaWaitToChange -= Time.deltaTime;
                if (charaWaitToChange <= 0)
                {
                    isCharaLocked = false;
                    charaWaitToChange = maxTime;
                    for (int i = 0; i < 3; i++)
                    {
                        charaImg.transform.GetChild(i).GetComponent<Image>().color = new Color(1, 1, 1, charaImg.transform.GetChild(i).GetComponent<Image>().color.a);
                    }
                }
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

                    colèreFlameAudio.SetActive(true);//AUDIO//


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

                            }
                            else
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


            }
            else
            {
                flame.SetActive(false);
                colèreFlameAudio.SetActive(false);//AUDIO//
            }


            characters[characterIndex].transform.position = transform.position;

            if (isInvicible)
            {
                for (int i = 0; i < lifeContainer.transform.childCount; i++)
                {
                    lifeContainer.transform.GetChild(i).GetComponent<Image>().color = new Color(lifeContainer.transform.GetChild(i).GetComponent<Image>().color.r, lifeContainer.transform.GetChild(i).GetComponent<Image>().color.g, lifeContainer.transform.GetChild(i).GetComponent<Image>().color.b, 0.25f);
                }
            } else
            {
                for (int i = 0; i < lifeContainer.transform.childCount; i++)
                {
                    lifeContainer.transform.GetChild(i).GetComponent<Image>().color = new Color(lifeContainer.transform.GetChild(i).GetComponent<Image>().color.r, lifeContainer.transform.GetChild(i).GetComponent<Image>().color.g, lifeContainer.transform.GetChild(i).GetComponent<Image>().color.b, 1f);
                }
            }


        }




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

        isCharaLocked = true;

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


        for (int i = 0; i < 3; i++)
        {
            if (i != characterIndex)
            {
                charaImg.transform.GetChild(i).GetComponent<Image>().color = new Color(charaImg.transform.GetChild(i).GetComponent<Image>().color.r, charaImg.transform.GetChild(i).GetComponent<Image>().color.g, charaImg.transform.GetChild(i).GetComponent<Image>().color.b, 0.25f);
            } else
            {
                charaImg.transform.GetChild(i).GetComponent<Image>().color = new Color(charaImg.transform.GetChild(i).GetComponent<Image>().color.r, charaImg.transform.GetChild(i).GetComponent<Image>().color.g, charaImg.transform.GetChild(i).GetComponent<Image>().color.b, 1);
            }
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
            vomiShootSpeAudio.SetActive(true);//AUDIO//
            vomiShootSplashSpeAudio.SetActive(true);//AUDIO//
        } else
        {
            bulletPositionOffset = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z + 1);
            bulletRotation = Quaternion.Euler(dirMouse.transform.rotation.eulerAngles.x, dirMouse.transform.rotation.eulerAngles.y + 90, dirMouse.transform.rotation.eulerAngles.z);
            GameObject go = Instantiate(bullet, transform.position, bulletRotation);
            go.SendMessage("getName", "Player");
            vomiShootAudio.SetActive(true);//AUDIO//
            vomiShootSplashSpeAudio.SetActive(true);//AUDIO//
        }
        
        
    }


    public void TakeDamage(float damageValue)
    {
        //currentLife -= damageValue;
        
        if (isInvicible)
        {
            return;
        } else
        {
            StartCoroutine(Invincibility());
        }

        currentLife--;
        lifeContainer.transform.GetChild(currentLife).gameObject.SetActive(false);

        Debug.Log("Player life: " + currentLife);

        if (currentLife <= 0)
        {
            Invoke("Dead", 1.0f);
            
        }

    }


    void Dead()
    {
        Debug.Log("Dead");
        gameManager.Lose();
    }

    IEnumerator Invincibility()
    {
        isInvicible = true;
        yield return new WaitForSeconds(invincibilityTime);
        isInvicible = false;
    }


    public void DealDamage(bool isFirstAttack, GameObject hit)
    {
        Debug.Log(hit);

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
