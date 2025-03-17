using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NewController : MonoBehaviour
{

    [Header("Player Meaning")]
    public float MoveSpeed = 6f;
    public float jumpForce = 10f;
    [SerializeField] private float walkSpeed = 4f;
    [SerializeField] private float runSpeed = 4f;
    [SerializeField] private float acceleration = 10f;
    [SerializeField] private float fovChangeSpeed = 10f;

    [Header("Other Things")]
    [SerializeField] private float movementMultiply = 10f;
    [SerializeField] private float airMultiply = 0.4f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform orientaition;
    [SerializeField] private Transform player;
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask layerGround;
    [SerializeField] private AudioClip soundWalk;
    [SerializeField] private List<LayerMask> layerForSound;
    [SerializeField] private List<AudioClip> soundForWalk;

    private float horzintalMovement;
    private float verticalMovement;


    private float plauerHeight;
    private float groundDistance = 0.4f;

    private float initialFov = 80f;
    private float desiredFov = 100f;
    private float currentFov;


    private float groundDrag = 6f;
    private float airDrag = 2f;

    private Vector3 moveDirection;
    private Vector3 slopeMoveDirection;


    private Rigidbody rb;
    private RaycastHit slopeHit;

    private bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        plauerHeight = transform.localScale.y;
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, layerGround);

        MyInput();
        ControllDrag();
        ControlSpeed();

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            Sit();
        }
        else
        {
            Stay();
        }

        slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private bool OnSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, plauerHeight / 2 + 0.5f))
        {
            if (slopeHit.normal != Vector3.up)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    private void Sit()
    {
        transform.localScale = new Vector3(0, plauerHeight / 2, 0);
    }
    private void Stay()
    {
        transform.localScale = new Vector3(0, plauerHeight, 0);
    }

    private void MyInput()
    {
        horzintalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = orientaition.forward * verticalMovement + orientaition.right * horzintalMovement;
    }

    private void MovePlayer()
    {
        if (isGrounded && !OnSlope())
        {
            rb.AddForce(moveDirection.normalized * MoveSpeed * movementMultiply, ForceMode.Acceleration);

        }
        else if(isGrounded && OnSlope())
        { 
            rb.AddForce(slopeMoveDirection.normalized * MoveSpeed * movementMultiply, ForceMode.Acceleration);

        }
        else if(!isGrounded)
        {
            rb.AddForce(moveDirection.normalized * MoveSpeed * movementMultiply * airMultiply, ForceMode.Acceleration);

        }
    }

    private void ControlSpeed()
    {
        if (Input.GetKey(KeyCode.LeftShift) && isGrounded)
        {
            MoveSpeed = Mathf.Lerp(MoveSpeed, runSpeed, acceleration * Time.deltaTime);
            currentFov = Mathf.Lerp(currentFov, desiredFov, fovChangeSpeed * Time.deltaTime);
            cam.fieldOfView = currentFov;
        }
        else
        {
            MoveSpeed = Mathf.Lerp(MoveSpeed, walkSpeed, acceleration * Time.deltaTime);
            currentFov = Mathf.Lerp(currentFov, initialFov, fovChangeSpeed * Time.deltaTime);
            cam.fieldOfView = currentFov;
        }
    }


    private void ControllDrag()
    {
        if (isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = airDrag;
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(MoveSpeed != 0)
        {
            for (int i = layerForSound.Count - 1; i < layerForSound.Count; i++)
            {
                if(collision.gameObject.layer == layerForSound[i])
                {
                   soundWalk = soundForWalk[i];
                   
                }
            }
        }
        else
        {
            soundWalk = null;
        }
    }

}
