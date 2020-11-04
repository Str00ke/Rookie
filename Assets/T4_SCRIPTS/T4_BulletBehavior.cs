using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T4_BulletBehavior : MonoBehaviour
{
    Rigidbody rb;
    MeshCollider col;
    public float playerSpeed;
    public float enemySpeed;
    float speed;
    Vector3 direction;
    T4_PlayerMovement playerMovement;
    string hitName;

    


    private void Awake()
    {
        playerMovement = FindObjectOfType<T4_PlayerMovement>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<MeshCollider>();
        if (gameObject.name == "Player")
        {
            speed = playerSpeed;
        } else
        {
            speed = enemySpeed;
        }
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



    private void OnCollisionEnter(Collision collision)
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        string[] splitName = other.name.Split(char.Parse("_"));
        string hitName = splitName[0];

        if (hitName == "Ennemy")
        {
            FindObjectOfType<T4_PlayerController>().DealDamage(false, other.gameObject);
        } else if (hitName == "Player")
        {
            FindObjectOfType<T4_EnemyController>().DealDamage(FindObjectOfType<T4_EnemyController>().damageDeal);
        } else
        {
            Destroy(gameObject);
        }


        

    }
}
