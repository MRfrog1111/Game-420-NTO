using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using static UnityEditor.Progress;

public class Paseka : MonoBehaviour
{
    public int maxHoney = 100;
    public int HoneyInSecond = 5;
    public int HoneyNow = 0;
    public float _time = 5f;

    public Transform Camera;
    public LayerMask layerMask;
    public ItemScriptableObject Honey;
    [SerializeField] private PlayerStats stats;
    [SerializeField] private CollectResource slots;

    private float hitRange = 3f;
    RaycastHit hit;

    private void Start()
    {
        StartCoroutine(Pasek());
        
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(Camera.position, Camera.forward, out hit, hitRange, layerMask))
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
        stats.resources.honey += HoneyNow;
        HoneyNow = 0;
        
        foreach (SlotInventory slot in slots.slots)
        {
            if (slot.item == Honey)
            {
               
                if (slot.count + HoneyNow <= Honey.maxCount)
                {
                    slot.count += HoneyNow;
                    slot.itemCountText.text = slot.count.ToString();
                    
                }
                else
                {
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
                slot.item = Honey;
                slot.count = HoneyNow;
                slot.isEmpty = false;
                slot.SetIcon(Honey.icon);
                slot.itemCountText.text = HoneyNow.ToString();

            }
        }
    }
}
