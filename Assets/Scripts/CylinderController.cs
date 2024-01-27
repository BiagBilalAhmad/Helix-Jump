using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderController : MonoBehaviour
{

    public float RotationSpeed;


    private void Update()
    {


        if (Input.GetMouseButton(0))
        {
        //    Debug.Log("masla");
            float Xpos = Input.GetAxisRaw("Mouse X");
            transform.Rotate(new Vector3(transform.position.x, -Xpos * RotationSpeed * Time.deltaTime, transform.position.z));

        }




        // For Mobile


        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                float Xpos = Input.GetTouch(0).deltaPosition.x;
                transform.Rotate(new Vector3(transform.position.x, -Xpos * RotationSpeed * Time.deltaTime, transform.position.z));
            }
        }
    }
}
