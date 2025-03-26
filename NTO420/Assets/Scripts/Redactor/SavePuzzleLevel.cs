using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SavePuzzleLevel : MonoBehaviour
{
    [SerializeField] private Savedobjects savedObjectsScript;
    [SerializeField] private string levelName;
    [SerializeField] private GridObjectsPlacement[] placements;
    public void Savelevel()
    {
        File.Delete(Application.dataPath + "/TestData.json");
        string saveFilePath = Application.dataPath + "/TestData.json";
        string savePlayerData = JsonUtility.ToJson(savedObjectsScript);
        File.WriteAllText(saveFilePath, savePlayerData);
        print(saveFilePath);
    }

    public void Loadlevel()
    {
        string saveFilePath = Application.dataPath + "/TestData.json";
        string loadPlayerData = File.ReadAllText(saveFilePath);
        gameObject.GetComponent<ClearAll>().DeleteAll();
        foreach (var obj in JsonUtility.FromJson<SaveableGridObjects>(loadPlayerData).gridObjects)
        {
            savedObjectsScript.savedObjects.gridObjects.Add(obj);
        }
        print(savedObjectsScript.savedObjects.gridObjects.Count);
        //placements[0].BlockPlacing(new Vector3(savedObjectsScript.savedObjects.gridObjects[0].xPos,savedObjectsScript.savedObjects.gridObjects[0].yPos,savedObjectsScript.savedObjects.gridObjects[0].zPos),savedObjectsScript.savedObjects.gridObjects[0].ID);
       for(int i = 0; i < savedObjectsScript.savedObjects.gridObjects.Count; i++)
        {
            print(savedObjectsScript.savedObjects.gridObjects[i].ID);
            if (savedObjectsScript.savedObjects.gridObjects[i].ID < 4 || savedObjectsScript.savedObjects.gridObjects[i].ID>5) // все кроме стен
            {
                placements[0].BlockPlacing(new Vector3(savedObjectsScript.savedObjects.gridObjects[i].xPos,savedObjectsScript.savedObjects.gridObjects[i].yPos,savedObjectsScript.savedObjects.gridObjects[i].zPos),savedObjectsScript.savedObjects.gridObjects[i].ID,false);
            }
            else if (savedObjectsScript.savedObjects.gridObjects[i].ID == 4)// горизонтальные стены
            {
                placements[1].BlockPlacing(new Vector3(savedObjectsScript.savedObjects.gridObjects[i].xPos,savedObjectsScript.savedObjects.gridObjects[i].yPos,savedObjectsScript.savedObjects.gridObjects[i].zPos),savedObjectsScript.savedObjects.gridObjects[i].ID,false);
            }
            else if (savedObjectsScript.savedObjects.gridObjects[i].ID == 5)// вертикальные стены
            {
                placements[2].BlockPlacing(new Vector3(savedObjectsScript.savedObjects.gridObjects[i].xPos,savedObjectsScript.savedObjects.gridObjects[i].yPos,savedObjectsScript.savedObjects.gridObjects[i].zPos),savedObjectsScript.savedObjects.gridObjects[i].ID,false);
            }
        }
    }
}


