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
        bool canBuild = false;
        int j = 0;
        int l =0;
        for (int i = 0; i < builds.Length; i++)
        {
            if(_base.tag == builds[i].tag)
            {
                
                foreach (SlotInventory slot in FindObjectsOfType<CollectResource>()[0].slots)
                {
                    int resursesCount = 0;
                    if (slot.isEmpty) continue;
                    if(slot.item == builds[i].GetComponent<BuildItem>().buildItem.buildResurses[j].buildObject)
                    {
                        resursesCount += slot.count;
                        l = i; 
                    }
                    if (resursesCount >= builds[i].GetComponent<BuildItem>().buildItem.buildResurses[j].buildObjectCount) canBuild = true;
                    else canBuild = false;
                    j++;
                }
                
                
            }
        }
        if (canBuild)
        {
            _base.SetActive(false);
            buildings[l].gameObject.SetActive(true);
        }
    }
}
