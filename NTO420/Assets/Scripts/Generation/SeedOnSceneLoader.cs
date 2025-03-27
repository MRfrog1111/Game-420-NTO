using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SeedOnSceneLoader : MonoBehaviour
{
    public HeightMapSettings ns;
    public TMP_Text tmp;
    


    // Start is called before the first frame update
    void Start()
    {
        tmp.text = "Seed: " + ns.noiseSettings.seed;
    }

}
