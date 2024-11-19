using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrintOxygen : MonoBehaviour
{
    public MC_movement MC_movement;
    public Text foodText;
    public Text oxygenText;

    private void Awake()
    {
        MC_movement = GetComponent<MC_movement>();
    }
    private void Update()
    {
        foodText.text = ("Food: " + (MC_movement.food)).ToString();
        oxygenText.text = ("Oxygen: " + (int)(MC_movement.oxygen)).ToString();
    }
}
