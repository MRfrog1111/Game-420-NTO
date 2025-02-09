using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlockBehavior : MonoBehaviour
{
    private RobotMoving robotMoving;
    public string commandLine = "";
    private GridObjectsPlacement placementSystem;
    private float  down = 2.02f;
    void Start()
    {
        //robot = GameObject.Find("Robot");
        robotMoving = GameObject.FindObjectOfType<RobotMoving>();
        transform.position = new Vector3(transform.position.x, transform.position.y+5f, transform.position.z);
        //grid = GameObject.FindObjectOfType<Grid>();
        placementSystem = GameObject.FindObjectOfType<GridObjectsPlacement>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000f))
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
        print(placementSystem.name);
        Vector3 checkedPosition = GetComponentInParent<Transform>().position;
        //checkedPosition = 
        while (placementSystem.GetLowerBlock(checkedPosition) != -1)
        {
            commandLine += placementSystem.GetLowerBlock(checkedPosition);
            checkedPosition.y -= down;
            break;
        }
        robotMoving.StartProgramm("RRM");
    }
    
}
