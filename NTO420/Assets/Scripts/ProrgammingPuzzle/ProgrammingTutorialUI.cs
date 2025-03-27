using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgrammingTutorialUI : MonoBehaviour
{
    private int tutroNum = 0;
    public GameObject[] ui;

    void Start()
    {
        int num = PlayerPrefs.GetInt("RobotMap");
        if (num == 0)
        {
            ui[0].SetActive(true);
            ui[2].SetActive(false);
        }
        
    }
    public void Next()
    {
        ui[tutroNum].SetActive(false);
        tutroNum++;
        ui[tutroNum].SetActive(true);
    }
}
