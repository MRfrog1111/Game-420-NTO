using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DefaultNamespace;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine.UI;
using Unity.VisualScripting;

public class CollectResource : MonoBehaviour
{
    [SerializeField] private PlayerRequests webAsker;
    private PlayerResources resources;
    [SerializeField] private LayerMask pickableLayerMask;
    [SerializeField] private Transform playerCameraTransform;
    
    public List<SlotInventory> slots = new List<SlotInventory>();
    public Transform inventoryPanel;
    private float hitRange = 3;
    RaycastHit hit;

    private void Start()
    {
        for (int i = 0; i < inventoryPanel.childCount; i++)
        {
            if (inventoryPanel.GetChild(i).GetComponent<SlotInventory>() != null)
            {
                slots.Add((inventoryPanel.GetChild(i).GetComponent<SlotInventory>()));

            }
        }
    }
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
                AddItem(hit.collider.GetComponent<Items>().Item, hit.collider.GetComponent<Items>().count);
                Destroy(hit.collider.gameObject);
            }
            
        }
    }
    public void GetRes(PlayerResources res)
    {
        resources = res;
    }
    

    private void AddItem(ItemScriptableObject _item, int _count)
    {
        StartCoroutine(webAsker.GetPlayerResources(GetRes));
        foreach (SlotInventory slot in slots)
        {
            if (slot.item == _item)
            {
                if (slot.count + _count <= _item.maxCount)
                { 
                    slot.count += _count;
                    slot.itemCountText.text = slot.count.ToString();
                    return;
                }
                break;
            }
        }
        foreach (SlotInventory slot in slots)
        {
            if (slot.isEmpty == true)
            {
                print(slot.item.name)
                slot.item = _item;
                slot.count = _count;
                slot.isEmpty = false;
                slot.SetIcon(_item.icon);
                slot.itemCountText.text = _count.ToString();
                switch (slot.item.name)
                {
                    case "Honey":
                        resources.honey += slot.count;
                        break;
                    default:
                        print("there's no such resource");
                        break;
                }
                break;
            }
        }
        StartCoroutine(webAsker.UpdatePlayerResources(resources));
        StartCoroutine(webAsker.GetPlayerResources(GetRes));
    }

}
