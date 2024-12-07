using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using static UnityEditor.Progress;

public class Generator : MonoBehaviour
{
    public Transform camera;
    public LayerMask pickableLayerMask;
    public CollectResource slots;
    

    private float hitRange = 3f;
    private RaycastHit hit;

    public int MaxHoney = 100;
    public int rashodHoney = 100;
    public int timeInSecond = 100;
    public int honeyNow;

    private void Start()
    {
        StartCoroutine(GeneratorRashod());
    }

    private void Update()
    {
        if (Physics.Raycast(camera.position, camera.forward, out hit, hitRange, pickableLayerMask))
        {
           if(Input.GetKey(KeyCode.E))
            {
                GeneratorOfHoney();
            }
        }
        
    }

    private void GeneratorOfHoney()
    {
        foreach (SlotInventory slot in slots.slots)
        {
            if (slot.item.itemType == ItemType.Honey)
            {
                StartCoroutine(HoneyFull(slot));
                if (slot.count == 0)
                {
                    //обновление слота
                    break;
                }

            }
        }
    }

    private IEnumerator GeneratorRashod()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeInSecond);
            if(honeyNow > 0)
            {
                honeyNow--;
                print(honeyNow);
            }
        }
    }

    private IEnumerator HoneyFull(SlotInventory _slot)
    {
        while(true)
        {
            yield return new WaitForSeconds(0.1f);
            
            if(honeyNow < MaxHoney)
            {
                _slot.count--;
                honeyNow++;
            }
        }
    }
}
