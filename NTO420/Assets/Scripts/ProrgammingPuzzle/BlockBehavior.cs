using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlockBehavior : MonoBehaviour
{
    public GameObject robot;
    private Vector3 target;
    public bool isRobotMoving = false;
    public float plusPos;
    private void Update()
    {
        if (isRobotMoving)
        {
            robot.transform.position = Vector3.MoveTowards(robot.transform.position, target, 0.01f);
           /* if (robot.transform.position.x - target.x <= 0.1f)
            {
                isRobotMoving = false;
            }*/
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
