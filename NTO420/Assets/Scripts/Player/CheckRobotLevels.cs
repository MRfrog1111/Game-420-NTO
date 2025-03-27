using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CheckRobotLevels : MonoBehaviour
{
    [SerializeField] private GameObject[] textObjs;
    [SerializeField] private Sprite[] defaultSprites;
    //private Tutorial tutor;
    public List<string> robotLevels;
    public void FindRobotLevels()
    {
        string [] fileEntries = Directory.GetFiles(Application.dataPath + "/", "*.json");
        foreach (var file in fileEntries)
        {
            robotLevels.Add(file);
        }
    }
    void Start()
    {
        FindRobotLevels();
        for(int i = 0; i < textObjs.Length; i++)
        {
            if (textObjs[i].GetComponent<SpriteRenderer>().sprite == null)
            {
                textObjs[i].GetComponent<SpriteRenderer>().sprite = defaultSprites[i];
            }
        }
        //GameObject.Find("PlayerCapsule").transform.localPosition = new Vector3(0, 0, 0);
    }
}
