using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test2 : MonoBehaviour
{
    bool isPicked = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 2);
            Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }

        if (Input.GetButton("Fire1"))
        {
            Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 2);
            Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }*/

        if (Input.GetButtonDown("Fire1")) {
            isPicked = !isPicked;
        }

        if (isPicked)
        {

            //transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //transform.position = new Vector3(Input.mousePosition.x, 0, Input.mousePosition.y);
            
        }

        //Vector3 ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));

    }
}
