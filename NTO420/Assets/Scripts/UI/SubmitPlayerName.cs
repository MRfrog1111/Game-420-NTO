using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DefaultNamespace;
using TMPro;
//using TMPro.EditorUtilities;
using UnityEditor;
using UnityEngine.SceneManagement;
public class SubmitPlayerName : MonoBehaviour
{
    [SerializeField] private PlayerRequests webAsker;
    //[SerializeField] private TextMeshProUGUI textUI;
    [SerializeField] private GameObject inputField;
    private PlayersStruct players;
    private string inputName;
    private bool canCreate = true;
    public void GetPlrs(PlayersStruct ps)
    {
        players = ps;
    }
    
    private void Awake()
    {
        StartCoroutine(webAsker.GetPlayers(GetPlrs));
    }

    public void SubmitName()
    {
        inputName = inputField.GetComponent<TMP_InputField>().text;
        for (int i = 0; i < players.players.Length; i++)
        {
            if (inputName == players.players[i].name)
            {
                canCreate = false;
                break;
            }
        }

        if (canCreate)
        {
            print("yes");
            StartCoroutine(webAsker.AddPlayer(inputName));
        }
        PlayerPrefs.SetString("PlayerName",inputName);
        SceneManager.LoadScene("StartLocation");
    }

}
