using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T4_BulletBehavior : MonoBehaviour
{
    Rigidbody rb;
    MeshCollider col;
    public int speed;
    Vector3 direction;
    T4_PlayerMovement playerMovement;

    private void Awake()
    {
        playerMovement = FindObjectOfType<T4_PlayerMovement>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<MeshCollider>();
        Debug.Log(gameObject.name);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //rb.AddRelativeForce(new Vector3(0.0f, 0.0f, 5), ForceMode.VelocityChange);
        if (gameObject.name == "Missile(Clone)")
        {
            direction = Vector3.up;
        }
        else
        {
            direction = Vector3.forward;
        }
        transform.Translate(direction * speed * Time.deltaTime);
        
    }

    



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name != "Player")
        {
            if (other.gameObject.name == "Ennemy")
            {
                Destroy(gameObject);
                Debug.Log("wow!");
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
    }
}
