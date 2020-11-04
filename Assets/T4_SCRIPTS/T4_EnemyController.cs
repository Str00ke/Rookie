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

    float distanceX;
    float distanceZ;

    public float damageDeal;
    public float MaxLife;
    public float currentLife;

    public float stunTime;
    float time = 0;
    bool isStun = false;

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

    [Header("Oni")]
    public float OniSpeed;
    public float OniMinDistance;
    public float OniTimeBeforeAttack;
    public float OniAttackCooldown;
    public float OniAttackRadius;
    public GameObject OniAttackRange;
    public float OniScale;

    [Header("Squelette")]
    public float SqueletteSpeed;
    public float SqueletteMinDistance;
    public float SqueletteTimeBeforeAttack;
    public float SqueletteAttackCooldown;
    public float SqueletteAttackRadius;
    public GameObject SqueletteAttackRange;
    public float SqueletteScale;

    





    private void Awake()
    {
        player = FindObjectOfType<T4_PlayerController>().gameObject;
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    
    void Start()
    {
        currentLife = MaxLife;

        string[] splitName = gameObject.name.Split(char.Parse("_"));
        enemyName = splitName[1];
    }

    
    void Update()
    {
        
        
        /*if (player.transform.position.z > transform.position.z)
        {
            anim.SetFloat("Vertical", 1);
        } else
        {
            anim.SetFloat("Vertical", -1);
        }*/

        if (player.transform.position.x > transform.position.x)
        {
            distanceX = player.transform.position.x - transform.position.x;

        } else
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
                sprite.flipX = true;
                anim.SetFloat("Vertical", 0);
                anim.SetFloat("Horizontal", 1);
            }
            else
            {
                sprite.flipX = false;
                anim.SetFloat("Vertical", 0);
                anim.SetFloat("Horizontal", -1f);
            }
        } else
        {
            if (player.transform.position.z > transform.position.z)
            {
                sprite.flipX = true;
                anim.SetFloat("Horizontal", 0);
                anim.SetFloat("Vertical", 1);
            }
            else
            {
                sprite.flipX = false;
                anim.SetFloat("Horizontal", 0);
                anim.SetFloat("Vertical", -1);
            }
        }

        

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
            
            if (enemyName == "Alien") //Maybe Bacwards if player too near?
            {
                if (!isStun)
                {
                    if (distance > AlienMinDistance)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, AlienSpeed * Time.deltaTime);
                        anim.SetBool("isIdle", false);
                    }
                    else
                    {
                        StartCoroutine(AlienShoot());
                    }
                }

                


            }

            if (enemyName == "Oni")
            {
                
                if (distance > OniMinDistance)
                {
                        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, OniSpeed * Time.deltaTime);
                        transform.LookAt(player.transform);
                }
                else
                {
                    StartCoroutine(OniCharge());
                }
                
            }




            if (enemyName == "Squelette")
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
        


        
    }



    IEnumerator AlienShoot()
    {
        anim.SetBool("isIdle", true);
        anim.SetBool("isCharging", true);
        
        //Maybe Bacwards if player too near?
        isAttacking = true;
        //transform.position = transform.position;

        yield return new WaitForSeconds(AlienTimeBeforeAttack);
        distance = Vector3.Distance(player.transform.position, transform.position);

        while (distance <= AlienMinDistance/* && distance >= AlienMaxDist*/) //Tirs par seconde? Rafale? Coup par coup? Check si player in distance tout les combiens?
        {
            anim.SetBool("isFlashing", true);
            for (int i = 0; i < AlienShotsNbrBtwChecks; i++)
            {
                GameObject go = Instantiate(bullet, transform.position, transform.rotation);
                go.SendMessage("getName", "Ennemy");
                yield return new WaitForSeconds(AlienBtwShots);
            }
            anim.SetBool("isCharging", true);
            yield return new WaitForSeconds(AlienBtwReload);
            distance = Vector3.Distance(player.transform.position, transform.position);
        }

        anim.SetBool("isCharging", false);
        anim.SetBool("isFlashing", false);
        anim.SetBool("isIdle", true);

        yield return new WaitForSeconds(1.0f);
        isAttacking = false;
    }

    IEnumerator OniCharge()
    {
        isAttacking = true;
        //transform.position = transform.position;
        yield return new WaitForSeconds(OniTimeBeforeAttack);
        distance = Vector3.Distance(player.transform.position, transform.position);
        while (distance <= OniMinDistance)
        {
            
            //attack
            

            Collider[] playerHit = Physics.OverlapBox(transform.position + (transform.forward * (OniAttackRadius / 2)), new Vector3(OniAttackRadius, OniAttackRadius, OniAttackRadius));
            Debug.Log("attack");
            foreach (Collider hit in playerHit)
            {
                if (hit.name == "Player")
                {
                    Debug.Log("Hit!");
                    DealDamage(damageDeal);
                }
            }

            //transform.LookAt(player.transform);

            yield return new WaitForSeconds(OniAttackCooldown);
            distance = Vector3.Distance(player.transform.position, transform.position);
        }

        yield return new WaitForSeconds(1.0f);
        isAttacking = false;
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
            Collider[] playerHit = Physics.OverlapSphere(transform.position, SqueletteAttackRadius);
            foreach (Collider hit in playerHit)
            {
                if (hit.name == "Player")
                {
                    //Debug.Log(hit.gameObject.name + " Touché!");
                    DealDamage(damageDeal);
                }
            }
            yield return new WaitForSeconds(SqueletteAttackCooldown);
            distance = Vector3.Distance(player.transform.position, transform.position);
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

            /*Matrix4x4 rotationMatrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
            Gizmos.matrix = rotationMatrix;*/

            Gizmos.DrawCube(transform.position + (transform.forward * (OniAttackRadius / 2)), new Vector3(OniAttackRadius, OniAttackRadius, OniAttackRadius));
            
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
            Destroy(gameObject);
        }

        if (isStun)
        {
            return;
        } else
        {
            isStun = true;
        }

    }

    public void DealDamage(float damageValue)
    {
        FindObjectOfType<T4_PlayerController>().TakeDamage(damageValue);
    }

}
