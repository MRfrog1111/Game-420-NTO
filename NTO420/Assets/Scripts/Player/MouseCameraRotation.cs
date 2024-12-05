using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCameraRotation : MonoBehaviour
{
    
    public float sensetivity;

    public Transform player;
    //public Transform arms;
    
    private float xRotation;
    private float yRotation;
    private float x;
    

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    
    private void Update()
    {
        yRotation = Input.GetAxis("Mouse Y") * sensetivity * Time.deltaTime;
        xRotation = Input.GetAxis("Mouse X") * sensetivity * Time.deltaTime;

        x -= yRotation;
        x = Mathf.Clamp(x, -90f, 90f);

        transform.localRotation = Quaternion.Euler(x, 0f, 0f);
        player.Rotate(Vector3.up * xRotation);
       // arms.localRotation = Quaternion.Euler(x, 0f, 0f);
    }   
    
}
