using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectWithCraft : MonoBehaviour
{
    public void CraftBuilding(int buildingNum)
    {
        GameObject craft = GameObject.Find("AllModules");
        craft.GetComponent<Craft>().CraftBuilding(buildingNum);
        print("step1");
    }

    public void CrafHoneyEssence()
    {
        GameObject craftEssence = GameObject.FindObjectOfType<CraftHoneyEssence>().gameObject;
        craftEssence.GetComponent<CraftHoneyEssence>().CraftEssence();
    }
}
