using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T4_FlameThrower : MonoBehaviour
{
    T4_PlayerController player;
    Quaternion playerRot;
    Quaternion flameRot;
    // Start is called before the first frame update

    private void Awake()
    {
        player = FindObjectOfType<T4_PlayerController>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        flameRot.SetFromToRotation(transform.position, player.transform.position);
        Debug.Log(flameRot);
        //Debug.Log(player.transform.rotation.y);*/
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
