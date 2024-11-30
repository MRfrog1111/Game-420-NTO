using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour
{
    public GameObject[] buildings;
    public Transform playerCamera;
    private float hitRange = 3;
    RaycastHit hit;

    private void Update()
    {
        if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, hitRange))
        {
            if (Input.GetKeyDown(KeyCode.E)) AddBase(hit.collider.gameObject, buildings);
                
        }
    }

    public void AddBase(GameObject _base,GameObject[] builds)
    {
        bool canBuild = true;
        int j = 0;
        for (int i = 0; i < builds.Length; i++)
        {
            if(_base.tag == builds[i].tag)
            {
                int resursesCount = 0;
                foreach (SlotInventory slot in FindObjectsOfType<CollectResource>()[0].slots)
                {
                    if(slot.isEmpty) continue;
                    if(slot.item.itemName == builds[i].GetComponent<BuildItem>().buildItem.ToString())
                    {
                        resursesCount += slot.count;
                        j = i; 
                    }
                }
                if (resursesCount < builds[j].GetComponent<BuildResurses>().buildObjectCount) canBuild = false;
                else continue;
                
            }
        }
        if (canBuild)
        {
            hit.collider.gameObject.SetActive(false);
            buildings[j].gameObject.SetActive(true);
        }
    }
}
