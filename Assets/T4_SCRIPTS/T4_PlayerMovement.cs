﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;

public class T4_PlayerMovement : MonoBehaviour
{

    Rigidbody rb;
    float movHorizontal;
    float movVertical;
    Vector3 movement;
    public bool isCouroutineInactive = false;
    bool hasHit = false;
    bool isStomping = false;
    bool isHitting = false;
    //bool isPicked = true;
    public GameObject cacPos;
    public Animator anim;
    public GameObject dirMouse;
    
    
    public bool isFirstAttack;
    T4_PlayerController charaPool;


    private void Awake()
    {
        charaPool = FindObjectOfType<T4_PlayerController>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isFirstAttack = true;
        anim = GetComponentInChildren<Animator>();
    }


    private void Update()
    {

        /*if (charaPool.characterIndex == 1 && isFirstAttack)
        {
            isPicked = true;
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(transform.position);
            Debug.Log(Input.mousePosition);
            transform.position = new Vector3(transform.position.x, transform.position.y - 10, transform.position.z);
            
            if (Input.GetButtonUp("Fire1"))
            {
                isPicked = false;
            }
        }*/

        if (Input.GetButtonUp("Fire1"))
        {
            isCouroutineInactive = true;
            isStomping = true;
            if (charaPool.characterIndex == 0 && !isFirstAttack && isHitting)
            {
                isHitting = false;
                StartCoroutine(CacHit());


            }

            
        }


        

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        
        movVertical = Input.GetAxis("Vertical");
        movHorizontal = Input.GetAxis("Horizontal");
        movement = new Vector3(movHorizontal, 0, movVertical);
        rb.MovePosition(rb.position + movement * 5 * Time.fixedDeltaTime);

        Vector3 mouse_pos = Input.mousePosition;

        Vector3 object_pos = Camera.main.WorldToScreenPoint(transform.position);
        mouse_pos.x = mouse_pos.x - object_pos.x;
        mouse_pos.y = mouse_pos.y - object_pos.y;
        float angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg - 90;
        
        if (angle < 90 && angle > -90)
        {
            if (movVertical < 0)
            {
                anim.SetFloat("Vertical", -movVertical);
            } else
            {
                anim.SetFloat("Vertical", movVertical);
            }

            
            
            /*if (movVertical > 0)
            {
                anim.SetFloat("Vertical", movVertical);
                Debug.Log("haut");
            } else
            {
                anim.SetFloat("Vertical", -1);
                Debug.Log(movVertical);
            }*/
        }
            
        else
        {

            if (movVertical < 0)
            {
                anim.SetFloat("Vertical", movVertical);
            }
            else
            {
                anim.SetFloat("Vertical", -movVertical);
            }
        }

        
            




        //anim.SetFloat("Horizontal", movHorizontal);
        //anim.SetFloat("Vertical", movVertical);

        if (rb.velocity.x != 0)
        {
            rb.velocity = new Vector3(0, 0, 0);
        }

        if (rb.angularVelocity.x != 0)
        {
            rb.angularVelocity = new Vector3(0, 0, 0);
        }

        
        if (!hasHit && isFirstAttack)
        {
            
            RaycastHit hit;

            if (charaPool.characterIndex == 0)
            {
                if (Physics.Raycast(transform.position, dirMouse.transform.TransformDirection(Vector3.right), out hit))
                {
                    Debug.DrawRay(transform.position, dirMouse.transform.TransformDirection(Vector3.right) * hit.distance, Color.yellow);
                    //Debug.Log(hit.distance);
                    //Debug.Log(hit.collider.gameObject.name);
                    if (isCouroutineInactive)
                    {
                        hasHit = true;
                        StartCoroutine(Dash((int)hit.distance, hit.point, hit.collider));
                        isCouroutineInactive = false;
                    }
                }
            } else if (charaPool.characterIndex == 2)
            {
                if (Physics.Raycast(transform.position, dirMouse.transform.TransformDirection(Vector3.left), out hit))
                {
                    Debug.DrawRay(transform.position, dirMouse.transform.TransformDirection(Vector3.left) * hit.distance, Color.yellow);
                    //Debug.Log(hit.distance);
                    if (isCouroutineInactive)
                    {
                        hasHit = true;
                        StartCoroutine(Dash((int)hit.distance, hit.point, hit.collider));
                        isCouroutineInactive = false;
                    }
                }
            }

            
            
        }


        if (charaPool.characterIndex == 1 && isFirstAttack && isCouroutineInactive && isStomping)
        {
            //while (!Input.GetButtonDown("Fire1"))
            Stomp();
            isFirstAttack = false;
            
        }

        



    }

    public void ChangeCharaAnim()
    {
        anim = GetComponentInChildren<Animator>();
        //Debug.Log(anim.gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.color = new Color(0, 255, 0, 0.25f);
        if (charaPool.characterIndex == 1 && isFirstAttack)
            Gizmos.DrawSphere(transform.position, 5.0f);
        if (charaPool.characterIndex == 0 && !isFirstAttack)
        {
            Gizmos.color = new Color(255, 0, 0, 0.25f);
            Gizmos.DrawSphere(cacPos.transform.position, 2.0f);
        }

        Gizmos.color = new Color(0, 0, 255, 0.25f);
        //Gizmos.DrawSphere(cacPos)

    }

    void Stomp()
    {

        

        Collider[] stompCollider = Physics.OverlapSphere(transform.position, 5.0f);
        foreach (Collider hit in stompCollider)
        {
            string[] splitName = hit.name.Split(char.Parse("_"));
            string hitName = splitName[0];

            if (hitName == "Ennemy")
            {
                
                StartCoroutine(StompKnockback(Vector3.Distance(transform.position, hit.transform.position), hit.gameObject));
            }


            

        }
        isStomping = false;
        //isFirstAttack = false;
    }

    IEnumerator CacHit()
    {

        Collider[] CacCollider = Physics.OverlapSphere(cacPos.transform.position, 2.0f);
        foreach (Collider hit in CacCollider)
        {
            string[] splitName = hit.name.Split(char.Parse("_"));
            string hitName = splitName[0];
            if (hitName == "Ennemy")
            {
                GetComponent<T4_PlayerController>().DealDamage(false, hit.gameObject);
            }

               

        }

        yield return new WaitForSeconds(0.5f);

        isHitting = true;
        //isFirstAttack = false;
    }
    
    IEnumerator StompKnockback(float distance, GameObject hit)
    {
        //Debug.Log(hit.gameObject);
        float maxDist = 5.0f;
        float actualDist = distance;
        float speed = -(actualDist - maxDist);
        Vector3 lockPos = transform.position;

        while (actualDist < maxDist)
        {
            transform.position = new Vector3(lockPos.x, lockPos.y, lockPos.z);
            hit.transform.position = Vector3.MoveTowards(hit.transform.position, transform.position, -(speed) * 7 * Time.deltaTime);
            //Debug.Log(-(actualDist - maxDist));
            actualDist = Vector3.Distance(transform.position, hit.transform.position);
            //Debug.Log(hit.transform.position);
            //Debug.Log(actualDist);
            yield return new WaitForSeconds(0.01f);
        }
        GetComponent<T4_PlayerController>().DealDamage(true, hit.gameObject);
        yield return new WaitForSeconds(0.1f);
    }

    IEnumerator Dash(int distValue, Vector3 direction, Collider hit)
    {
        
        //Debug.Log(hit.gameObject.name);
        isFirstAttack = false;

        int speed;

        string[] splitName = hit.gameObject.name.Split(char.Parse("_"));
        string hitName = splitName[0];

        if (hitName == "Ennemy")
        {
            speed = 200;
        } else
        {
            speed = 100;
        }

        /*Debug.Log(speed);
        Debug.Log(distValue);*/

        float distance = Vector3.Distance(transform.position, direction);

        if (charaPool.characterIndex == 0)
        {
            if (distValue > 10 && hitName != "Ennemy")
            {



                while (distance > (distValue - (distValue / 3)))
                {

                    transform.position = Vector3.Lerp(
                    transform.position, direction,
                    Time.deltaTime * speed / distance);
                    distance = Vector3.Distance(transform.position, direction);
                    yield return new WaitForSeconds(0.01f);
                }



            }
            else if (/*distValue > 15 && */hitName == "Ennemy")
            {
                while (distance > 1f)
                {
                    transform.position = Vector3.Lerp(
                    transform.position, direction,
                    Time.deltaTime * speed / distance);
                    distance = Vector3.Distance(transform.position, direction);
                    yield return new WaitForSeconds(0.01f);
                }
                GetComponent<T4_PlayerController>().DealDamage(true, hit.gameObject);
            }
            else
            {
                while (distance > 1f)
                {
                    transform.position = Vector3.Lerp(
                    transform.position, direction,
                    Time.deltaTime * speed / distance);
                    distance = Vector3.Distance(transform.position, direction);
                    yield return new WaitForSeconds(0.01f);
                }
            }
        } else if (charaPool.characterIndex == 2)
        {
            if (distValue < 7)
            {
                while (distance > 1f)
                {
                    transform.position = Vector3.Lerp(
                    transform.position, direction,
                    Time.deltaTime * (speed / 2) / distance);
                    distance = Vector3.Distance(transform.position, direction);
                    yield return new WaitForSeconds(0.01f);
                }
            }
            else
            {
                
                for (float i = 0; i < 6; i += 0.3f)
                {
                    transform.position = Vector3.Lerp(
                    transform.position, direction,
                    Time.deltaTime * (speed / 3) / distance);
                    distance = Vector3.Distance(transform.position, direction);
                    yield return new WaitForSeconds(0.01f);
                }
                /*while (distance > 10)
                {
                    transform.position = Vector3.Lerp(
                    transform.position, direction,
                    Time.deltaTime * (speed / 3) / distance);
                    distance = Vector3.Distance(transform.position, direction);
                    yield return new WaitForSeconds(0.01f);
                }*/
            }
        }

        
        

        yield return new WaitForSeconds(0.1f);

        hasHit = false;
        isHitting = true;
    }
}
