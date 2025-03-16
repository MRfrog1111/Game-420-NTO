using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class IzmeneniePokazatelei : MonoBehaviour
{
    private PlayerStats playerRes;

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
        hp.fillAmount = playerRes.resources.hp / 100;
    }
    public void IzmenenieOxygen()
    {
        oxygen.fillAmount = playerRes.resources.oxygen / 100;
    }
    public void IzmenenieFood()
    {
        food.fillAmount = playerRes.resources.food / 100;
    }
}
