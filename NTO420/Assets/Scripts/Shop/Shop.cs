using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DefaultNamespace;
using DefaultNamespace;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private ShopRequests shopReq;
    [SerializeField] private PlayerStats stats;
    public TextMeshProUGUI[] shopText;
    public GameObject[] decoratives;
    private ShopStruct shop;
    void Start()
    {
        shop = new ShopStruct()
        {
            name = "shop1",
            resources = new ShopResources()
            {
                flower = 1,
                atmospheric_filter = 1,
                bear_figure = 1,
                bee_plush = 1,
                bug_plush = 1
                
            }
        };
        StartCoroutine(shopReq.UpdateShopResources(shop.resources));
       // StartCoroutine(playerReq.GetPlayerResources(GetPLayerRes)); 
        StartCoroutine(shopReq.GetShop(GetShopRes));
    }
    
    public void FirstUpdate()
    {
        if (stats.resources.atmospheric_filter == 1)
        {
            shopText[0].text = "Продать";
        }
        if (stats.resources.bug_plush == 1)
        {
            decoratives[0].SetActive(true);
            shopText[1].text = "Продать";
        }
        if (stats.resources.bee_plush == 1)
        {
            decoratives[1].SetActive(true);
            shopText[2].text = "Продать";
        }
        if (stats.resources.bear_figure == 1)
        {
            decoratives[2].SetActive(true);
            shopText[3].text = "Продать";
        }
        if (stats.resources.flower == 1)
        {
            decoratives[3].SetActive(true);
            shopText[4].text = "Продать";
        }
    }
    
    private void OnEnable()
    {
        PlayerStats.onResourcesChange += FirstUpdate;
    }

    private void OnDisable() {
        PlayerStats.onResourcesChange -= FirstUpdate;
    }

    public void BuyItem(string shopItemName)
    {
        stats.CheckUpdates();
        switch (shopItemName)
        {
            case "atmosferic_filter":
               // print(shop.resources.quantum_beacon_of_return);
                if (shop.resources.atmospheric_filter == 1 && stats.resources.honey_esence >= 15)
                {
                    //print("you can buy it");
                    shop.resources.atmospheric_filter = 0;
                    stats.resources.atmospheric_filter = 1;
                    stats.resources.oxygen = 100;
                    stats.resources.honey_esence -= 15;
                    stats.UpdateRes();
                    ShopChangesLogs sc = new ShopChangesLogs()
                    {
                        atmospheric_filter_change = "-1"
                    };
                    StartCoroutine(shopReq.SendLog("player bought atmospheric filter",sc));
                    shopText[0].text = "Продать";
                }
                else if (stats.resources.atmospheric_filter == 1)
                {
                    //print("here");
                    shop.resources.atmospheric_filter = 1;
                    stats.resources.atmospheric_filter = 0;
                    stats.resources.honey_esence += 15;
                    stats.UpdateRes();
                    ShopChangesLogs sc = new ShopChangesLogs()
                    {
                        atmospheric_filter_change = "+1"
                    };
                    PlayerChangesLogs pc = new PlayerChangesLogs()
                    {
                        honey_esence_change = "+15"
                    };
                    StartCoroutine(shopReq.SendLog("player sold atmospheric filter",sc));
                    StartCoroutine(stats.webAsker.SendLog("player sold atmospheric filter",pc));
                    shopText[0].text = "Купить";
                }
                break;
            case "bug_plush":
                if (shop.resources.bug_plush == 1 && stats.resources.honey_esence >= 5)
                {
                    //print("you can buy it");
                    shop.resources.bug_plush = 0;
                    stats.resources.bug_plush = 1;
                    stats.resources.honey_esence -= 5;
                    stats.UpdateRes();
                    ShopChangesLogs sc = new ShopChangesLogs()
                    {
                        bug_plush_change = "-1"
                    };
                    StartCoroutine(shopReq.SendLog("player bought bug_plush",sc));
                    decoratives[0].SetActive(true);
                    shopText[1].text = "Продать";
                }
                else if (stats.resources.bug_plush == 1)
                {
                    //print("here");
                    shop.resources.bug_plush = 1;
                    stats.resources.bug_plush = 0;
                    stats.resources.honey_esence += 5;
                    stats.UpdateRes();
                    ShopChangesLogs sc = new ShopChangesLogs()
                    {
                        bug_plush_change = "+1"
                    };
                    PlayerChangesLogs pc = new PlayerChangesLogs()
                    {
                        honey_esence_change = "+5"
                    };
                    StartCoroutine(shopReq.SendLog("player sold bug plush",sc));
                    StartCoroutine(stats.webAsker.SendLog("player sold bug plush",pc));
                    decoratives[0].SetActive(false);
                    shopText[1].text = "Купить";
                } 
                break;
            case "bee_plush":
                if (shop.resources.bee_plush == 1 && stats.resources.honey_esence >= 5)
                {
                    //print("you can buy it");
                    shop.resources.bee_plush = 0;
                    stats.resources.bee_plush = 1;
                    stats.resources.honey_esence -= 5;
                    stats.UpdateRes();
                    ShopChangesLogs sc = new ShopChangesLogs()
                    {
                        bee_plush_change = "-1"
                    };
                    StartCoroutine(shopReq.SendLog("player bought bee_plush",sc));
                    decoratives[1].SetActive(true);
                    shopText[2].text = "Продать";
                }
                else if (stats.resources.bee_plush == 1)
                {
                    //print("here");
                    shop.resources.bee_plush = 1;
                    stats.resources.bee_plush = 0;
                    stats.resources.honey_esence += 5;
                    stats.UpdateRes();
                    ShopChangesLogs sc = new ShopChangesLogs()
                    {
                        bee_plush_change = "+1"
                    };
                    PlayerChangesLogs pc = new PlayerChangesLogs()
                    {
                        honey_esence_change = "+5"
                    };
                    StartCoroutine(shopReq.SendLog("player sold bee plush",sc));
                    StartCoroutine(stats.webAsker.SendLog("player sold bee plush",pc));
                    decoratives[1].SetActive(false);
                    shopText[2].text = "Купить";
                }
                
                break;
            case "bear_figure":
                if (shop.resources.bear_figure == 1 && stats.resources.honey_esence >= 5)
                {
                    //print("you can buy it");
                    shop.resources.bear_figure = 0;
                    stats.resources.bear_figure = 1;
                    stats.resources.honey_esence -= 5;
                    stats.UpdateRes();
                    ShopChangesLogs sc = new ShopChangesLogs()
                    {
                        bear_figure_change = "-1"
                    };
                    StartCoroutine(shopReq.SendLog("player bought bear figure",sc));
                    decoratives[2].SetActive(true);
                    shopText[3].text = "Продать";
                }
                else if (stats.resources.bear_figure == 1)
                {
                    //print("here");
                    shop.resources.bear_figure = 1;
                    stats.resources.bear_figure = 0;
                    stats.resources.honey_esence += 5;
                    stats.UpdateRes();
                    ShopChangesLogs sc = new ShopChangesLogs()
                    {
                        bear_figure_change = "+1"
                    };
                    PlayerChangesLogs pc = new PlayerChangesLogs()
                    {
                        honey_esence_change = "+5"
                    };
                    StartCoroutine(shopReq.SendLog("player sold bear figure",sc));
                    StartCoroutine(stats.webAsker.SendLog("player sold bear figure",pc));
                    decoratives[2].SetActive(false);
                    shopText[3].text = "Купить";
                }
                
                break;
            case "flower":
                if (shop.resources.flower == 1 && stats.resources.honey_esence >= 5)
                {
                    //print("you can buy it");
                    shop.resources.flower = 0;
                    stats.resources.flower = 1;
                    stats.resources.honey_esence -= 5;
                    stats.UpdateRes();
                    ShopChangesLogs sc = new ShopChangesLogs()
                    {
                        flower_change = "-1"
                    };
                    StartCoroutine(shopReq.SendLog("player bought flower",sc));
                    decoratives[3].SetActive(true);
                    shopText[4].text = "Продать";
                }
                else if (stats.resources.flower == 1)
                {
                    //print("here");
                    shop.resources.flower = 1;
                    stats.resources.flower = 0;
                    stats.resources.honey_esence += 5;
                    stats.UpdateRes();
                    ShopChangesLogs sc = new ShopChangesLogs()
                    {
                        flower_change = "+1"
                    };
                    PlayerChangesLogs pc = new PlayerChangesLogs()
                    {
                        honey_esence_change = "+5"
                    };
                    StartCoroutine(shopReq.SendLog("player sold flower", sc));
                    StartCoroutine(stats.webAsker.SendLog("player sold flower", pc));
                    decoratives[3].SetActive(false);
                    shopText[4].text = "Купить";
                }

                break;
            default:
                break;

        }
        
        StartCoroutine(shopReq.UpdateShopResources(shop.resources));
        StartCoroutine(shopReq.GetShop(GetShopRes));
        //print(shopItemName);
    }
    public void GetShopRes(ShopStruct s)
    {
        shop = s;
        //print("test " + s.resources.quantum_beacon_of_return);
    }

    private void spawnPlush(GameObject plush)
    {
        plush.SetActive(true);
    }
}
