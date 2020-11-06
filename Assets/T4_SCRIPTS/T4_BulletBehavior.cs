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
    string shooterName;
    public GameObject impactExplosion;
    
    


    private void Awake()
    {
        playerMovement = FindObjectOfType<T4_PlayerMovement>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<MeshCollider>();

        

        if (shooterName == "Player")
        {
            speed = playerSpeed;
        } else
        {
            speed = enemySpeed;
        }
    }

    public void getName(string name)
    {
        shooterName = name;
        Debug.Log(shooterName);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(gameObject.name + " ,  " + shooterName);
        //rb.AddRelativeForce(new Vector3(0.0f, 0.0f, 5), ForceMode.VelocityChange);
        if (gameObject.name == "Missile(Clone)")
        {
            direction = Vector3.up;
        }
        else if (gameObject.name == "Bullet Alien(Clone)" && shooterName == "Ennemy")
        {
            direction = -Vector3.forward;
        } else
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


        if (hitName == "Ennemy" && shooterName != "Ennemy")
        {
            FindObjectOfType<T4_PlayerController>().DealDamage(false, other.gameObject);
            Instantiate(impactExplosion, transform.position, new Quaternion(0, 0, 0, 0), null);
            Destroy(gameObject);
        } else if (hitName == "Player" && shooterName != "Player")
        {
            FindObjectOfType<T4_EnemyController>().DealDamage(FindObjectOfType<T4_EnemyController>().damageDeal);
            Instantiate(impactExplosion, transform.position, new Quaternion(0, 0, 0, 0), null);
            Destroy(gameObject);
        } else if (other.gameObject.name == "Wall")
        {
            Destroy(gameObject);
        }


        

    }
}
