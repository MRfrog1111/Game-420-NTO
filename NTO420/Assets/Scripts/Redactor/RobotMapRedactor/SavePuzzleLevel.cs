using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using SFB;
using TMPro;
using UnityEngine.Networking;

public class SavePuzzleLevel : MonoBehaviour
{
    [SerializeField] private Savedobjects savedObjectsScript;
    [SerializeField] private string levelName;
    [SerializeField] private GridObjectsPlacement[] placements;
    [SerializeField] private TMP_InputField inputField;

    public void LevelLoading(string saveFilePath)
    {
        gameObject.GetComponent<ClearAll>().DeleteAll();
        //string [] fileEntries = Directory.GetFiles(Application.dataPath + "/", "*.json");
        string loadPlayerData = File.ReadAllText(saveFilePath); //тут все работает
        print(loadPlayerData);
        //print("load"+saveFilePath);
        savedObjectsScript.savedObjects.gridObjects.Clear(); //тут ломается
        //print("childCountAtStart" + savedObjectsScript.savedObjects.gridObjects.Count);
        SaveableGridObjects gridObj = JsonUtility.FromJson<SaveableGridObjects>(loadPlayerData);
        //print("count"+JsonUtility.FromJson<SaveableGridObjects>(loadPlayerData).gridObjects.Count);
        //savedObjectsScript.savedObjects.gridObjects.Add(JsonUtility.FromJson<SaveableGridObjects>(loadPlayerData).gridObjects[0]);
        for (int i = 0; i < gridObj.gridObjects.Count; i++)
        {
            print("id" + gridObj.gridObjects[i].ID);
            savedObjectsScript.savedObjects.gridObjects.Add(gridObj.gridObjects[i]);
            /* SaveableGridObject test = new SaveableGridObject(){
                 ID = 0,
                 xPos = 0,
                 yPos = 0,
                 zPos = 0,
             };
             savedObjectsScript.savedObjects.gridObjects.Add(test);*/
        }

        print("childCount" + savedObjectsScript.savedObjects.gridObjects.Count);
        //placements[0].BlockPlacing(new Vector3(savedObjectsScript.savedObjects.gridObjects[0].xPos,savedObjectsScript.savedObjects.gridObjects[0].yPos,savedObjectsScript.savedObjects.gridObjects[0].zPos),savedObjectsScript.savedObjects.gridObjects[0].ID);
        for (int i = 0; i < savedObjectsScript.savedObjects.gridObjects.Count; i++)
        {
            print(savedObjectsScript.savedObjects.gridObjects[i].ID);
            if (savedObjectsScript.savedObjects.gridObjects[i].ID < 4 ||
                savedObjectsScript.savedObjects.gridObjects[i].ID > 5) // все кроме стен
            {
                placements[0].BlockPlacing(
                    new Vector3(savedObjectsScript.savedObjects.gridObjects[i].xPos,
                        savedObjectsScript.savedObjects.gridObjects[i].yPos,
                        savedObjectsScript.savedObjects.gridObjects[i].zPos),
                    savedObjectsScript.savedObjects.gridObjects[i].ID, false);
            }
            else if (savedObjectsScript.savedObjects.gridObjects[i].ID == 4) // горизонтальные стены
            {
                placements[1].BlockPlacing(
                    new Vector3(savedObjectsScript.savedObjects.gridObjects[i].xPos,
                        savedObjectsScript.savedObjects.gridObjects[i].yPos,
                        savedObjectsScript.savedObjects.gridObjects[i].zPos),
                    savedObjectsScript.savedObjects.gridObjects[i].ID, false);
            }
            else if (savedObjectsScript.savedObjects.gridObjects[i].ID == 5) // вертикальные стены
            {
                placements[2].BlockPlacing(
                    new Vector3(savedObjectsScript.savedObjects.gridObjects[i].xPos,
                        savedObjectsScript.savedObjects.gridObjects[i].yPos,
                        savedObjectsScript.savedObjects.gridObjects[i].zPos),
                    savedObjectsScript.savedObjects.gridObjects[i].ID, false);
            }
        }
    }

    public void Savelevel()
    {
        string filename = inputField.text;
        //File.Delete(Application.dataPath + "/"+ filename+ ".json");
        string saveFilePath = Application.dataPath + "/" + filename + ".json";
        string savePlayerData = JsonUtility.ToJson(savedObjectsScript.savedObjects);
        print(savePlayerData);
        File.WriteAllText(saveFilePath, savePlayerData);
        // print(saveFilePath);
    }

    public void Loadlevel()
    {
        string filename = inputField.text;
        string saveFilePath = Application.dataPath + "/" + filename + ".json";
        LevelLoading(saveFilePath);

    }
}


