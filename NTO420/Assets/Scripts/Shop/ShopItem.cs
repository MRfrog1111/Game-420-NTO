using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    public Shop shop;
    public void Buy(string shopItemName)
    {
        shop.BuyItem(shopItemName);
    }
}
