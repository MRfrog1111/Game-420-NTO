using System;
using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Generator1 : MonoBehaviour
{
    public Transform camera;
    public LayerMask pickableLayerMask;
    public CollectResource slots;
    public PlayerStats stats;
    public Tutorial tutor;
    private float hitRange = 3f;
    private RaycastHit hit;
    public TextMeshProUGUI generatortext;
    
    public int MaxHoney = 100;
    public int rashodHoney = 100;
    public int timeInSecond = 100;
    public int honeyNow;

    public ManagerUI ui;
    public Paseka paseka;
    private void Start()
    {
        StartCoroutine(GeneratorRashod());
    }

    private void Update()
    {
        generatortext.text = honeyNow.ToString() + "%";
        if (hit.collider != null)
        {
            hit.collider.GetComponent<Highlight>()?.ToggleHightLight(false);
        }
        if (Physics.Raycast(camera.position, camera.forward, out hit, hitRange, pickableLayerMask))
        {
            hit.collider.GetComponent<Highlight>()?.ToggleHightLight(true);
           if(Input.GetKey(KeyCode.E))
            {
                if (!tutor.isUsedGenerator)
                {
                    tutor.isUsedGenerator = true;
                }

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
                    slot.itemCountText.text = "";
                    slot._icon.GetComponent<Image>().sprite = null;
                    slot.item = null;
                    slot._icon = null;
                    slot.isEmpty = true;
                    slot._icon = null;
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
                if (!ui.isWorking)
                {
                    paseka.isWorking = true;
                    ui.isWorking = true;
                    stats.isWorking = true;
                }

                print(honeyNow);
            }
            else
            {
                if (ui.isWorking)
                {
                    paseka.isWorking = false;
                    ui.isWorking = false;
                    stats.isWorking = false;
                }
            }
        }
    }

    private IEnumerator HoneyFull(SlotInventory _slot)
    {
        /*while(Input.GetKey(KeyCode.E) && _slot.count > 0)
        {*/
            yield return new WaitForSeconds(0.1f);
            
            if(_slot.count > 0 && honeyNow < MaxHoney)
            {
                _slot.count--;
                _slot.itemCountText.text = _slot.count.ToString();
                stats.resources.honey--;
                honeyNow++;
                stats.UpdateRes();
            }
        //}
    }
}
