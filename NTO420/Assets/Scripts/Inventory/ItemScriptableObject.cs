using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType {Honey, Weapon, BuildingMaterials, Seeds, Electricals, Minerals, Upgrade, Repair }
public class ItemScriptableObject : ScriptableObject
{
    public ItemType itemType;
    public GameObject itemPrefab;
    public Sprite icon;
    public string itemName;
    public string itemDescription;
    public int maxCount;
    
}
