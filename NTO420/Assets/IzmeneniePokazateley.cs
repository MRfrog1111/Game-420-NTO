using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IzmeneniePokazateley : MonoBehaviour
{
    [SerializeField] private PlayerStats stats;
    public Scrollbar oxygen;
    public Scrollbar food;

    private void Start()
    {
        oxygen.size = 1;
        food.size = 1;
    }
    
    public void Oxygen()
    {
        oxygen.size = stats.resources.oxygen / 100f;
        print(stats.resources.oxygen);
    }

    public void Food()
    {
        food.size = stats.resources.food / 100f;
    }

}
