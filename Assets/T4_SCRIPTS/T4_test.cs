using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T4_test : MonoBehaviour
{

    Vector3 difference;
    float rotationZ;
    void Update () {
        /*if (Input.GetKeyDown(KeyCode.A))
        {
            //Vector3 mousePos = new Vector3(Input.mousePosition.x, 0, Input.mousePosition.z);
            //Vector3 lookPos = Camera.main.ScreenToWorldPoint(mousePos);
            //rotationZ = -(Mathf.Atan2(lookPos.x, lookPos.y) * Mathf.Rad2Deg + 90f);
            //transform.rotation = Quaternion.Euler(0f, rotationZ, 0f);
            //Debug.Log(mousePos);
            Vector3 mousePos = new Vector3(Input.mousePosition.x, 0, Input.mousePosition.z);
            transform.Rotate(Camera.main.ScreenToWorldPoint(mousePos));
        }*/

        Vector3 mouse_pos = Input.mousePosition;

        Vector3 object_pos = Camera.main.WorldToScreenPoint(transform.position);
        mouse_pos.x = mouse_pos.x - object_pos.x;
        mouse_pos.y = mouse_pos.y - object_pos.y;
        float angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, -angle, 0));
    }
}
