using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeInit : MonoBehaviour
{
    public string volumeParametr = "MasterVolume";
    public AudioMixer mixer;


    // Start is called before the first frame update
    void Start()
    {
        var volumeValue = PlayerPrefs.GetFloat(volumeParametr, volumeParametr == "MusicVol" ? 0f : - 80f);
        mixer.SetFloat(volumeParametr, volumeValue);
    }

}
