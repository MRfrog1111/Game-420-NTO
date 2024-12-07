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
   // private PlayerResources resources;
    [SerializeField] private PlayerStats stats;
    [SerializeField] private LayerMask pickableLayerMask;
    [SerializeField] private Transform playerCameraTransform;
    
    public List<SlotInventory> slots = new List<SlotInventory>();
    public Transform inventoryPanel;
    private float hitRange = 3f;
    RaycastHit hit;

    [SerializeField] private ItemScriptableObject[] startObjects;
    public static event Action onResourcesChange;
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

    public void FirstUpdate()
    {
        if (stats.resources.honey > 0)
        {
          AddFirstItem(0,stats.resources.honey);
        }
        if (stats.resources.wax > 0)
        {
            AddFirstItem(1,stats.resources.wax);
        }
        if (stats.resources.minerals > 0)
        {
            AddFirstItem(2,stats.resources.minerals);
        }
        if (stats.resources.silicon_sand > 0)
        {
            AddFirstItem(3,stats.resources.silicon_sand);
        }
    }

    private void AddFirstItem(int n, int _count)
    {
        slots[n].item = startObjects[n];;
        slots[n].count = _count;
        slots[n].isEmpty = false;
        slots[n].SetIcon(startObjects[n].icon);
        slots[n].itemCountText.text = _count.ToString();
    }
    private void OnEnable()
    {
        PlayerStats.onResourcesChange += FirstUpdate;

    }

    private void OnDisable() {
        PlayerStats.onResourcesChange -= FirstUpdate;
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
            if (Input.GetKeyDown(KeyCode.E))
            {
                AddItem(hit.collider.GetComponent<Items>().Item, hit.collider.GetComponent<Items>().count);
                //print(hit.collider.gameObject.name);
                Destroy(hit.collider.gameObject);
               
            }
            
        }
    }
  /*  public void GetRes(PlayerResources res)
    {
        resources = res;
    }
    */

    private void AddItem(ItemScriptableObject _item, int _count)
    {
        //StartCoroutine(stats.webAsker.GetPlayerResources(stats.GetRes));
        stats.CheckUpdates();
        //print("item" + _item.name);
        PlayerChangesLogs changes = new PlayerChangesLogs();
        foreach (SlotInventory slot in slots)
        {
            if (slot.item == _item)
            {
                print(slot.count + _count + " " + _item.maxCount);
                if (slot.count + _count <= _item.maxCount)
                { 
                    slot.count += _count;
                    slot.itemCountText.text = slot.count.ToString();
                    //
                }
                else
                {
                    return;
                }
                break;
            }
        }
        
        foreach (SlotInventory slot in slots)
        {
            if (slot.isEmpty == true)
            {
               // print(_item.name + _count);
                slot.item = _item;
                slot.count = _count;
                slot.isEmpty = false;
                slot.SetIcon(_item.icon);
                slot.itemCountText.text = _count.ToString();
               
                switch (_item.name)
                {
                    case "Honey":
                        print("axaxaxaxa");
                        stats.resources.honey += slot.count;
                        //print("inside " + stats.resources.honey);
                        changes.honey_change = "+"+_count.ToString();
                        StartCoroutine(webAsker.SendLog("collected honey",changes));
                        break;
                    case "Wax":
                        stats.resources.wax += slot.count;
                        changes.vosk_change = "+"+_count.ToString();
                        StartCoroutine(webAsker.SendLog("collected wax",changes));
                        break;
                    case "Minerals":
                        stats.resources.minerals += slot.count;
                        changes.minerals_change = "+"+_count.ToString();
                        StartCoroutine(webAsker.SendLog("collected minerals after killing bug",changes));
                        break;
                    case "SiliconSand":
                        stats.resources.silicon_sand += slot.count;
                        changes.silicon_sand_change = "+"+_count.ToString();
                        StartCoroutine(webAsker.SendLog("collected dilicon sand",changes));
                        break;
                    default:
                        print("there's no such resource");
                        break;
                }
                break;
            }
        }
        //print("chekck3 " + stats.resources.honey);
        stats.UpdateRes();
        stats.CheckUpdates();
        //StartCoroutine(webAsker.UpdatePlayerResources(stats.resources));
        //StartCoroutine(webAsker.GetPlayerResources(stats.GetRes));
        onResourcesChange?.Invoke();
    }

}
