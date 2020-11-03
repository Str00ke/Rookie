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
    int dashVert;
    int dashHor;
    float test = 0f;
    bool isCouroutineInactive = false;
    bool hasHit = false;
    T4_PlayerController charaPool;


    private void Awake()
    {
        charaPool = FindObjectOfType<T4_PlayerController>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {

            if (movVertical != 0)
            {
                if (movVertical > 0)
                {
                    dashVert = 1;

                }
                else
                {
                    dashVert = -1;
                }
            }
                

            if (movHorizontal != 0)
            {
                if (movHorizontal > 0)
                {
                    dashHor = 1;
                }
                else
                {
                    dashHor = -1;
                }
            }
            

        }

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

        /*if (dashVert != 0 || dashHor != 0)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
            {
                if (hit.distance > 5)
                {
                    if (dashVert != 0)
                    {
                        StartCoroutine(DashVert(dashVert));
                    }

                    if (dashHor != 0)
                    {
                        StartCoroutine(DashHor(dashHor));
                    }

                }
                else
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                    print("Found an object - distance: " + hit.distance);
                }
                

            }
        }*/

        if (!hasHit)
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
                        StartCoroutine(Dash((int)hit.distance, hit.point));
                        isCouroutineInactive = false;
                    }
                }
            } else if (charaPool.characterIndex == 1)
            {
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out hit))
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * hit.distance, Color.yellow);
                    //Debug.Log(hit.distance);
                    if (isCouroutineInactive)
                    {
                        hasHit = true;
                        StartCoroutine(Dash((int)hit.distance, hit.point));
                        isCouroutineInactive = false;
                    }
                }
            }

            
        }
        





        //transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + dashUp);
        dashVert = 0;
        dashHor = 0;



    }

    IEnumerator DashVert(int value)
    {
        for (int i = 0; i < 10; i++) {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + value);
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator DashHor(int value)
    {
        for (int i = 0; i < 10; i++)
        {
            transform.position = new Vector3(transform.position.x + value, transform.position.y, transform.position.z);
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator Dash(int distValue, Vector3 direction)
    {
        /*float x;
        float z;
        Vector3 movementDash;
        //Debug.Log(distValue);

        /*if (direction.x > transform.position.x)
        {
            x = 1.0f;
            Debug.Log(direction.x - transform.position.x);
        }
        else if (direction.x < transform.position.x)
        {
            x = -1.0f;
            Debug.Log(transform.position.x - direction.x);
        }
        else
        {
            x = 0;
            Debug.Log("0");
        }
        if (direction.z > transform.position.z)
        {
            z = 1.0f;
            Debug.Log(direction.z - transform.position.z);
        }
        else if (direction.z < transform.position.z)
        {
            z = -1.0f;
            Debug.Log(transform.position.z - direction.z);
        }
        else
        {
            z = 0;
            Debug.Log("0");
        }

        if (direction.x != transform.position.x)
        {
            Debug.Log(direction.x - transform.position.x);
        } else
        {
            Debug.Log("0");
        }

        if (direction.z != transform.position.z)
        {
            Debug.Log(direction.z - transform.position.z);
        }
        else
        {
            Debug.Log("0");
        }

        //movementDash = new Vector3(x, 0, z);


        for (int i = 0; i < (distValue * 10); i++)
        {

            //Debug.Log(i + "<  " + (distValue * 10));
            //rb.MovePosition(rb.position + movementDash);
            //transform.position += (transform.position + movementDash / 10) ;
            //transform.position = new Vector3(transform.position.x + (movementDash.x / 10), transform.position.y, transform.position.z + (movementDash.z / 10));
            
            yield return new WaitForSeconds(0.01f);
        }*/

        

        float distance = Vector3.Distance(transform.position, direction);
        while (distance > 1f)
        {
            transform.position = Vector3.Lerp(
            transform.position, direction,
            Time.deltaTime * 80 / distance);
            distance = Vector3.Distance(transform.position, direction);
            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(0.1f);

        hasHit = false;
    }
}
