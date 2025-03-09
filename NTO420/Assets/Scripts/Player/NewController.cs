using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NewController : MonoBehaviour
{
    public float MoveSpeed = 6f;
    public float movementMultiply = 10f;

    private float horzintalMovement;
    private float verticalMovement;
    private float drag = 6f;

    private Vector3 moveDirection;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        MyInput();
        ControllDrag();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horzintalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = transform.forward * verticalMovement + transform.right * horzintalMovement;
    }

    private void MovePlayer()
    {
        rb.AddForce(moveDirection.normalized * MoveSpeed * movementMultiply, ForceMode.Acceleration);
    }

    private void ControllDrag()
    {
        rb.drag = drag;
    }

}
