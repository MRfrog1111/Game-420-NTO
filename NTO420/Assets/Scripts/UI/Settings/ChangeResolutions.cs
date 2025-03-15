using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Progress;

public class ChangeResolutions : MonoBehaviour
{
    private Resolution[] resolutions;
    private List<string> options = new List<string>();

    private int currentResolutionIndex = 0;
    private int _resolution = 0;

    public TMP_Text _resolutions;

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
        _resolutions.text = options[_resolution].ToString();
        Resolution resolution = resolutions[_resolution];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);


    }
    public void SetResolutionPlus()
    {

        if (_resolution++ < resolutions.Length - 1)
            _resolution++;
        else
            _resolution = 0;
    }
    public void SetResolutionMines()
    {

        if (_resolution-- > 0)
            _resolution--;
        else
            _resolution = resolutions.Length - 1;
    }
}
