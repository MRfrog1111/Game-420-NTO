using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Transform player;
    public float rotationSpeed = 100f;
    private float xRotaition = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
        xRotaition -= mouseY;
        xRotaition = Mathf.Clamp(xRotaition, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotaition, 0f, 0f);
        player.Rotate(Vector3.up * mouseX);



    }
}
