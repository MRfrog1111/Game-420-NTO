using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMap : MonoBehaviour
{
    public GameObject[] maps;

    private int currentMap;
    // Start is called before the first frame update
    void Awake()
    {
        currentMap = PlayerPrefs.GetInt("RobotMap");
        maps[currentMap].SetActive(true);
    }
}
