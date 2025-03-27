
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class LoadSeed : MonoBehaviour
{

    public HeightMapSettings ns;
    public InputField InputField;

    public void RandomSeed()
    {
        ns.noiseSettings.seed = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
        Load();
    }

    public void SpecialSeed()
    {
        if(Convert.ToInt32(InputField.text) > int.MaxValue && Convert.ToInt32(InputField.text) < int.MinValue)
            return;
        ns.noiseSettings.seed = Convert.ToInt32(InputField.text);
        Load();
    }

    public void Load()
    {
        SceneManager.LoadScene("Terrain");
    }
}
