using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlockBehavior : MonoBehaviour
{
    private GameObject robot;
    private Vector3 target;
    public bool isRobotMoving = false;
    public float plusPos;
    public float speed;

    void Start()
    {
        robot = GameObject.Find("Robot");
        transform.position = new Vector3(transform.position.x, transform.position.y+5f, transform.position.z);
    }
    private void Update()
    {
        if (isRobotMoving)
        {
            robot.transform.position = Vector3.MoveTowards(robot.transform.position, target, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position,target)<= 0.01f)
            {
                isRobotMoving = false;
            }
           
        }
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100f))
            {
                if (hit.collider.gameObject.name == gameObject.name)
                {
                    print(hit.collider.gameObject.name);
                    target = new Vector3(robot.transform.position.x+plusPos, robot.transform.position.y, robot.transform.position.z);
                    isRobotMoving = true;
                }
            }
        }
    }
}
