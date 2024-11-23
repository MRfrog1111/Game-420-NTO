using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectResource : MonoBehaviour
{
    [SerializeField] private LayerMask pickableLayerMask;

    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private GameObject pickapUI;
    private float hitRange = 3;
    private RaycastHit hit;

    private void Update()
    {
        if (hit.collider != null)
        {
           hit.collider.GetComponent<Highlight>()?.ToggleHightLight(false);
        }
        if(Physics.Raycast(playerCameraTransform.position,playerCameraTransform.forward,out hit,hitRange,pickableLayerMask))
        {
            hit.collider.GetComponent<Highlight>()?.ToggleHightLight(true);
            if (Input.GetMouseButtonUp(0))
            {
                AddResource(hit.collider.gameObject);
            }
            
        }
    }

    void AddResource(GameObject obj)
    {
        print(obj.name);
        Destroy(obj);
    }
    
}
