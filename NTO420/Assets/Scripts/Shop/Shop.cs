using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DefaultNamespace;
using DefaultNamespace;
public class Shop : MonoBehaviour
{
    [SerializeField] private ShopRequests shopReq;
    [SerializeField] private PlayerRequests playerReq;
    private PlayerResources resources;
    private ShopStruct shop;
    void Awake()
    {
        resources = new PlayerResources();
        shop = new ShopStruct();
        StartCoroutine(playerReq.GetPlayerResources(GetPLayerRes)); 
        StartCoroutine(shopReq.GetShop(GetShopRes));
    }

    public void BuyItem(string shopItemName)
    {
        StartCoroutine(shopReq.GetShop(GetShopRes));
        switch (shopItemName)
        {
            case "quantum_beacon_of_return":
               // print(shop.resources.quantum_beacon_of_return);
                if (shop.resources.quantum_beacon_of_return == 1)
                {
                    print("you can buy it");
                    shop.resources.quantum_beacon_of_return = 0;
                    print(shop.resources.quantum_beacon_of_return);
                }
                break;

        }
        StartCoroutine(shopReq.UpdateShopResources(shop.resources));
        //StartCoroutine(shopReq.GetShop(GetShopRes));
        //print(shopItemName);
    }
    
    public void GetPLayerRes(PlayerResources res)
    {
        resources = res;
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
