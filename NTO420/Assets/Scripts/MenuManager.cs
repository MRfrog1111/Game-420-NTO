using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuManager : MonoBehaviour
{
    static float soundVolume;
    static float sensetivityVolume;
    static bool Start = false;

    public GameObject Settings;
    public GameObject MeinMenu;
    public Slider volume;
    public Slider sensetivity;
    public MouseLook mouseLook;

    private void Awake()
    {
        Settings.SetActive(false);
        if (Start)
        {
            AudioListener.volume = soundVolume;
            volume.value = soundVolume;

            mouseLook.rotationSpeed = sensetivityVolume;
            sensetivity.value = sensetivityVolume;
        }
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
    public void Volume()
    {
        AudioListener.volume = volume.value;
        soundVolume = volume.value;
        Start = true;
    }

    public void Sensetivity()
    {
        mouseLook.rotationSpeed *= sensetivity.value;
        sensetivityVolume = sensetivity.value;
    }
}
