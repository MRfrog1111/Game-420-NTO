using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DefaultNamespace;
public class CollectResource1 : MonoBehaviour
{
    [SerializeField] private PlayerRequests webAsker;
    private PlayerResources resources;
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
    public void GetRes(PlayerResources res)
    {
        resources = res;
    }
    void AddResource(GameObject obj)
    {
        StartCoroutine(webAsker.GetPlayerResources(GetRes));
        PlayerChangesLogs changes = new PlayerChangesLogs();
        switch (obj.name)
        {
            case "Honey":
                resources.honey += 1;
                changes.honey_change = "+1";
                StartCoroutine(webAsker.SendLog("collected honey",changes));
                break;
            case "Vosk":
                resources.vosk += 1;
                changes.vosk_change = "+1";
                StartCoroutine(webAsker.SendLog("collected vosk",changes));
                break;
            default:
                print("there's no such resource");
                break;
        }
        StartCoroutine(webAsker.UpdatePlayerResources(resources));
        StartCoroutine(webAsker.GetPlayerResources(GetRes));
        Destroy(obj);
    }
    
}
