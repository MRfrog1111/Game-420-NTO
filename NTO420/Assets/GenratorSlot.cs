using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenratorSlot : MonoBehaviour
{
    public SlotInventory oldSlot;
    public SlotInventory slot;

    private void Update()
    {
        if (oldSlot.item.itemType == ItemType.Honey)
        {
            slot = oldSlot;
        }
    }
}
