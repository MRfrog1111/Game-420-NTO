using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class MenuManager : MonoBehaviour
{
    static float soundVolume;
    static float sensetivityVolume;
    static bool _Start = false;

    Resolution[] resolutions;
    List<string> options = new List<string>();

    public GameObject Settings;
    public GameObject MeinMenu;
    public Slider volume;
    public Slider sensetivity;
    public MouseLook mouseLook;
    public TMP_Text _resolutions;

    int currentResolutionIndex = 0;

    private void Awake()
    {
        Settings.SetActive(false);
        if (_Start)
        {
            AudioListener.volume = soundVolume;
            volume.value = soundVolume;

            mouseLook.rotationSpeed = sensetivityVolume;
            sensetivity.value = sensetivityVolume;
        }
    }

    private void Start()
    {
        resolutions = Screen.resolutions;
        

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height + " " + resolutions[i].refreshRateRatio + "Hz";
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
    }

    private void Update()
    {
        _resolutions.text = options[currentResolutionIndex].ToString();
        Resolution resolution = resolutions[currentResolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetResolution()
    {
        
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
        _Start = true;
    }

    public void Sensetivity()
    {
        mouseLook.rotationSpeed *= sensetivity.value;
        sensetivityVolume = sensetivity.value;
        _Start = true;
    }

    public void PlusIndex()
    {
        if(currentResolutionIndex++ < resolutions.Length)
            currentResolutionIndex++;
        else 
            currentResolutionIndex = 0;
    }
    public void MinesIndex()
    {
        if(currentResolutionIndex-- > 0)
            currentResolutionIndex--;
        else
            currentResolutionIndex = resolutions.Length;
    }
}
