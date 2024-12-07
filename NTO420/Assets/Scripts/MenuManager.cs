using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    
    public GameObject Settings;
    public GameObject MeinMenu;

    private void Awake()
    {
        Settings.SetActive(false);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ShowSettings()
    {
        Settings.SetActive(true);
        MeinMenu.SetActive(false);
    }
    public void CloseSettings()
    {
        Settings.SetActive(false);
        MeinMenu.SetActive(true);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
