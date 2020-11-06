using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics.Debug;
using UnityEngine;

public class T4_EnemyController : MonoBehaviour
{
    string enemyName;
    GameObject player;
    public GameObject bullet;
    float distance;
    bool isAttacking = false;
    SpriteRenderer sprite;
    Animator anim;
    bool isDead = false;
    public GameObject bulletDir;
    public GameObject sword;
    float angle;
    float Horizontal;
    float Vertical;
    //Vector3 currentEulerAngles;
    Collider[] playerHit;

    float distanceX;
    float distanceZ;

    public float damageDeal;
    public float MaxLife;
    public float currentLife;

    public float stunTime;
    float time = 0;
    bool isStun = false;

    public T4_GameManager gameManager;
    public int squelettePoints;
    public int alienPoints;
    public int oniPoints;

    [Header("Alien")]
    public float AlienSpeed;
    public float AlienMinDistance;
    public float AlienTimeBeforeAttack;
    public float AlienAttackCooldown;
    public float AlienShotsNbrBtwChecks;
    public float AlienBtwShots;
    public float AlienBtwReload;
    public float AlienMaxDist;
    public float AlienScale;
    #region Alien Audio
    public GameObject alienChargeAudio;
    public GameObject alienShootAudio;
    public GameObject alienDeathAudio;
    #endregion

    [Header("Oni")]
    public float OniSpeed;
    public float OniMinDistance;
    public float OniTimeBeforeAttack;
    public float OniAttackCooldown;
    public float OniAttackRadius;
    public GameObject OniAttackRange;
    public float OniScale;
    #region Oni Audio
    public GameObject oniChargeAudio;
    public GameObject oniHitAudio;
    public GameObject oniMissAudio;
    public GameObject oniDeathAudio;
    #endregion

    [Header("Squelette")]
    public float SqueletteSpeed;
    public float SqueletteMinDistance;
    public float SqueletteTimeBeforeAttack;
    public float SqueletteAttackCooldown;
    public float SqueletteAttackRadius;
    public GameObject SqueletteAttackRange;
    public float SqueletteScale;
    #region Squelette Audio
    public GameObject squeletteHitAudio;
    public GameObject squeletteDeathAudio;
    #endregion






    /*private void Awake()
    {
        player = FindObjectOfType<T4_PlayerController>().gameObject;
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }*/


    void Start()
    {


        player = FindObjectOfType<T4_PlayerController>().gameObject;
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        gameManager = GetComponent<T4_GameManager>();

        currentLife = MaxLife;

        string[] splitName = gameObject.name.Split(char.Parse("_"));
        enemyName = splitName[1];

        bulletDir.transform.rotation = Quaternion.Euler(bulletDir.transform.rotation.eulerAngles.x, bulletDir.transform.rotation.eulerAngles.y, bulletDir.transform.rotation.eulerAngles.z);
    }

    
    void Update()
    {

        Vector3 player_pos = player.transform.position;


        Vector3 mouse_pos = player.transform.position;

        Vector3 object_pos = transform.position;
        mouse_pos.x = mouse_pos.x - object_pos.x;
        mouse_pos.z = mouse_pos.z - object_pos.z;
        angle = Mathf.Atan2(mouse_pos.z, mouse_pos.x) * Mathf.Rad2Deg + 90;
        //bulletDir.transform.rotation = Quaternion.Euler(bulletDir.transform.rotation.eulerAngles.x, -angle, bulletDir.transform.rotation.eulerAngles.z);
        //bullet.transform.rotation = new Quaternion(0, 0, angle, 0);
        //Debug.Log(angle);
        //Debug.Log(bulletDir.transform.rotation);
        //bulletDir.transform.rotation = Quaternion.LookRotation(player.transform.position, Vector3.up);
        //bulletDir.transform.LookAt(player.transform.position, Vector3.zero);
        //Quaternion rotation = Quaternion.Euler(bulletDir.transform.rotation.x + 90, 0, 30);
        //bulletDir.transform.rotation = rotation;
        //Debug.Log((bulletDir.transform.rotation.eulerAngles.x - 90) + "  " + (bulletDir.transform.rotation.eulerAngles.y) + "  " + (bulletDir.transform.rotation.eulerAngles.z));
        
        if (!isAttacking && !isDead)
        {
            if (player.transform.position.x > transform.position.x)
            {
                distanceX = player.transform.position.x - transform.position.x;

            }
            else
            {
                distanceX = transform.position.x - player.transform.position.x;
            }


            if (player.transform.position.z > transform.position.z)
            {
                distanceZ = player.transform.position.z - transform.position.z;
            }
            else
            {
                distanceZ = transform.position.z - player.transform.position.z;
            }

            if (distanceX > distanceZ)
            {
                if (player.transform.position.x > transform.position.x)
                {
                    if (enemyName != "Oni")
                    {
                        sprite.flipX = true;
                    }
                    else
                    {
                        sprite.flipX = false;
                    }

                    anim.SetFloat("Vertical", 0);
                    anim.SetFloat("Horizontal", 1);
                    Vertical = 0;
                    Horizontal = 1;
                }
                else
                {
                    if (enemyName != "Oni")
                    {
                        sprite.flipX = false;
                    }
                    else
                    {
                        sprite.flipX = true;
                        
                    }

                    anim.SetFloat("Vertical", 0);
                    anim.SetFloat("Horizontal", -1f);
                    Vertical = 0;
                    Horizontal = -1;
                }
            }
            else
            {
                if (player.transform.position.z > transform.position.z)
                {
                    if (enemyName != "Oni")
                    {
                        sprite.flipX = true;
                    }
                    else
                    {
                        sprite.flipX = false;
                    }

                    anim.SetFloat("Horizontal", 0);
                    anim.SetFloat("Vertical", 1);
                    Vertical = 1;
                    Horizontal = 0;
                }
                else
                {
                    if (enemyName != "Oni")
                    {
                        sprite.flipX = false;
                    }
                    else
                    {
                        sprite.flipX = true;
                    }

                    anim.SetFloat("Horizontal", 0);
                    anim.SetFloat("Vertical", -1);
                    Vertical = -1;
                    Horizontal = 0;
                }
            }
        }
        


        if (!isDead)
        {

            

            if (isStun)
            {
                if (time < stunTime)
                {
                    time += Time.deltaTime;
                }
                else
                {
                    isStun = false;
                    time = 0;
                }

            }

            distance = Vector3.Distance(player.transform.position, transform.position);
            if (!isAttacking)
            {

                if (enemyName == "Alien" || enemyName == "Alien(Clone)") //Maybe Bacwards if player too near?
                {
                    if (!isStun)
                    {
                        if (distance > AlienMinDistance)
                        {
                            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, AlienSpeed * Time.deltaTime);
                            anim.SetBool("isIdle", false);
                            Debug.Log(transform.position);
                        }
                        else
                        {
                            StartCoroutine(AlienShoot());
                        }
                    }




                }

                if (enemyName == "Oni" || enemyName == "Oni(Clone)")
                {

                    if (distance > OniMinDistance)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, OniSpeed * Time.deltaTime);
                        //transform.LookAt(player.transform);
                        anim.SetBool("isIdle", false);
                    }
                    else
                    {
                        StartCoroutine(OniCharge());
                    }

                }




                if (enemyName == "Squelette" || enemyName == "Squelette(Clone)")
                {

                    if (!isStun)
                    {
                        if (distance > SqueletteMinDistance)
                        {
                            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, SqueletteSpeed * Time.deltaTime);
                        }
                        else
                        {
                            StartCoroutine(SqueletteHit());
                        }
                    }

                }
            }
        } else if (enemyName == "Oni")
        {
            //sprite.flipX = false;
        }

        
        


        
    }

    
    IEnumerator AlienShoot()
    {
        anim.SetBool("isIdle", true);
        anim.SetBool("isCharging", true);
        alienChargeAudio.SetActive(true);//AUDIO//

        //Maybe Bacwards if player too near?
        isAttacking = true;
        //transform.position = transform.position;

        yield return new WaitForSeconds(AlienTimeBeforeAttack);
        distance = Vector3.Distance(player.transform.position, transform.position);

        
        //bulletDir.transform.LookAt(player.transform.position);
        
        while (distance <= AlienMinDistance/* && distance >= AlienMaxDist*/) //Tirs par seconde? Rafale? Coup par coup? Check si player in distance tout les combiens?
        {
            anim.SetBool("isFlashing", true);
            for (int i = 0; i < AlienShotsNbrBtwChecks; i++)
            {
                //bullet.transform.rotation = new Quaternion(0, angle, 0, 0);
                //Debug.Log(bullet.transform.rotation);
                //bulletDir.transform.rotation = Quaternion.RotateTowards(transform.rotation, player.transform.rotation, 1);
                GameObject go = Instantiate(bullet, transform.position, /*Quaternion.Euler(bulletDir.transform.rotation.eulerAngles.x, -angle, bulletDir.transform.rotation.eulerAngles.z)*/ Quaternion.Euler(0, -angle, 0));
                alienShootAudio.SetActive(true);//AUDIO//
                go.SendMessage("getName", "Ennemy");
                yield return new WaitForSeconds(AlienBtwShots);
            }
            anim.SetBool("isCharging", true);
            yield return new WaitForSeconds(AlienBtwReload);
            distance = Vector3.Distance(player.transform.position, transform.position);
            if (isDead)
            {
                break;
            }
        }

        anim.SetBool("isCharging", false);
        anim.SetBool("isFlashing", false);
        anim.SetBool("isIdle", true);

        yield return new WaitForSeconds(1.0f);
        isAttacking = false;
    }

    IEnumerator OniCharge()
    {
        anim.SetBool("isIdle", true);
        isAttacking = true;
        //transform.position = transform.position;
        /*anim.SetTrigger("Charge");
        yield return new WaitForSeconds(OniTimeBeforeAttack);*/
        distance = Vector3.Distance(player.transform.position, transform.position);
        while (distance <= OniMinDistance)
        {
            
            
            anim.SetTrigger("Charge");
            oniChargeAudio.SetActive(true);//AUDIO//




            if (Vertical == 0)
            {
                if (Horizontal == -1)
                {
                    
                    sword.transform.position = transform.position + (new Vector3(1, 0, 0) * -(OniAttackRadius));
                }
                else
                {
                    sword.transform.position = transform.position + (new Vector3(1, 0, 0) * (OniAttackRadius));
                    
                }
            }
            else
            {
                if (Vertical == -1)
                {
                    sword.transform.position = transform.position + (new Vector3(0, 0, 1) * -(OniAttackRadius));
                    
                }
                else
                {
                    sword.transform.position = transform.position + (new Vector3(0, 0, 1) * (OniAttackRadius));
                    
                }
            }


            sword.SetActive(true);
            sword.GetComponent<Animator>().SetTrigger("Attack");
            yield return new WaitForSeconds(OniTimeBeforeAttack);
            //attack




            anim.SetTrigger("Attack");

            yield return new WaitForSeconds(0.3f);

            if (Vertical == 0)
            {
                if (Horizontal == -1)
                {
                    playerHit = Physics.OverlapBox(transform.position + (new Vector3(1, 0, 0) * -(OniAttackRadius)), new Vector3(OniAttackRadius, OniAttackRadius, OniAttackRadius));
                }
                else
                {
                    
                    playerHit = Physics.OverlapBox(transform.position + (new Vector3(1, 0, 0) * (OniAttackRadius)), new Vector3(OniAttackRadius, OniAttackRadius, OniAttackRadius));
                }
            }
            else
            {
                if (Vertical == -1)
                {
                   
                    playerHit = Physics.OverlapBox(transform.position + (new Vector3(0, 0, 1) * -(OniAttackRadius)), new Vector3(OniAttackRadius, OniAttackRadius, OniAttackRadius));
                }
                else
                {
                    
                    playerHit = Physics.OverlapBox(transform.position + (new Vector3(0, 0, 1) * (OniAttackRadius)), new Vector3(OniAttackRadius, OniAttackRadius, OniAttackRadius));
                }
            }

            
            Debug.Log(playerHit);

            bool playerIsHit = false;//PLAYER HIT SET//

            foreach (Collider hit in playerHit)
            {
                if (hit.name == "Player")
                {
                    DealDamage(damageDeal);
                    playerIsHit = true;//PLAYER IS HIT//
                }
            }

            if (playerIsHit == false)//PLAYER HIT CHECK
            {
                oniMissAudio.SetActive(true);//AUDIO//
            }
            else
            {
                oniHitAudio.SetActive(true);//AUDIO//
            }


            
            yield return new WaitForSeconds(OniAttackCooldown);
            isAttacking = false;
            distance = Vector3.Distance(player.transform.position, transform.position);

            /*if (distance > OniMinDistance)
            {
                break;
            }
            {
                anim.SetTrigger("Charge");
                
            }*/

            
        }

        yield return new WaitForSeconds(1.0f);
        isAttacking = false;
        anim.SetBool("isIdle", false);
    }


    IEnumerator SqueletteHit()
    {
        isAttacking = true;
        //transform.position = transform.position;
        yield return new WaitForSeconds(SqueletteTimeBeforeAttack);
        distance = Vector3.Distance(player.transform.position, transform.position);
        while (distance <= SqueletteMinDistance) //try with while
        {
            if (isStun)
            {
                break;
            }

            //attack
            anim.SetTrigger("Attack");
            Collider[] playerHit = Physics.OverlapSphere(transform.position, SqueletteAttackRadius);
            bool playerIsHit = false;//PLAYER HIT SET//
            foreach (Collider hit in playerHit)
            {
                if (hit.name == "Player")
                {
                    //Debug.Log(hit.gameObject.name + " Touché!");
                    DealDamage(damageDeal);
                    playerIsHit = true;//PLAYER IS HIT//
                }
            }
            yield return new WaitForSeconds(SqueletteAttackCooldown);
            distance = Vector3.Distance(player.transform.position, transform.position);

            if (playerIsHit == true)//PLAYER HIT CHECK
            {
                squeletteHitAudio.SetActive(true);//AUDIO//
            }

        }

        yield return new WaitForSeconds(0.3f);
        isAttacking = false;
    }


    private void OnDrawGizmos()
    {

        if (enemyName == "Oni")
        {
            Gizmos.color = new Color(0, 255, 0, 0.25f);
            //Gizmos.DrawSphere(/*OniAttackRange.*/transform.position, OniAttackRadius);


            if (Vertical == 0)
            {
                if (Horizontal == -1)
                {
                    Gizmos.DrawCube(transform.position + (new Vector3(1, 0, 0) * -(OniAttackRadius)), new Vector3(OniAttackRadius, OniAttackRadius, OniAttackRadius));
                } else
                {
                    Gizmos.DrawCube(transform.position + (new Vector3(1, 0, 0) * (OniAttackRadius)), new Vector3(OniAttackRadius, OniAttackRadius, OniAttackRadius));
                }
            } else
            {
                if (Vertical == -1)
                {
                    Gizmos.DrawCube(transform.position + (new Vector3(0, 0, 1) * -(OniAttackRadius)), new Vector3(OniAttackRadius, OniAttackRadius, OniAttackRadius));
                } else
                {
                    Gizmos.DrawCube(transform.position + (new Vector3(0, 0, 1) * (OniAttackRadius)), new Vector3(OniAttackRadius, OniAttackRadius, OniAttackRadius));
                }
            }

     
            
        } else if (enemyName == "Squelette")
        {
            Gizmos.color = new Color(255, 0, 0, 0.25f);
            Gizmos.DrawSphere(/*SqueletteAttackRange.*/transform.position, SqueletteAttackRadius);
        }
        if (enemyName == "Alien")
        {
            Gizmos.color = new Color(100, 100, 0, 0.25f);
            Gizmos.DrawSphere(/*SqueletteAttackRange.*/transform.position, AlienMinDistance);
            Gizmos.color = new Color(0, 0, 255, 0.25f);
            Gizmos.DrawSphere(/*SqueletteAttackRange.*/transform.position, AlienMaxDist);
        }

    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Wall")
        {
            Debug.Log("Touch");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Wall")
        {
            Debug.Log("Touch");
        }
    }*/


    public void TakeDamage(float damageValue)
    {
        currentLife -= damageDeal;
        
        Debug.Log("Enemy life: " + currentLife);

        if (currentLife <= 0)
        {
            StartCoroutine(Death());
        }

        if (isStun)
        {
            return;
        } else
        {
            StartCoroutine(HitBlink());
            isStun = true;
        }

    }

    IEnumerator HitBlink()
    {

        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0.5f);
        yield return new WaitForSeconds(0.10f);
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1f);
        yield return new WaitForSeconds(0.10f);
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0.5f);
        yield return new WaitForSeconds(0.10f);
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1f);




    }

    IEnumerator Death()
    {
        isDead = true;
        transform.GetComponent<Rigidbody>().freezeRotation = true;
        transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        gameObject.GetComponent<SphereCollider>().enabled = false;

        anim.SetTrigger("Death");
        if (enemyName == "Oni")//NAME CHECK//
        {
            oniDeathAudio.SetActive(true);//AUDIO//
            FindObjectOfType<T4_GameManager>().AddScore(oniPoints);
        }
        else if (enemyName == "Alien")
        {
            alienDeathAudio.SetActive(true);//AUDIO//
            FindObjectOfType<T4_GameManager>().AddScore(alienPoints);
        }
        else if (enemyName == "Squelette")
        {
            squeletteDeathAudio.SetActive(true);//AUDIO//
            FindObjectOfType<T4_GameManager>().AddScore(squelettePoints);
        }

        yield return new WaitForSeconds(5.0f);
        Destroy(gameObject);
    }

    public void DealDamage(float damageValue)
    {
        FindObjectOfType<T4_PlayerController>().TakeDamage(damageValue);
    }

}
