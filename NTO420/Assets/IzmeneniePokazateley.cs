using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IzmeneniePokazateley : MonoBehaviour
{
    [SerializeField] private PlayerStats stats;
    public Image oxygen;
    public Image food;

    private void Start()
    {
        oxygen.fillAmount = 1;
        food.fillAmount = 1;
    }
    
    public void Oxygen()
    {
        oxygen.fillAmount = stats.resources.oxygen / 100;
        print(stats.resources.oxygen);
    }

    public void Food()
    {
        food.fillAmount = stats.resources.food / 100;
    }

}
