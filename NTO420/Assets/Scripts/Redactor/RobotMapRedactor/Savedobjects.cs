using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Savedobjects : MonoBehaviour
{
    public SaveableGridObjects savedObjects;
}

[System.Serializable]
public class SaveableGridObject
{
    public int ID;
   public float xPos;
    public float yPos;
    public float zPos;
}

[System.Serializable]
public class SaveableGridObjects
{
    public List<SaveableGridObject> gridObjects=new List<SaveableGridObject>();

    /*public SaveableGridObjects()
    {
        gridObjects = new List<SaveableGridObject>();
    }*/
}