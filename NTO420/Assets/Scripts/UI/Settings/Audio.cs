using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class Audio : MonoBehaviour
{
    public string volumeParameter = "MasterVolume";
    public AudioMixer mixer;
    public Slider slider;

    private const float multiplayer = 20f;
    private float volumeValue;

    private void Awake()
    {
        slider.onValueChanged.AddListener(SliderValue);
    }

    private void Start()
    {
        volumeValue = PlayerPrefs.GetFloat(volumeParameter, Mathf.Log10(slider.value) * multiplayer);
        slider.value = Mathf.Pow(10f, volumeValue / multiplayer);
    }

    private void SliderValue(float value)
    {
        volumeValue = Mathf.Log10(value) * multiplayer;
        mixer.SetFloat(volumeParameter, volumeValue);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(volumeParameter, volumeValue);
    }


}
