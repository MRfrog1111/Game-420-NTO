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
    public TextMeshProUGUI[] shopText; // 0 -мяак
    private ShopStruct shop;
    void Start()
    {
        shop = new ShopStruct()
        {
            name = "shop_norm",
            resources = new ShopResources()
            {
                quantum_beacon_of_return = 1,
                bee_plush = 1,
                bug_plush = 1,
                atmospheric_filter = 1,
                protective_dome = 1
                
            }
        };
        StartCoroutine(shopReq.UpdateShopResources(shop.resources));
       // StartCoroutine(playerReq.GetPlayerResources(GetPLayerRes)); 
        StartCoroutine(shopReq.GetShop(GetShopRes));
    }

    public void BuyItem(string shopItemName)
    {
        stats.CheckUpdates();
        switch (shopItemName)
        {
            case "quantum_beacon_of_return":
               // print(shop.resources.quantum_beacon_of_return);
                if (shop.resources.quantum_beacon_of_return == 1 && stats.resources.honey_esence >= 15)
                {
                    //print("you can buy it");
                    shop.resources.quantum_beacon_of_return = 0;
                    stats.resources.quantum_beacon_of_return = 1;
                    stats.resources.honey_esence -= 15;
                    stats.UpdateRes();
                    ShopChangesLogs sc = new ShopChangesLogs()
                    {
                        quantum_beacon_of_return_change = "-1"
                    };
                    StartCoroutine(shopReq.SendLog("player bought quantum beacon of return",sc));
                    shopText[0].text = "Продать";
                }
                else if (stats.resources.quantum_beacon_of_return == 1)
                {
                    //print("here");
                    shop.resources.quantum_beacon_of_return = 1;
                    stats.resources.quantum_beacon_of_return = 0;
                    stats.resources.honey_esence += 15;
                    stats.UpdateRes();
                    ShopChangesLogs sc = new ShopChangesLogs()
                    {
                        quantum_beacon_of_return_change = "+1"
                    };
                    PlayerChangesLogs pc = new PlayerChangesLogs()
                    {
                        honey_esence_change = "+15"
                    };
                    StartCoroutine(shopReq.SendLog("player sold quantum beacon of return",sc));
                    StartCoroutine(stats.webAsker.SendLog("player sold quantum beacon of return",pc));
                    shopText[0].text = "Купить";
                }
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
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        
    }
}
