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
    bool testB = false;
    bool hasHit = false;

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
            testB = true;
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
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                Debug.Log(hit.point);
                if (testB)
                {
                    hasHit = true;
                    StartCoroutine(Dash((int)hit.distance, hit.point));
                    testB = false;
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
        float x;
        float z;
        Vector3 movementDash;
        Debug.Log(distValue);
        

        
        for (int i = 0; i < distValue * 10; i++)
        {

            if (direction.x > transform.position.x)
            {
                x = 0.1f;
            }
            else if (direction.x < transform.position.x)
            {
                x = -0.1f;
            }
            else
            {
                x = 0;
            }
            if (direction.z > transform.position.z)
            {
                z = 0.1f;
            }
            else if (direction.z < transform.position.z)
            {
                z = -0.1f;
            }
            else
            {
                z = 0;
            }

            movementDash = new Vector3(x, 0, z);

            rb.MovePosition(rb.position + movementDash);

            
            yield return new WaitForSeconds(0.01f);
        }

        hasHit = false;
    }
}
