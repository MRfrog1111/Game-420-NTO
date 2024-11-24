using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Transform inventoryPanel;
    public List<SlotInventory> slots =  new List<SlotInventory>();
    private Camera mainCamera;
    public float distance = 3f;

    private void Start()
    {
        mainCamera = Camera.main;
        for (int i = 0; i < inventoryPanel.childCount; i++)
        {
            if(inventoryPanel.GetChild(0).GetComponent<SlotInventory>() != null)
            {
                slots.Add((inventoryPanel.GetChild(0).GetComponent<SlotInventory>()));

            }
        }
    }

    private void Update()
    {

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, distance))
        {
            
            if (hit.collider.gameObject.GetComponent<Items>() != null)
            {
                AddItem(hit.collider.gameObject.GetComponent<Items>().Item, hit.collider.gameObject.GetComponent<Items>().count);
                Destroy(hit.collider.gameObject);
            }
            Debug.DrawRay(ray.origin, ray.direction * distance);
            
        }
        
    }

    private void AddItem(ItemScriptableObject _item, int _count)
    {
        foreach(SlotInventory slot in slots)
        {
            if(slot.item == _item)
            {
                slot.count += _count;
                slot.itemCountText.text = slot.count.ToString();
                return;
            }
        }
        foreach(SlotInventory slot in slots)
        {
            if(slot.isEmpty == false)
            {
                slot.item = _item;
                slot.count = _count;
                slot.isEmpty = false;
                slot.SetIcon(_item.icon);
                slot.itemCountText.text = _count.ToString();
                break;
            }
        }
    }
}
