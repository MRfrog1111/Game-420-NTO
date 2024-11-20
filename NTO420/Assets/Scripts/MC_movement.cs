using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_movement : MonoBehaviour
{
    public float movingSpeed;
    public float jumpForce;
    private int run;
    private int golod;



    private float x;
    private float z;

    private Rigidbody rb;
    private PlayerStats stuts;

    private void Start()
    {
        stuts = GetComponent<PlayerStats>();
        rb = GetComponent<Rigidbody>();
    }

    

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            run = 2; 
            if ((x != 0 || z != 0) && stuts.golod != 3)  stuts.golod = 3;
        }
        else
        {
            run = 1;
            if (stuts.golod != 1)
                stuts.golod = 1;
        }
        x = Input.GetAxis("Horizontal") * movingSpeed * run;
        z = Input.GetAxis("Vertical") * movingSpeed * run;

    }
    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + transform.forward * z * Time.fixedDeltaTime + transform.right * x * Time.fixedDeltaTime);
    }

    
}
