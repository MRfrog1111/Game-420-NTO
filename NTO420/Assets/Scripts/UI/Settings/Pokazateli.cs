using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class Pokazateli : MonoBehaviour
{
    [SerializeField] private Image hp;
    [SerializeField] private Image oxygen;
    [SerializeField] private Image food;


    private void Update()
    {
        IzmenenieHP();
        IzmenenieOxygen();
        IzmenenieFood();
    }

    public void IzmenenieHP()
    {
        hp.fillAmount = PlayerResources.hp / 100;
    }
    public void IzmenenieOxygen()
    {
        oxygen.fillAmount = PlayerResources.oxygen / 100;
    }
    public void IzmenenieFood()
    {
        food.fillAmount = PlayerResources.food / 100;
    }
}
