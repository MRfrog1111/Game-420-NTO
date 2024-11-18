using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_movement : MonoBehaviour
{
    public float movingSpeed;
    public float jumpForce;

    private float x;
    private float z;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        x = Input.GetAxis("Horizontal") * movingSpeed;
        z = Input.GetAxis("Vertical") * movingSpeed;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + transform.forward * z * Time.fixedDeltaTime + transform.right * x * Time.fixedDeltaTime);

       
    }

    
}
