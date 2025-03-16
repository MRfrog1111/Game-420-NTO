using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sensetivity : MonoBehaviour
{
    public Slider sensetivity;
    
   
    private void Update()
    {
        
        CameraController.sens = sensetivity.value;
    }
}
