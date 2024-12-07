using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
//using static UnityEditor.Progress;

public class Paseka : MonoBehaviour
{
    public int maxHoney = 100;
    public int HoneyInSecond = 5;
    public int HoneyNow = 0;
    public float _time = 5f;

    public Transform Camera;
    public LayerMask layerMask;
    public Items Honey;
    [SerializeField] private PlayerStats stats;
    [SerializeField] private CollectResource slots;
    [SerializeField] private Tutorial tutor;
    private float hitRange = 3f;
    RaycastHit hit;

    private void Start()
    {
        StartCoroutine(Pasek());
        
    }

    private void Update()
    {
        Honey.count = HoneyNow;
        if (hit.collider != null)
        {
            hit.collider.GetComponent<Highlight>()?.ToggleHightLight(false);
        }
        if (Physics.Raycast(Camera.position, Camera.forward, out hit, hitRange, layerMask))
            {
                hit.collider.GetComponent<Highlight>()?.ToggleHightLight(true);
                if(Input.GetKeyDown(KeyCode.E))
                {
                TakeHoney(); 
                }
        }    
    }

    private IEnumerator Pasek()
    {
        while (true)
        {
            yield return new WaitForSeconds(_time);
            if (HoneyNow < maxHoney)
                HoneyNow += HoneyInSecond;
            else
                HoneyNow = 100;
            print("HoneyInPaseka " + HoneyNow);
        }
    }

   

    public void TakeHoney()
    {
        stats.CheckUpdates();
        stats.resources.honey += HoneyNow;
        stats.UpdateRes();
        HoneyNow = 0;
        tutor.isGotHoney = true;
        tutor.CheckStage();
        int temporary = 0;
        foreach (SlotInventory slot in slots.slots)
        {
            if (slot.item == Honey)
            {
               
                if (slot.count + HoneyNow <= Honey.Item.maxCount)
                {
                    slot.count += Honey.count;
                    slot.itemCountText.text = slot.count.ToString();
                    
                }
                else
                {
                    temporary = (slot.count + HoneyNow) - Honey.Item.maxCount;
                    return;
                    
                }
                break;
            }
        }

        foreach (SlotInventory slot in slots.slots)
        {
            if (slot.isEmpty == true)
            {
                //print(_item.name + _count);
                slot.item = Honey.Item;
                slot.count = Honey.count;
                slot.isEmpty = false;
                slot.SetIcon(Honey.Item.icon);
                slot.itemCountText.text = slot.count.ToString();
                break;
            }
        }
    }
}
