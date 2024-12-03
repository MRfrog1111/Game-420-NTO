using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DefaultNamespace;

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
        int canBuild = 0;
        
        int l =0;
        for (int i = 0; i < builds.Length; i++)
        {
            if(_base.tag == builds[i].tag)
            {

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
                            Debug.Log(builds[i].GetComponent<BuildItem>().buildItem.buildResurses[j].buildObject);
                        }
                        if (resursesCount >= builds[i].GetComponent<BuildItem>().buildItem.buildResurses[j].buildObjectCount) canBuild++;
                        

                    }
                }


            }
        }
        if (canBuild == builds[l].GetComponent<BuildItem>().buildItem.buildResurses.Count)
        {
            _base.SetActive(false);
            buildings[l].gameObject.SetActive(true);
        }
    }
}
