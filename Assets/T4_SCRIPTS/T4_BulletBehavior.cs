using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T4_BulletBehavior : MonoBehaviour
{
    Rigidbody rb;
    MeshCollider col;
    public int speed;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<MeshCollider>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //rb.AddRelativeForce(new Vector3(0.0f, 0.0f, 5), ForceMode.VelocityChange);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
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
