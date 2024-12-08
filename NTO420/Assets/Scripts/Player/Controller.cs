using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float speed = 5f;
    public float gravity = 5f;


    public int jumpForse = 2;
    private int run = 1;

    private Vector3 velocity;

    private PlayerResources resources;
    public CharacterController characterController;
    public AudioSource walk;

    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        if (Input.GetKey(KeyCode.LeftShift) || resources.food > 0) run = 2;
        else run = 1;

        Walk(move);
        if (Input.GetKey(KeyCode.Space) && characterController.isGrounded)
            Jump();

    }
    private void FixedUpdate()
    {
        Gravity(characterController.isGrounded);
    }

    private void Gravity(bool isGrounded)
    {
        if(isGrounded && velocity.y < 0)
            velocity.y = -1f;
        velocity.y -= gravity * Time.fixedDeltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    private void Jump()
    {
        velocity.y = jumpForse;
    }
    private void Walk(Vector3 move)
    {
        characterController.Move(move * speed * run * Time.deltaTime);
        if((move.x != 0 || move.z != 0)&&!walk.isPlaying)
        {
            walk.Play();
        }
    }
}
