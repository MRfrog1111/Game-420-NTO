using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ConnectWithShop : MonoBehaviour
{
   public List<TextMeshProUGUI> buttons;
   public void Buy(string itemName)
   {
      Shop shop = GameObject.FindObjectOfType<Shop>();
      shop.BuyItem(itemName);
   }
}
