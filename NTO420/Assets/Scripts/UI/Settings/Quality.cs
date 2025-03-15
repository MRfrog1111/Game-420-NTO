using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quality : MonoBehaviour
{
    public void SetLowQuality()
    {
        QualitySettings.SetQualityLevel(0);
    }
    public void SetMediumQuality()
    {
        QualitySettings.SetQualityLevel(1);
    }
    public void SetHighQuality()
    {
        QualitySettings.SetQualityLevel(2);
    }
}
