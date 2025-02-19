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
    private int hitrange = 50;
    void Start()
    {
        robotMoving = GameObject.Find("Robot").GetComponent<RobotMoving>();
        placementSystem = GameObject.FindObjectOfType<GridObjectsPlacement>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
            if (Input.mousePosition.x <= screenPos.x + hitrange && Input.mousePosition.x >= screenPos.x - hitrange && Input.mousePosition.y <= screenPos.y + hitrange && Input.mousePosition.y >= screenPos.y - hitrange)
            {
                OnClicked();
            }
        }
    }

    void OnClicked()
    {
        Vector3 checkedPosition = GetComponentInParent<Transform>().position;
        commandLine = "";
        while (placementSystem.GetLowerBlock(checkedPosition) != -1)
        {
            commandLine += placementSystem.GetLowerBlock(checkedPosition);
            checkedPosition.z -= down;
        }
        robotMoving.StartProgramm(commandLine);
    }
    
}
