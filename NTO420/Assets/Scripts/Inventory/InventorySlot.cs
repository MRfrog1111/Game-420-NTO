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
    public GameObject icon;
    public TMP_Text itemCountText;

    private void Start()
    {
        icon = transform.GetChild(0).gameObject;
        itemCountText = transform.GetChild(1).GetComponent<TMP_Text>();
    }

    public void SetIcon(Sprite _icon)
    {
        icon.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        icon.GetComponent<Image>().sprite = _icon;
    }
}
