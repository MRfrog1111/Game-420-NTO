using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType {Food, Weapon}
public class ItemScriptableObject : ScriptableObject
{
    public ItemType itemType;
    public GameObject itemPrefab;
    public Sprite icon;
    public string itemName;
    public string itemDescription;
    public int maxCount;
    
}
