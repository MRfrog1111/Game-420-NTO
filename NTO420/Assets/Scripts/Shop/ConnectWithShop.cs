using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectWithShop : MonoBehaviour
{
   public void Buy(string itemName)
   {
      Shop shop = GameObject.FindObjectOfType<Shop>();
      shop.BuyItem(itemName);
   }
}
