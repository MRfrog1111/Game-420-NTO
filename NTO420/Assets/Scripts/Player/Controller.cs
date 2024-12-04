using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float speed = 5f;
    private int run = 1;

    private PlayerResources resources;
    public CharacterController characterController;

    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        if (Input.GetKey(KeyCode.LeftShift) || resources.food > 0) run = 2;
        else run = 1;
        characterController.Move(move * speed * run * Time.deltaTime);


    }

    

}
