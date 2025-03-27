using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CheckRobotLevels : MonoBehaviour
{
    public List<string> robotLevels;
    // Start is called before the first frame update
    void Start()
    {
       FindRobotLevels();
    }

    public void FindRobotLevels()
    {
        string [] fileEntries = Directory.GetFiles(Application.dataPath + "/", "*.json");
        foreach (var file in fileEntries)
        {
            robotLevels.Add(file);
        }
    }
}
