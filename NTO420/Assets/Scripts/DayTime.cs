using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DayTime : MonoBehaviour
{
    
    [SerializeField] Gradient directionLightGradient;
    [SerializeField] Gradient dambientLightGradient;

    [SerializeField, Range(1, 3600)] float timeDayInSeconds;
    [SerializeField, Range(0f, 1f)] float timeProgress;

    [SerializeField] Light dirLight;

    Vector3 defaultAngels;
    void Start()
    {
        defaultAngels = dirLight.transform.localEulerAngles;
    }

   
    void Update()
    {
        if(Application.isPlaying)
            timeProgress += Time.deltaTime / timeDayInSeconds; 

        if(timeProgress > 1f) 
            timeProgress = 0f;

        dirLight.color = directionLightGradient.Evaluate(timeProgress);
        RenderSettings.ambientEquatorColor = directionLightGradient.Evaluate(timeProgress);

        dirLight.transform.localEulerAngles = new Vector3(360f * timeProgress - 90, defaultAngels.x, defaultAngels.z);

    }
}
