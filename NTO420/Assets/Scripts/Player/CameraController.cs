using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CameraController : MonoBehaviour
{
    [Range(1f, 50f)] public static float sens;

    [SerializeField] private Transform cam;
    [SerializeField] private Transform orientaition;
    

    private float mouseX;
    private float mouseY;

    private float multiplayer = 0.01f;

    private float xRotaition;
    private float yRotaition;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        MyInput();

        cam.transform.localRotation = Quaternion.Euler(xRotaition, yRotaition, 0);
        orientaition.transform.rotation = Quaternion.Euler(0, yRotaition, 0);
        
    }

    private void MyInput()
    {
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        yRotaition += mouseX * sens * multiplayer;
        xRotaition -= mouseY * sens * multiplayer;

        xRotaition = Mathf.Clamp(xRotaition, -90f, 90f);   
    }


}
