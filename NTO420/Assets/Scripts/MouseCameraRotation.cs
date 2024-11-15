using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCameraRotation : MonoBehaviour
{
    public Vector2 turn;

    public float sensetivity;

    public GameObject player;

    public GameObject pos;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        turn.x += Input.GetAxis("Mouse X")*Time.deltaTime *sensetivity;
        turn.y += Input.GetAxis("Mouse Y") * sensetivity*Time.deltaTime;
        transform.localRotation = Quaternion.Euler(-turn.y,turn.x,0);
        player.transform.localRotation = Quaternion.Euler(0, turn.x, 0);
        transform.position = pos.transform.position;
    }   
    
}
