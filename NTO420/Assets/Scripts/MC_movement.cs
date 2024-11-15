using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_movement : MonoBehaviour
{
    public float movingSpeed;

    public float jumpForce;

    private Rigidbody rb;

    public Vector3 movingVector;

    public GameObject camera;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //gameObject.transform.rotation = Quaternion.Euler(gameObject.transform.rotation.x,camera.transform.rotation.y,gameObject.transform.rotation.z);
        if (Input.GetKey(KeyCode.W))
        {
            movingVector = transform.forward;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            movingVector = -transform.forward;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            movingVector = -transform.right;
        }

        else if (Input.GetKey(KeyCode.D))
        {
            movingVector = transform.right;
        }

        else
        {
            movingVector = Vector3.zero;
        }
        rb.MovePosition(rb.position + movingVector.normalized * (movingSpeed * Time.deltaTime));
    }

    private void Move()
    {
        
    }
}
