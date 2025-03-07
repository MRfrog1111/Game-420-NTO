using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NewController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody; 
    [SerializeField] private Transform head;
    [SerializeField] private Transform body;
    [SerializeField] private GameObject groundChek;
    [SerializeField] private LayerMask ground;

    private Vector3 _movement;
    private float _xRotaition;
    private float _yRotaition;
    private bool _isGrounded;

    public float speedRotaition;



    private void Update()
    {
        Look();
    }

    private void Look()
    {
        float x = Input.GetAxis("Horizontal") * speedRotaition * Time.deltaTime;
        float y = Input.GetAxis("Vertical") * speedRotaition * Time.deltaTime;

        _xRotaition += x;
        _yRotaition -= y;

        _yRotaition = Mathf.Clamp(_yRotaition, -80f, 90f);
        _xRotaition = Mathf.Repeat(_xRotaition, 360f);

        head.localRotation = Quaternion.Euler(_yRotaition, 0f, 0f);
        body.localRotation = Quaternion.Euler(0f, _xRotaition, 0f);

    }

    private void Move()
    {
        //Vector3 direction = ;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == ground)
            _isGrounded = true;
        else 
            _isGrounded = false;
    }
}
