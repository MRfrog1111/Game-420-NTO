using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Build : MonoBehaviour
{
    public GameObject[] buildings;
    public PlayerStats stats;
    public Transform playerCamera;
    private float hitRange = 3;
    RaycastHit hit;

   /* private void Update()
    {
        if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, hitRange))
        {
            if (Input.GetKeyDown(KeyCode.E)) AddBase(hit.collider.gameObject, buildings);
                
        }
    }*/

    public void AddBase(GameObject _base,GameObject[] builds)
    {
        print("build1");
        int canBuild = 0;
        stats.CheckUpdates();
        int l = 0;
        for (int i = 0; i < builds.Length; i++)
        {
            if(_base.tag == builds[i].tag)
            {
                print(i + " " +  builds[i].GetComponent<BuildItem>().buildItem.buildResurses.Count);
                for (int j = 0; j < builds[i].GetComponent<BuildItem>().buildItem.buildResurses.Count; j++)
                {
                    foreach (SlotInventory slot in FindObjectsOfType<CollectResource>()[0].slots)
                    {
                        int resursesCount = 0;
                        if (slot.isEmpty) continue;
                        if (slot.item == builds[i].GetComponent<BuildItem>().buildItem.buildResurses[j].buildObject)
                        {
                            resursesCount += slot.count;
                            l = i;
                            
                        }
                        if (resursesCount >= builds[i].GetComponent<BuildItem>().buildItem.buildResurses[j].buildObjectCount) canBuild++;
                        

                    }
                }


            }
        }
        if (canBuild >= builds[l].GetComponent<BuildItem>().buildItem.buildResurses.Count)
        {
            _base.SetActive(false);
            buildings[l].gameObject.SetActive(true);
            for (int j = 0; j < builds[l].GetComponent<BuildItem>().buildItem.buildResurses.Count; j++)
            {
                /*foreach (SlotInventory slot in FindObjectsOfType<CollectResource>()[0].slots)
                {
                   
                   
                    if (slot.item == builds[l].GetComponent<BuildItem>().buildItem.buildResurses[j].buildObject)
                    {
                        slot.count -= builds[l].GetComponent<BuildItem>().buildItem.buildResurses[j].buildObjectCount;
                       /* if(slot.count == 0)
                        {
                           
                        }*
                    }
                    
                }*/
                switch (builds[l].GetComponent<BuildItem>().buildItem.buildResurses[j].buildObject.name)
                {
                    case "Wax":
                        stats.resources.wax -= builds[l].GetComponent<BuildItem>().buildItem.buildResurses[j]
                            .buildObjectCount;
                        break;
                    case "SiliconSand":
                        stats.resources.silicon_sand -= builds[l].GetComponent<BuildItem>().buildItem.buildResurses[j]
                            .buildObjectCount;
                        break;
                    case "Minerals":
                        stats.resources.minerals -= builds[l].GetComponent<BuildItem>().buildItem.buildResurses[j]
                            .buildObjectCount;
                        break;
                    default:
                        break;
                }
                stats.UpdateRes();
            }
        }
    }
}
