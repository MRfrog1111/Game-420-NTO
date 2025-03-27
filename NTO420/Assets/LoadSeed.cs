
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using System.Collections;

public class LoadSeed : MonoBehaviour
{

    public HeightMapSettings ns;
    public InputField InputField;

    public TMP_Text nezy;

    private void Start()
    {
        nezy.gameObject.SetActive(false);
    }
    public void RandomSeed()
    {
        ns.noiseSettings.seed = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
        Load();
    }

    public void SpecialSeed()
    {
        if(Convert.ToInt32(InputField.text) > int.MaxValue && Convert.ToInt32(InputField.text) < int.MinValue)
        {
            nezy.gameObject.SetActive(true);
            StartCoroutine(NezyOff());
            return;
        }
        ns.noiseSettings.seed = Convert.ToInt32(InputField.text);
        Load();
    }

    public void Load()
    {
        SceneManager.LoadScene("Terrain");
    }

    public IEnumerator NezyOff()
    {
        yield return new WaitForSeconds(2f);
        nezy.gameObject.SetActive(false);
    }
}
