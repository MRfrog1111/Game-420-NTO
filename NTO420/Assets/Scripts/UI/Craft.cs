using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DefaultNamespace;
using UnityEditor.iOS.Xcode;
using UnityEngine.UI;
public class Craft : MonoBehaviour
{
   // [SerializeField] private Build build;

    [SerializeField] private PlayerStats stats;
    public GameObject[] buildings;

    public GameObject[] bases;
    private int minus;
    public void FirstUpdate()
    {
        print("updated");
        if (stats.resources.living_module > 0)
        {
            buildings[0].SetActive(true);
            bases[0].SetActive(false);
        }
        if (stats.resources.apiary_module > 0)
        {
            buildings[1].SetActive(true);
            bases[1].SetActive(false);
        }
    }

    private void OnEnable()
    {
        PlayerStats.onResourcesChange += FirstUpdate;

    }

    private void OnDisable() {
        
        PlayerStats.onResourcesChange -= FirstUpdate;
    }

    public void CraftBuilding(int buildingNum)
    {
        AddBase(bases[buildingNum], buildings);
    }

    public void AddBase(GameObject _base, GameObject[] builds)
    {
        print("build1");
        int canBuild = 0;
        stats.CheckUpdates();
        int l = 0;
        for (int i = 0; i < builds.Length; i++)
        {
            if(_base.tag == builds[i].tag)
            {
                print("build2 " + i + " " +  builds[i].GetComponent<BuildItem>().buildItem.buildResurses.Count);
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
            //print("a");
            for (int j = 0; j < builds[l].GetComponent<BuildItem>().buildItem.buildResurses.Count; j++)
            {
                minus = builds[l].GetComponent<BuildItem>().buildItem.buildResurses[j].buildObjectCount;
                foreach (SlotInventory slot in FindObjectsOfType<CollectResource>()[0].slots)
                {
                    if (minus > 0 && slot.item == builds[l].GetComponent<BuildItem>().buildItem.buildResurses[j].buildObject)
                    {
                        if (minus < slot.count)
                        {
                           /* slot.count -= builds[l].GetComponent<BuildItem>().buildItem.buildResurses[j]
                                .buildObjectCount;*/
                            slot.count -= minus;
                            slot.itemCountText.text = slot.count.ToString();
                        }
                        else
                        {
                            minus -= slot.count;
                           // slot.count = 0;
                           slot.itemCountText.text = "";
                            slot._icon.GetComponent<Image>().color = new Color(1, 1, 1, 0);
                            slot._icon.GetComponent<Image>().sprite = null;
                            slot.item = null;
                            slot._icon = null;
                            slot.isEmpty = true;
                            slot._icon = null;
                        }

                        /* if(slot.count == 0)
                         {
                            
                         }*/
                    }
                    
                }
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

                switch (_base.tag)
                {
                    case "Home":
                        stats.resources.living_module = 1; 
                        break;
                    case "Honey":
                        stats.resources.apiary_module = 1; 
                        break;
                    default:
                        break;
                }
                stats.UpdateRes();
            }
        }
    }
}


