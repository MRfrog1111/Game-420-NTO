using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DefaultNamespace;
using UnityEngine.SceneManagement;
//using UnityEditor.iOS.Xcode;
using UnityEngine.UI;


public class Craft : MonoBehaviour
{
   // [SerializeField] private Build build;

     private PlayerStats stats;
     private GameObject player;
    public GameObject[] buildings;
     private Tutorial tutor;
    public GameObject[] bases;
    private int minus;

    void Start()
    {
        print("updated");
        player = GameObject.Find("PlayerCompleteEdition");
        tutor = GameObject.FindObjectOfType<Tutorial>();
        stats = player.GetComponent<PlayerStats>();
        print(player.name);
        print(tutor.name);
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
    public void FirstUpdate()
    {
        print("updated");
        player = GameObject.Find("PlayerCompleteEdition");
        tutor = GameObject.FindObjectOfType<Tutorial>();
        stats = player.GetComponent<PlayerStats>();
        print(player.name);
        print(tutor.name);
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
        //print("step2");
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
                        print(FindObjectsOfType<CollectResource>()[0].name + " " + FindObjectsOfType<CollectResource>()[0].transform.parent.name);
                        int resursesCount = 0;
                        if (slot.isEmpty) continue;
                        print("test4" + slot.item.name+ " "+builds[i].GetComponent<BuildItem>().buildItem.buildResurses[j].buildObject.name);
                        if (slot.item == builds[i].GetComponent<BuildItem>().buildItem.buildResurses[j].buildObject)
                        {
                            print("slot test "+slot.item.name);
                            resursesCount += slot.count;
                            l = i;
                            
                        }
                        if (resursesCount >= builds[i].GetComponent<BuildItem>().buildItem.buildResurses[j].buildObjectCount) canBuild++;
                    }
                }
            }
        }
        print("canBuild "+canBuild+" "+builds[l].GetComponent<BuildItem>().buildItem.buildResurses.Count);
        if (canBuild >= builds[l].GetComponent<BuildItem>().buildItem.buildResurses.Count)
        {
            _base.SetActive(false);
            buildings[l].gameObject.SetActive(true);
            print("tulen");
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
                           // slot._icon.GetComponent<Image>().color = new Color(1, 1, 1, 0);
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
                        GoToPuzzle(1);
                        break;
                    case "Honey":
                        stats.resources.apiary_module = 1; 
                        GoToPuzzle(2);
                        break;
                    default:
                        break;
                }
                stats.UpdateRes();
            }
            tutor.CheckStage();
        }
    }

    void GoToPuzzle(int puzzleNum)
    {
        print("tp");
      player.GetComponent<CharacterEnabler>().GotoPuzzle(puzzleNum);
    }
}



