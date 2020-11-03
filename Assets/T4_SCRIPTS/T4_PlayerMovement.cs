using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;

public class T4_PlayerMovement : MonoBehaviour
{

    Rigidbody rb;
    float movHorizontal;
    float movVertical;
    Vector3 movement;
    bool isCouroutineInactive = false;
    bool hasHit = false;
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
    }


    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.C))
        {
            isCouroutineInactive = true;
        }
 
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        
        movVertical = Input.GetAxis("Vertical");
        movHorizontal = Input.GetAxis("Horizontal");
        movement = new Vector3(movHorizontal, 0, movVertical);
        rb.MovePosition(rb.position + movement * 5 * Time.fixedDeltaTime);

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
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit))
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * hit.distance, Color.yellow);
                    //Debug.Log(hit.distance);
                    if (isCouroutineInactive)
                    {
                        hasHit = true;
                        StartCoroutine(Dash((int)hit.distance, hit.point, hit.collider));
                        isCouroutineInactive = false;
                    }
                }
            } else if (charaPool.characterIndex == 2)
            {
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out hit))
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * hit.distance, Color.yellow);
                    Debug.Log(hit.distance);
                    if (isCouroutineInactive)
                    {
                        hasHit = true;
                        StartCoroutine(Dash((int)hit.distance, hit.point, hit.collider));
                        isCouroutineInactive = false;
                    }
                }
            }

            
            
        }




    }



    IEnumerator Dash(int distValue, Vector3 direction, Collider hit)
    {
        
        //Debug.Log(hit.gameObject.name);
        isFirstAttack = false;

        int speed;

        if (hit.gameObject.name == "Ennemy")
        {
            speed = 200;
        } else
        {
            speed = 100;
        }

        Debug.Log(speed);/*
        Debug.Log(distValue);*/

        float distance = Vector3.Distance(transform.position, direction);

        if (charaPool.characterIndex == 0)
        {
            if (distValue > 10 && hit.gameObject.name != "Ennemy")
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
            else if (distValue > 15 && hit.gameObject.name == "Ennemy")
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
                while (distance > (distValue - (distValue / 3)))
                {
                    transform.position = Vector3.Lerp(
                    transform.position, direction,
                    Time.deltaTime * (speed / 3) / distance);
                    distance = Vector3.Distance(transform.position, direction);
                    yield return new WaitForSeconds(0.01f);
                }
            }
        }

        
        

        yield return new WaitForSeconds(0.1f);

        hasHit = false;
    }
}
