using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingScriptableObject : ScriptableObject
{
    public List<BuildResurses> buildResurses;                                                
}

[System.Serializable]
public class BuildResurses : MonoBehaviour
{
    public ItemScriptableObject buildObject;
    public int buildObjectCount;
}
