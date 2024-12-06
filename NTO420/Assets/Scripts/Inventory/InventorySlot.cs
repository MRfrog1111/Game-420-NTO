using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotInventory : MonoBehaviour
{
    public ItemScriptableObject item;
    public int count;
    public bool isEmpty = true;
    public GameObject _icon;
    public TMP_Text itemCountText;
    public PlayerStats stats;
    
    void ChangeText(){
        if (item != null)
        {
            switch (item.name)
            {
                case "Honey":
                    count = stats.resources.honey;
                    break;
                case "Minerals":
                    count = stats.resources.minerals;
                    break;
                case "Wax":
                    count = stats.resources.wax;
                    break;
                case "SiliconSand":
                    count = stats.resources.silicon_sand;
                    break;
                default:
                    break;
            }

            itemCountText.text = count.ToString();
        }
    }
    private void OnEnable()
    {
        PlayerStats.ChangeInventory += ChangeText;

    }

    private void OnDisable() {
        PlayerStats.ChangeInventory -= ChangeText;
    }
    private void Awake()
    {
        _icon = transform.GetChild(0).GetChild(0).gameObject;
        itemCountText = transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>();
    }
    

    public void SetIcon(Sprite icon)
    {
        _icon.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        _icon.GetComponent<Image>().sprite = icon;
    }
}
