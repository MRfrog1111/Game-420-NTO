using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DefaultNamespace;
public class CraftHoneyEssence : MonoBehaviour
{
    [SerializeField] private PlayerStats stats;
    [SerializeField] private Tutorial tutor;
    [SerializeField] private BuildItem essence;
    [SerializeField] private CollectResource collectRes;
    [SerializeField] private ItemScriptableObject honeyEssence;
    private int minus;
    
    public void CraftEssence()
    {
        int canBuild = 0;
        //stats.CheckUpdates();
        int l = 0;
        for (int j = 0; j < essence.buildItem.buildResurses.Count; j++)
        {
            foreach (SlotInventory slot in FindObjectsOfType<CollectResource>()[0].slots)
            {
                int resursesCount = 0;
                if (slot.isEmpty) continue;
                if (slot.item == essence.buildItem.buildResurses[j].buildObject)
                {
                    resursesCount += slot.count;

                }

                if (resursesCount >= essence.buildItem.buildResurses[j].buildObjectCount) canBuild++;


            }
        }

        if (canBuild >= essence.buildItem.buildResurses.Count)
        {
            for (int j = 0; j < essence.buildItem.buildResurses.Count; j++)
            {
                minus = essence.buildItem.buildResurses[j].buildObjectCount;
                foreach (SlotInventory slot in FindObjectsOfType<CollectResource>()[0].slots)
                {
                    if (minus > 0 && slot.item == essence.buildItem.buildResurses[j].buildObject)
                    {
                        if (minus < slot.count)
                        {
                            slot.count -= minus;
                            slot.itemCountText.text = slot.count.ToString();
                        }
                        else 
                        {
                            minus -= slot.count; 
                            slot.count = 0;
                            slot.itemCountText.text = "";
                            // slot._icon.GetComponent<Image>().color = new Color(1, 1, 1, 0);
                            slot.item = null;
                            slot._icon.GetComponent<Image>().sprite = null;
                            slot._icon = null;
                            slot.isEmpty = true;
                            //slot._icon = null;
                        }
                    }

                }
            //stats.resources.honey_esence += 1;
            }
            print("a");
            stats.resources.minerals -= essence.buildItem.buildResurses[0].buildObjectCount;
            stats.resources.honey -= essence.buildItem.buildResurses[0].buildObjectCount;
            stats.resources.honey_esence += 1;
            
            AddItem(honeyEssence, 1);
            stats.UpdateRes();
        }
    }

    private void AddItem(ItemScriptableObject _item, int _count)
    {
        //StartCoroutine(stats.webAsker.GetPlayerResources(stats.GetRes));
        stats.CheckUpdates();
        //print("item" + _item.name);
        bool isIn = false;
        PlayerChangesLogs changes = new PlayerChangesLogs();
        foreach (SlotInventory slot in collectRes.slots)
        {
            if (slot.item == _item)
            {
                //print(slot.count + _count + " " + _item.maxCount);
                if (slot.count + _count <= _item.maxCount)
                {
                    isIn = true;
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

        /*if (!isIn)
        {*/
            foreach (SlotInventory slot in collectRes.slots)
            {
                if (slot.isEmpty == true)
                {
                    // print(_item.name + _count);
                    slot.item = _item;
                    slot.count = _count;
                    slot.isEmpty = false;
                    slot.SetIcon(_item.icon);
                    slot.itemCountText.text = _count.ToString();
                    //stats.resources.honey_esence += slot.count;
                    changes.honey_esence_change = "+" + _count.ToString();
                    StartCoroutine(collectRes.webAsker.SendLog("crafted honey essence", changes));
                    stats.UpdateRes();
                    tutor.CheckStage();
                    break;
                }
            }
       // }
    }

}
