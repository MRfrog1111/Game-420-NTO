using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMoving : MonoBehaviour
{
    private Vector3 target;
    public bool isRobotMoving = false;
    public float plusPos;
    public float speed;
    public GameObject robot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isRobotMoving)
        {
            robot.transform.position = Vector3.MoveTowards(robot.transform.position, target, speed * Time.deltaTime);
        }

        //target = new Vector3(robot.transform.position.x+plusPos, robot.transform.position.y, robot.transform.position.z);
    }

    public void MoveRobot()
    {
        switch (transform.eulerAngles.y)
        {
            case 0:
                target = new Vector3(robot.transform.position.x+plusPos, robot.transform.position.y, robot.transform.position.z);
                break;
            case 90:
                target = new Vector3(robot.transform.position.x, robot.transform.position.y, robot.transform.position.z-plusPos);
                break;
            case 180:
                target = new Vector3(robot.transform.position.x-plusPos, robot.transform.position.y, robot.transform.position.z);
                break;
            case 270:
                target = new Vector3(robot.transform.position.x, robot.transform.position.y, robot.transform.position.z+plusPos);
                break;
        }
        isRobotMoving = true;
    }

    public void RotateRobot()
    {
        robot.transform.eulerAngles = new Vector3(90, robot.transform.eulerAngles.y + 90, 0);
    }
}
