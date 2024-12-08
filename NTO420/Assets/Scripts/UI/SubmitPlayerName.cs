using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DefaultNamespace;
using TMPro;

using UnityEditor;
using UnityEngine.SceneManagement;
public class SubmitPlayerName : MonoBehaviour
{
    [SerializeField] private PlayerRequests webAsker;
    [SerializeField] private ShopRequests shopReq;
    //[SerializeField] private TextMeshProUGUI textUI;
    [SerializeField] private GameObject inputField;
    private PlayersStruct players;
    private string inputName;
    private bool canCreate = true;
    private bool isConnected = false;
    public void GetPlrs(PlayersStruct ps)
    {
        players = ps;
        isConnected = true;
    }
    
    private void Awake()
    {
        StartCoroutine(webAsker.GetPlayers(GetPlrs));
    }

    public void SubmitName()
    {
        inputName = inputField.GetComponent<TMP_InputField>().text;
        if (isConnected)
        {
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
            PlayerPrefs.SetInt("Connection", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Connection", 0);
        }

        PlayerPrefs.SetString("PlayerName",inputName);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
