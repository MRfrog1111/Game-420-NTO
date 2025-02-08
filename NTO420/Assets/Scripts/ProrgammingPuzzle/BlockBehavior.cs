using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlockBehavior : MonoBehaviour
{
    private RobotMoving robotMoving;
    void Start()
    {
        //robot = GameObject.Find("Robot");
        robotMoving = GameObject.FindObjectOfType<RobotMoving>();
        transform.position = new Vector3(transform.position.x, transform.position.y+5f, transform.position.z);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100f))
            {
                if (hit.collider.gameObject.name == gameObject.name)
                {
                    print(hit.collider.gameObject.name);
                    OnClicked();
                }
            }
        }
    }

    void OnClicked()
    {
        //
        if (gameObject.name == "R")
        {
            robotMoving.RotateRobot();
            
        }
        else
        {
            print('A');
            robotMoving.MoveRobot();
        }
    }
}
