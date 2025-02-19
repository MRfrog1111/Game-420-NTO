using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeleteBlock : MonoBehaviour
{
    private RobotMoving robotMoving;
    private GridObjectsPlacement placementSystem;
    private int hitrange = 50;
    void Start()
    {
        placementSystem = GameObject.FindObjectOfType<GridObjectsPlacement>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
            if (Input.mousePosition.x <= screenPos.x + hitrange && Input.mousePosition.x >= screenPos.x - hitrange &&
                Input.mousePosition.y <= screenPos.y + hitrange && Input.mousePosition.y >= screenPos.y - hitrange)
            {
                placementSystem.DeleteBlock();
                Destroy(gameObject.transform.parent.gameObject);
            }
        }
    }
    
}