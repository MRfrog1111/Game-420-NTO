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

    private void Awake()
    {
        _icon = transform.GetChild(0).gameObject;
        itemCountText = transform.GetChild(1).GetComponent<TMP_Text>();
    }

    public void SetIcon(Sprite icon)
    {
        _icon.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        _icon.GetComponent<Image>().sprite = icon;
    }
}
