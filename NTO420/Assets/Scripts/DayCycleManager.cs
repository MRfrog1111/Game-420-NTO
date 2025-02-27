using System;
using UnityEngine;


public class DayCycleManager : MonoBehaviour
{
    [Range(0, 1)]
    public float TimeOfDay;
    public float DayDuration = 30f;

    public AnimationCurve SunCurve;
    public AnimationCurve MoonCurve;
    public AnimationCurve SkyboxCurve;

    public Material DaySkybox;
    public Material NightSkybox;

    public ParticleSystem Stars;

    public Light Sun;
    public Light Moon;

    private float sunIntensity;
    private float moonIntensity;

    public static event Action NightBegun, DayBegun;
    private bool isInvokedNight = false;
    private void Start()
    {
        sunIntensity = Sun.intensity;
        moonIntensity = Moon.intensity;
    }

    private void Update()
    {
        TimeOfDay += Time.deltaTime / DayDuration;
        if (TimeOfDay >= 1)
        {
            TimeOfDay -= 1;
            isInvokedNight = false;
            DayBegun?.Invoke();
        }

        if (TimeOfDay >= 0.5f && !isInvokedNight)
        {
            NightBegun?.Invoke();
            isInvokedNight = true;
        }


        RenderSettings.skybox.Lerp(NightSkybox, DaySkybox, SkyboxCurve.Evaluate(TimeOfDay));
        RenderSettings.sun = SkyboxCurve.Evaluate(TimeOfDay) > 0.1f ? Sun : Moon;
        DynamicGI.UpdateEnvironment();

        var mainModule = Stars.main;
        //mainModule.startColor = new Color(1, 1, 1, 1 - SkyboxCurve.Evaluate(TimeOfDay));


        Sun.transform.localRotation = Quaternion.Euler(TimeOfDay * 360f, 180, 0);
        Moon.transform.localRotation = Quaternion.Euler(TimeOfDay * 360f + 180f, 180, 0);
        
        Sun.intensity = sunIntensity * SunCurve.Evaluate(TimeOfDay);
        Moon.intensity = moonIntensity * MoonCurve.Evaluate(TimeOfDay);
    }
}