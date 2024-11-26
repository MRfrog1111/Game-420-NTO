using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MC_movement : MonoBehaviour
{
    public float movingSpeed;
    public float jumpForce;
    public static int oxygen = 100;
    public static float food = 100f;
    private int run;
    private int golod;

    private float x;
    private float z;

    private Rigidbody rb;
    private bool canJump = true;
    
    private void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        StartCoroutine(rashodOxygen());
        StartCoroutine(rashodFood());
    }

    

    private void Update()
    {
        Jump();
        if (Input.GetKey(KeyCode.LeftShift))
        {
            run = 2;
            if (x != 0 || z != 0) golod = 3;
        }
        else
        {
            run = 1;
            golod = 1;
        }
        x = Input.GetAxis("Horizontal") * movingSpeed * run;
        z = Input.GetAxis("Vertical") * movingSpeed * run;

    }
    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + transform.forward * z * Time.fixedDeltaTime + transform.right * x * Time.fixedDeltaTime);
    }

    private void Jump()
    {
        if (canJump && Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(Vector3.up*jumpForce*rb.mass,ForceMode.Impulse);
            canJump = false;
        }
    }
    private IEnumerator rashodFood()
    {
        while(food > 0)
        {
            yield return new WaitForSeconds(1f);
            food -= (1f / 12f) * golod;
            
        }
    }
    
    private IEnumerator rashodOxygen()
    {
        while (oxygen > 0)
        {
            yield return new WaitForSeconds(9f);
            --oxygen;
           
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        canJump = true;
    }
    /* private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Base") oxygen = 100;
    }*/



}
