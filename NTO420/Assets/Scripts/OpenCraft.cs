using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DefaultNamespace;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine.UI;
using Unity.VisualScripting;

public class OpenCraft : MonoBehaviour
{
   // [SerializeField] private PlayerRequests webAsker;
   // private PlayerResources resources;
    //[SerializeField] private PlayerStats stats; 
    [SerializeField] private LayerMask craftTableLayer;
    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private ManagerUI ui;
    //public Transform inventoryPanel;
    private float hitRange = 3f;
    RaycastHit hit;
    private void Start()
    {

    }
    private void Update()
    {
        if (hit.collider != null)
        {
           hit.collider.GetComponent<Highlight>()?.ToggleHightLight(false);
        }
        if(Physics.Raycast(playerCameraTransform.position,playerCameraTransform.forward,out hit,hitRange,craftTableLayer))
        {
            hit.collider.GetComponent<Highlight>()?.ToggleHightLight(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
              
                ui.OpenCraftMenu();
            }
            
        }
    }

}
