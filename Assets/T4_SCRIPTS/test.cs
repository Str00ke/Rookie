using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{

    Vector3 difference;
    float rotationZ;
    void Update () {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Vector3 mousePos = new Vector3(Input.mousePosition.x, 0, Input.mousePosition.z);
            Vector3 lookPos = Camera.main.ScreenToWorldPoint(mousePos);
            //rotationZ = -(Mathf.Atan2(lookPos.x, lookPos.y) * Mathf.Rad2Deg + 90f);
            //transform.rotation = Quaternion.Euler(0f, rotationZ, 0f);*/
            Debug.Log(lookPos);
        }
        

    }
}
